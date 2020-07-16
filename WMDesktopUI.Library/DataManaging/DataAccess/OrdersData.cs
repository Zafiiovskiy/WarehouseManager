using System;
using System.Collections.Generic;
using System.Text;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Library.DataManaging.SQLAccess;

namespace WMDesktopUI.Library.DataManaging.DataAccess
{
    public class OrdersData
    {
        public List<OModel> GetOrders()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<OModel, dynamic>("dbo.spOrdersGet", new { }, "WMData");
            return output;
        }

        public void ReverseOrders(OReverseModel model)
        {
            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData("dbo.spOrdersReverseClient", model, "WMData");
        }

        public void ReverseOrderByProduct(OReverseModel model)
        {
            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData("dbo.spOrdersReversByProductId", model, "WMData");
        }

        public List<OReverseModel> GetOrderByClientId(object model)
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<OReverseModel, dynamic>("dbo.spOrdersGetClientId", model, "WMData");
            return output;
        }


        public void PostOrder(OPostModel model)
        {
            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData("dbo.spOrdersPost", model, "WMData");
        }
    }
}
