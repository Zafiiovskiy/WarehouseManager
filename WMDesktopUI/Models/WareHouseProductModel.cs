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
    public class WareHouseProductModel : INotifyPropertyChanged
    {
        private bool _selected;
        private bool _wasUpdated;
        private string _factoryNumber;
        private string _name;
        private string _set;
        private string _type;
        private byte[] _photo;
        private int _quantityInStock;
        private string _productDescription;
        private decimal _netPrice;
        private decimal _sellPrice;


        public bool Selected
        {
            get { return _selected; }
            set 
            {
                _selected = value;
                CallPropertyChanged(nameof(Selected));
            }
        }

        public bool WasUpdated
        {
            get { return _wasUpdated; }
            set 
            {
                _wasUpdated = value;
                CallPropertyChanged(nameof(WasUpdated));
            }
        }


        public int ProductId { get; set; }

        public string FactoryNumber
        {
            get { return _factoryNumber; }
            set
            {
                _factoryNumber = value;
                CallPropertyChanged(nameof(FactoryNumber));
                WasUpdated = true;
                CallPropertyChanged(nameof(WasUpdated));
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                CallPropertyChanged(nameof(Name));
                WasUpdated = true;
                CallPropertyChanged(nameof(WasUpdated));
            }
        }
        public string Set
        {
            get { return _set; }
            set
            {
                _set = value;
                CallPropertyChanged(nameof(Set));
                WasUpdated = true;
                CallPropertyChanged(nameof(WasUpdated));
            }
        }
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                CallPropertyChanged(nameof(Type));
                WasUpdated = true;
                CallPropertyChanged(nameof(WasUpdated));
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
                WasUpdated = true;
                CallPropertyChanged(nameof(WasUpdated));
            }
        }
        public int QuantityInStock
        {
            get { return _quantityInStock; }
            set
            {
                _quantityInStock = value;
                CallPropertyChanged(nameof(QuantityInStock));
                WasUpdated = true;
                CallPropertyChanged(nameof(WasUpdated));
            }
        }
        public string ProductDescription
        {
            get { return _productDescription; }
            set
            {
                _productDescription = value;
                CallPropertyChanged(nameof(ProductDescription));
                WasUpdated = true;
                CallPropertyChanged(nameof(WasUpdated));
            }
        }
        public decimal NetPrice
        {
            get { return _netPrice; }
            set
            {
                _netPrice = value;
                CallPropertyChanged(nameof(NetPrice));
                WasUpdated = true;
                CallPropertyChanged(nameof(WasUpdated));
            }
        }
        public decimal SellPrice
        {
            get { return _sellPrice; }
            set
            {
                _sellPrice = value;
                CallPropertyChanged(nameof(SellPrice));
                WasUpdated = true;
                CallPropertyChanged(nameof(WasUpdated));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void CallPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private static BitmapImage toBitmap(Byte[] value)
        {
            if (value != null && value is byte[])
            {
                byte[] ByteArray = value as byte[];
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(ByteArray);
                bmp.EndInit();
                return bmp;
            }
            return null;
        }
    }
}
