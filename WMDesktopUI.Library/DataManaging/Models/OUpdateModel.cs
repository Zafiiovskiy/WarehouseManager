using System;
using System.Collections.Generic;
using System.Text;

namespace WMDesktopUI.Library.DataManaging.Models
{
    public class OUpdateModel
    {
        public int ProductId { get; set; }
        public int OrderQuantity { get; set; }
        public int WareHouseQuantity { get; set; }
        public decimal ProductNetPrice { get; set; }
        public decimal ProductSellPrice { get; set; }
        public int ClientId { get; set; }
    }
}
