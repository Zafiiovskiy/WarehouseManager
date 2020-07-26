using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Library.DataManaging.SQLAccess;

namespace WMDesktopUI.Library.DataManaging.DataAccess
{
    public class ToBuysData
    {
        public void PostToBuy(OPostModel model)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData("dbo.spToBuysPost", model, "WMData");
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"PostToBuy(OPostModel model) coundn't post data (model.ProductId = {model.ProductId}).", ex);
            }
        }
        public List<OReverseModel> GetToBuyByClientId(object Id)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                var output = sql.LoadData<OReverseModel, dynamic>("dbo.spToBuysGetClientId", Id, "WMData");
                return output;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"GetToBuyByClientId(object Id) coundn't get data (Id = {Id.ToString()}).", ex);
            }
        }
        public void ReverseToBuyByProduct(OReverseModel model)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData("dbo.spToBuysReversByProductId", model, "WMData");
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"ReverseToBuyByProduct(OReverseModel model) coundn't post data (model.ProductId = {model.ProductId}).", ex);
            }
        }
        public void ReverseToBuys(OReverseModel model)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData("dbo.spToBuysReverseClient", model, "WMData");
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"ReverseToBuys(OReverseModel model) coundn't post data (model.ProductId = {model.ProductId}).", ex);
            }
        }
        public List<OReverseModel> GetToBuysByClientId(object Id)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                var output = sql.LoadData<OReverseModel, dynamic>("dbo.spToBuysGetClientId", Id, "WMData");
                return output;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"GetOrderByClientId(object Id) coundn't get data (Id = {Id.ToString()}).", ex);
            }
        }
        public List<OGetModel> GetToBuysAllByClientId(object Id)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                var output = sql.LoadData<OGetModel, dynamic>("dbo.spToBuysGetAllClientId", Id, "WMData");
                return output;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"GetToBuysAllByClientId(object Id) coundn't get data (Id = {Id.ToString()}).", ex);
            }
        }
        public void UpdateToBuy(OPostModel model)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData("dbo.spToBuysUpdate", model, "WMData");
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"PostToBuy(OPostModel model) coundn't post data (model.ProductId = {model.ProductId}).", ex);
            }
        }
    }
}

