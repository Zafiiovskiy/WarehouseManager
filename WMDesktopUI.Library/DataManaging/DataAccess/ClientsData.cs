using System.Collections.Generic;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Library.DataManaging.SQLAccess;

namespace WMDesktopUI.Library.DataManaging.DataAccess
{
    public class ClientsData
    {
        public List<CModel> GetClients()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<CModel, dynamic>("dbo.spClientsGetAll", new { }, "WMData");
            return output;
        }
        public void PostClient(CPostModel model)
        {
            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData("dbo.spClientsPost", model, "WMData");
        }

        public void UpdateClient(CModel model)
        {
            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData("dbo.spClientsUpdate", model, "WMData");
        }
    }
}
