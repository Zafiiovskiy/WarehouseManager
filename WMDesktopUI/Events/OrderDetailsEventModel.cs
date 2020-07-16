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
    public class OrderDetailsEventModel
    {
        public BindableCollection<WareHouseProductModel> OrderProducts { get; set; }
        public ClientModel Client { get; set; }
        public List<OReverseModel> Orders { get; set; }
        public OrderDetailsEventModel(BindableCollection<WareHouseProductModel> orderProducts,ClientModel client, List<OReverseModel> orders)
        {
            OrderProducts = orderProducts;
            Client = client;
            Orders = orders;
        }
    }
}
