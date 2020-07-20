using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Models;

namespace WMDesktopUI.Events
{
    public class ToBuyDetailsEventModel
    {
        public BindableCollection<WareHouseProductModel> ToBuyProducts { get; set; }
        public ClientModel Client { get; set; }
        public List<OReverseModel> ToBuys { get; set; }
        public ToBuyDetailsEventModel(BindableCollection<WareHouseProductModel> toBuyProducts, ClientModel client, List<OReverseModel> toBuys)
        {
            ToBuyProducts = toBuyProducts;
            Client = client;
            ToBuys = toBuys;
        }
    }
}
