﻿using System.Collections.Generic;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Library.DataManaging.SQLAccess;
using WMDesktopUI.Models;

namespace WMDesktopUI.Library.DataManaging.DataAccess
{
    public class WareHouseData
    {
        public List<WHProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<WHProductModel, dynamic>("dbo.spWareHouseGetAll", new { }, "WMData");
            return output;
        }
        public void PostProduct(WHPostProductModel model)
        {
            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData("dbo.spWareHousePostAll", model, "WMData");
        }

        public void UpdateProduct(WHProductModel model)
        {
            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData("dbo.spWareHouseUpdateAll", model, "WMData");
        }
    }
}