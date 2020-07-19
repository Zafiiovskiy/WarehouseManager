using Caliburn.Micro;
using WMDesktopUI.Models;

namespace WMDesktopUI.Events
{
    public class MakeToBuyEventModel
    {
        public BindableCollection<WareHouseProductModel> ToBuyProducts { get; set; }
        public MakeToBuyEventModel(BindableCollection<WareHouseProductModel> toBuyProducts)
        {
            ToBuyProducts = toBuyProducts;
        }
    }
}
