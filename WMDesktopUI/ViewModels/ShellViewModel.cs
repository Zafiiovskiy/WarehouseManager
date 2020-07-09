using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        
        public void OpenWareHouse()
        {
            ActivateItem(new WareHauseViewModel());
        }
        public void OpenOrders()
        {
            ActivateItem(new OrdersViewModel());
        }
        public void OpenHistory()
        {
            ActivateItem(new HistoryViewModel());
        }
        public void OpenToBuy()
        {
            ActivateItem(new ToBuyViewModel());
        }
        public void OpenClients()
        {
            ActivateItem(new ClientsViewModel());
        }
    }
}
