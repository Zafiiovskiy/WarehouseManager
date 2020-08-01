using System.Collections.Generic;
using WMDesktopUI.Library.DataManaging.Models;

namespace WMDesktopUI.Library.DataManaging.DataAccess
{
    public interface IClientsData
    {
        List<CModel> GetClients();
        CModel GetClientsById(object Id);
        List<CModel> GetClientsHaveOrders();
        List<CModel> GetClientsHaveToBuys();
        void PostClient(CPostModel model);
        void UpdateClient(CModel model);
    }
}