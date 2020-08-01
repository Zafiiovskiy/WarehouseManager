using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Library.DataManaging.SQLAccess;

namespace WMDesktopUI.Library.DataManaging.DataAccess
{
    public class ClientsData : IClientsData
    {
        public List<CModel> GetClients()
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                var output = sql.LoadData<CModel, dynamic>("dbo.spClientsGetAll", new { }, "WMData");
                return output;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("GetClients() coundn't get data.", ex);
            }
        }
        public CModel GetClientsById(object Id)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                var output = sql.LoadData<CModel, dynamic>("dbo.spClientsGetById", Id, "WMData");
                return output.First();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("GetClientsById() coundn't get data.", ex);
            }
        }
        public List<CModel> GetClientsHaveOrders()
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                var output = sql.LoadData<CModel, dynamic>("dbo.spClientsGetHaveOrders", new { }, "WMData");
                return output;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("GetClientsHaveOrders() coundn't get data.", ex);
            }
        }
        public List<CModel> GetClientsHaveToBuys()
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                var output = sql.LoadData<CModel, dynamic>("dbo.spClientsGetHaveToBuys", new { }, "WMData");
                return output;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("GetClientsHaveToBuys() coundn't get data.", ex);
            }
        }
        public void PostClient(CPostModel model)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData("dbo.spClientsPost", model, "WMData");
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"PostClient(CPostModel model) coundn't post data.(model.PhoneNumber = {model.PhoneNumber})", ex);
            }
        }
        public void UpdateClient(CModel model)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData("dbo.spClientsUpdate", model, "WMData");
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"UpdateClient(CPostModel model) coundn't update data.(model.PhoneNumber = {model.PhoneNumber})", ex);
            }
        }
    }
}
