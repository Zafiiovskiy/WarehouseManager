using System;
using System.Collections.Generic;
using WMDesktopUI.Library.DataManaging.Models;

namespace WMDesktopUI.Library.DataManaging.DataAccess
{
    public interface IOrdersData
    {
        List<OReverseModel> GetOrderByClientId(object Id);
        List<OModel> GetOrders();
        DateTime GetOrderTimeByClient(object Id);
        void PostOrder(OPostModel model);
        void ReverseOrderByProduct(OReverseModel model);
        List<OGetModel> GetOrderAllByClientId(object Id);
        void ReverseOrders(OReverseModel model);
        void UpdateOrder(OUpdateModel model);
    }
}