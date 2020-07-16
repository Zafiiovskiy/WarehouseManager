using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WMDesktopUI.Models
{
    public class OrderModel : INotifyPropertyChanged
    {
        private bool _selected;
        private int _productQuantity;
        private decimal _productNetPrice;
        private decimal _productSellPrice;

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                CallPropertyChanged(nameof(Selected));
            }
        }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity 
        {
            get { return _productQuantity; }
            set
            {
                _productQuantity = value;
                CallPropertyChanged(nameof(ProductQuantity));
            } 
        }
        public decimal ProductNetPrice 
        {
            get
            {
                return _productNetPrice;
            }
            set
            {
                _productNetPrice = value;
                CallPropertyChanged(nameof(ProductNetPrice));
            } 
        }
        public decimal ProductSellPrice 
        {
            get
            {
                return _productSellPrice;
            }
            set
            {
                _productSellPrice = value;
                CallPropertyChanged(nameof(ProductSellPrice));
            }
        }
        public int ClientId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public bool IsDone { get; set; }

        public void CallPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
