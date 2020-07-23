using System.Collections.Generic;
using WMDesktopUI.Library.DataManaging.Models;

namespace WMDesktopUI.Library.DataManaging.DataAccess
{
    public interface IOrdersData
    {
        List<OReverseModel> GetOrderByClientId(object Id);
        List<OModel> GetOrders();
        void PostOrder(OPostModel model);
        void ReverseOrderByProduct(OReverseModel model);
        void ReverseOrders(OReverseModel model);
        void UpdateOrder(OUpdateModel model);
    }
}