using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Library.DataManaging.SQLAccess;

namespace WMDesktopUI.Library.DataManaging.DataAccess
{
    public class OrdersData : IOrdersData
    {
        public List<OModel> GetOrders()
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                var output = sql.LoadData<OModel, dynamic>("dbo.spOrdersGet", new { }, "WMData");
                return output;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("GetOrders() coundn't return data.", ex);
            }
        }

        public void ReverseOrders(OReverseModel model)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData("dbo.spOrdersReverseClient", model, "WMData");
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"ReverseOrders(OReverseModel model) coundn't post data (model.ProductId = {model.ProductId}).", ex);
            }
        }

        public void ReverseOrderByProduct(OReverseModel model)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData("dbo.spOrdersReversByProductId", model, "WMData");
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"ReverseOrderByProduct(OReverseModel model) coundn't post data (model.ProductId = {model.ProductId}).", ex);
            }
        }

        public List<OReverseModel> GetOrderByClientId(object Id)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                var output = sql.LoadData<OReverseModel, dynamic>("dbo.spOrdersGetClientId", Id, "WMData");
                return output;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"GetOrderByClientId(object Id) coundn't get data (Id = {Id.ToString()}).", ex);
            }
        }


        public void PostOrder(OPostModel model)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData("dbo.spOrdersPost", model, "WMData");
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"PostOrder(OPostModel model) coundn't post data (model.ProductId = {model.ProductId}).", ex);
            }
        }
    }
}
