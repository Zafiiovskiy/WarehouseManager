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
            try
            {
                return ConfigurationManager.ConnectionStrings[name].ConnectionString;
            }
            catch(Exception ex)
            {
                throw new ConfigurationErrorsException("Connection string wasn't loaded.", ex);
            }
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {

            try
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
            catch (Exception ex)
            {
                throw new DataException($"LoadData could't run {storedProcedure} with parameters [{parameters.ToString()}] to database {connectionStringName}", ex);
            }
        }

        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            try
            {
                string connectionString = GetConnectionString(connectionStringName);
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    connection.Execute
                        (storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new DataException($"SaveData could't run {storedProcedure} with parameters [{parameters.ToString()}] to database {connectionStringName}", ex);
            }
        }

        private IDbConnection dbConnection;
        private IDbTransaction dbTransaction;
        public void StartTransaction(string connectionStringName)
        {
            try
            {
                string connectionString = GetConnectionString(connectionStringName);
                dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
            }
            catch (Exception ex)
            {
                dbConnection.Close();
                dbTransaction.Commit();
                throw new DataException($"StartTransaction could't run to database {connectionStringName}", ex);
            }
        }

        public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
        {
            try
            {
                dbConnection.Execute
                        (storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: dbTransaction);
            }
            catch (Exception ex)
            {
                dbConnection.Close();
                throw new DataException($"SaveDataInTransaction could't run {storedProcedure} with parameters [{parameters.ToString()}]", ex);
            }
        }

        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            try
            {
                List<T> rows = dbConnection.Query<T>
                            (storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: dbTransaction)
                            .ToList();

                return rows;
            }
            catch (Exception ex)
            {
                dbConnection.Close();
                throw new DataException($"LoadDataInTransaction could't run {storedProcedure} with parameters [{parameters.ToString()}] to database {connectionStringName}", ex);
            }
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
