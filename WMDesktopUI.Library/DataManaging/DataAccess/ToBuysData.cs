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
    }
}

