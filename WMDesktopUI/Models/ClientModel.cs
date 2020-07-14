using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMDesktopUI.Models
{
    public class ClientModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string _phoneNumber;
        private bool _selected;
        private string _name;
        private string _surname;
        private string _adress;
        private bool _wasUpdated = false;



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

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set 
            {
                _phoneNumber = value;
                CallPropertyChanged(nameof(PhoneNumber));
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

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                CallPropertyChanged(nameof(Surname));
                WasUpdated = true;
                CallPropertyChanged(nameof(WasUpdated));
            }
        }


        public string Adress
        {
            get { return _adress; }
            set
            {
                _adress = value;
                CallPropertyChanged(nameof(Adress));
                WasUpdated = true;
                CallPropertyChanged(nameof(WasUpdated));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void CallPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
