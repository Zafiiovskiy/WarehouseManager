using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMDesktopUI.Models
{
    public class WHProductModel
    {
        public int ProductId { get; set; }
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
