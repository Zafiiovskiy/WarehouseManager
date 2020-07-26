using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMDesktopUI.Models
{
    public class OrderClientModel : ClientModel , INotifyPropertyChanged
    {
        private DateTime _dateTimeOrder;

        public DateTime DateTimeOrder
        {
            get { return _dateTimeOrder; }
            set 
            {
                _dateTimeOrder = value;
                CallPropertyChanged(nameof(DateTimeOrder));
            }
        }
    }
}
