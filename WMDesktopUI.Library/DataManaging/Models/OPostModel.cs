using System;
using System.Collections.Generic;
using System.Text;

namespace WMDesktopUI.Library.DataManaging.Models
{
    public class OPostModel
    {
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductNetPrice { get; set; }
        public decimal ProductSellPrice { get; set; }
        public int ClientId { get; set; }
    }
}
