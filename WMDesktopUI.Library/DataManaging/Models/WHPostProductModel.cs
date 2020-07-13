using System;
using System.Collections.Generic;
using System.Text;

namespace WMDesktopUI.Library.DataManaging.Models
{
    public class WHPostProductModel
    {
        public string FactoryNumber { get; set; }
        public string Name { get; set; }
        public string Set { get; set; }
        public string Type { get; set; }
        public byte[] Photo { get; set; }
        public int QuantityInStock { get; set; }
        public string ProductDescription { get; set; }
        public decimal NetPrice { get; set; }
        public decimal SellPrice { get; set; }
    }
}
