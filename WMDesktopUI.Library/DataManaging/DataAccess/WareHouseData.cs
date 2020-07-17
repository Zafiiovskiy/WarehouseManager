using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Library.DataManaging.SQLAccess;
using WMDesktopUI.Models;

namespace WMDesktopUI.Library.DataManaging.DataAccess
{
    public class WareHouseData
    {
        public List<WHProductModel> GetProducts()
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                var output = sql.LoadData<WHProductModel, dynamic>("dbo.spWareHouseGetAll", new { }, "WMData");
                return output;
            }
            catch(Exception ex)
            {
                throw new InvalidDataException("GetProducts() coundn't post data.", ex);
            }
        }

        public WHProductModel GetProductById(object Id)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                var output = sql.LoadData<WHProductModel, dynamic>("dbo.spWareHouseGetById", Id, "WMData");
                return output.First();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"GetProductById(object Id) coundn't get data. (Id = {Id.ToString()})", ex);
            }
        }
        public void PostProduct(WHPostProductModel model)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData("dbo.spWareHousePostAll", model, "WMData");
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"PostProduct(WHPostProductModel model) coundn't post data. (model = {model.FactoryNumber})", ex);
            }
        }

        public void UpdateProduct(WHProductModel model)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData("dbo.spWareHouseUpdateAll", model, "WMData");
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"UpdateProduct(WHProductModel model) coundn't update data. (model = {model.FactoryNumber})", ex);
            }
        }
    }
}
