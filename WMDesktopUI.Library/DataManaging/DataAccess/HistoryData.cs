using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Library.DataManaging.SQLAccess;

namespace WMDesktopUI.Library.DataManaging.DataAccess
{
    public class HistoryData
    {
        public void PostHistory(HPostModel model)
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData("dbo.spHistoryPost", model, "WMData");
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"PostHistory(CPostModel model) coundn't post data.", ex);
            }
        }
        public List<HModel> GetHistory()
        {
            try
            {
                SqlDataAccess sql = new SqlDataAccess();
                var result = sql.LoadData<HModel, dynamic>("dbo.spHistoryGetAll", new { }, "WMData");
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"GetHistory() coundn't get data.", ex);
            }
        }
    }
}
