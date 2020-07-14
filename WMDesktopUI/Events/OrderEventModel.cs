using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMDesktopUI.Models;

namespace WMDesktopUI.Events
{
    public class OrderEventModel
    {
        public BindableCollection<WareHouseProductModel> OrderProducts { get; set; }
        public OrderEventModel(BindableCollection<WareHouseProductModel> orderProducts)
        {
            OrderProducts = orderProducts;
        }
    }
}
