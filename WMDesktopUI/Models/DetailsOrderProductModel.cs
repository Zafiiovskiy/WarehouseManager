using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMDesktopUI.Models
{
    public class DetailsOrderProductModel : INotifyPropertyChanged
    {
        private string _factoryNumber;
        private string _name;
        private string _set;
        private string _type;
        private byte[] _photo;
        private int _quantityInStock;
        private string _productDescription;
        private decimal _netPrice;
        private decimal _sellPrice;


        


        public int ProductId { get; set; }
        public int MaxQuantity { get; set; }
        public int CurrentQuantityInStock
        {
            get { return (MaxQuantity - QuantityInStock); }
            set
            {
            }
        }

        public string FactoryNumber
        {
            get { return _factoryNumber; }
            set
            {
                _factoryNumber = value;
                CallPropertyChanged(nameof(FactoryNumber));
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                CallPropertyChanged(nameof(Name));
            }
        }
        public string Set
        {
            get { return _set; }
            set
            {
                _set = value;
                CallPropertyChanged(nameof(Set));
            }
        }
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                CallPropertyChanged(nameof(Type));
            }
        }
        public byte[] Photo
        {
            get
            {
                return _photo;
            }
            set
            {
                _photo = value;
                CallPropertyChanged(nameof(Photo));
            }
        }
        public int QuantityInStock
        {
            get { return _quantityInStock; }
            set
            {
                _quantityInStock = value;
                CallPropertyChanged(nameof(QuantityInStock));
            }
        }
        public string ProductDescription
        {
            get { return _productDescription; }
            set
            {
                _productDescription = value;
                CallPropertyChanged(nameof(ProductDescription));
            }
        }
        public decimal NetPrice
        {
            get { return _netPrice; }
            set
            {
                _netPrice = value;
                CallPropertyChanged(nameof(NetPrice));
            }
        }
        public decimal SellPrice
        {
            get { return _sellPrice; }
            set
            {
                _sellPrice = value;
                CallPropertyChanged(nameof(SellPrice));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void CallPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
