using System.Collections.Generic;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Models;

namespace WMDesktopUI.Library.DataManaging.DataAccess
{
    public interface IWareHouseData
    {
        WHProductModel GetProductById(object Id);
        List<WHProductModel> GetProducts();
        List<WHProductModel> GetProductsToBuy();
        void PostProduct(WHPostProductModel model);
        void UpdateProduct(WHProductModel model);
    }
}