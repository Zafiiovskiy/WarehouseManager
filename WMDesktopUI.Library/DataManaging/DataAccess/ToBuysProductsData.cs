using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Library.DataManaging.SQLAccess;
using WMDesktopUI.Models;

namespace WMDesktopUI.Library.DataManaging.DataAccess
{
    public class ToBuysProductsData
    {
        public List<WHProductModel> GetProducts()
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                var output = sql.LoadData<WHProductModel, dynamic>("dbo.spToBuysProductsGetAll", new { }, "WMData");
                return output;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("GetProducts() coundn't post data.", ex);
            }
        }
       
        
        public WHProductModel GetProductById(object Id)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                var output = sql.LoadData<WHProductModel, dynamic>("dbo.spToBuysProductsGetById", Id, "WMData");
                return output.First();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"GetProductById(object Id) coundn't get data. (Id = {Id.ToString()})", ex);
            }
        }
        public WHProductModel PostProduct(WHPostProductModel model)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                var output = sql.LoadData<WHProductModel,dynamic>("dbo.spToBuysProductsPostAll", model, "WMData");
                return output.First();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"PostProduct(WHPostProductModel model) coundn't post data. (model = {model.FactoryNumber})", ex);
            }
        }
    }
}

