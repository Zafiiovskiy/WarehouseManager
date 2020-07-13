using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMDesktopUI.Library.DataManaging.SQLAccess
{
    public class SqlDataAccess : IDisposable
    {
        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            
            string connectionString = GetConnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>
                    (storedProcedure, parameters, commandType: CommandType.StoredProcedure)
                    .ToList();

                return rows;
            }
        }

        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute
                    (storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        private IDbConnection dbConnection;
        private IDbTransaction dbTransaction;
        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
            dbTransaction = dbConnection.BeginTransaction();
        }

        public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
        {
            dbConnection.Execute
                    (storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: dbTransaction);
        }

        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            List<T> rows = dbConnection.Query<T>
                    (storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: dbTransaction)
                    .ToList();

            return rows;
        }
        public void CommitTransaction()
        {
            dbTransaction?.Commit();
            dbConnection?.Close();
        }

        public void RollBackTransaction()
        {
            dbTransaction?.Rollback();
            dbConnection?.Close();
        }

        public void Dispose()
        {
            CommitTransaction();
        }
    }
}
