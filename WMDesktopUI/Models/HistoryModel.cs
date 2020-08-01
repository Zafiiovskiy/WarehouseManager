using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WMDesktopUI.Models
{
    public class HistoryModel
    {
        public int Id { get; set; }
        public string FactoryNumber { get; set; }
        public string Name { get; set; }
        public string Set { get; set; }
        public string Type { get; set; }
        public byte[] Photo { get; set; }
        public string ProductDescription { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductNetPrice { get; set; }
        public decimal ProductSellPrice { get; set; }
        public int ClientId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime FinishDateTime { get; set; }
        public ClientModel Client { get; set; }
    }
}
