using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using WMDesktopUI.Views;

namespace WMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private IEventAggregator _events;
        private WareHauseViewModel _wareHauseViewModel;
        private OrdersViewModel _ordersViewModel;
        private HistoryViewModel _historyViewModel;
        private ToBuyViewModel _toBuyViewModel;
        private ClientsViewModel _clientsViewModel;
        private AddProductToWareHouseViewModel _addProductToWareHouseViewModel;
        private AddClientsViewModel _addClientsViewModel;

        public ShellViewModel(WareHauseViewModel wareHauseViewModel, OrdersViewModel ordersViewModel,
            HistoryViewModel historyViewModel, ToBuyViewModel toBuyViewModel, ClientsViewModel clientsViewModel,
            IEventAggregator events, AddProductToWareHouseViewModel addProductToWareHouseViewModel,
            AddClientsViewModel addClientsViewModel)
        {
            _events = events;
            _wareHauseViewModel = wareHauseViewModel;
            _addProductToWareHouseViewModel = addProductToWareHouseViewModel;
            _ordersViewModel = ordersViewModel;
            _historyViewModel = historyViewModel;
            _toBuyViewModel = toBuyViewModel;
            _clientsViewModel = clientsViewModel;
            _addClientsViewModel = addClientsViewModel;
            WindowMaximizeButtonVisibility = Visibility.Collapsed;
            MyWindowState = WindowState.Maximized;
            ActivateItem(_wareHauseViewModel);
        }

        private Visibility _windowMaximizeButtonVisibility;

        public Visibility WindowMaximizeButtonVisibility
        {
            get { return _windowMaximizeButtonVisibility; }
            set 
            {
                _windowMaximizeButtonVisibility = value;
                NotifyOfPropertyChange(() => WindowMaximizeButtonVisibility);
            }
        }

        private Visibility _windowMinimizeButtonVisibility;

        public Visibility WindowMinimizeButtonVisibility
        {
            get { return _windowMinimizeButtonVisibility; }
            set 
            {
                _windowMinimizeButtonVisibility = value;
                NotifyOfPropertyChange(() => WindowMinimizeButtonVisibility);
            }
        }
        private WindowState _myWindowState;

        public WindowState MyWindowState
        {
            get { return _myWindowState; }
            set 
            {
                _myWindowState = value;
                NotifyOfPropertyChange(() => MyWindowState);
            }
        }



        public void OpenWareHouse()
        {
            ActivateItem(_wareHauseViewModel);
        }
        public void OpenOrders()
        {
            ActivateItem(_ordersViewModel);
        }
        public void OpenHistory()
        {
            ActivateItem(_historyViewModel);
        }
        public void OpenToBuy()
        {
            ActivateItem(_toBuyViewModel);
        }
        public void OpenClients()
        {
            ActivateItem(_clientsViewModel);
        }
        public void OpenAddClients()
        {
            ActivateItem(_addClientsViewModel);
        }
        public void OpenAddToWareHouse()
        {
            ActivateItem(_addProductToWareHouseViewModel);
        }
        public void CloseApplication()
        {
            MessageBoxResult result = MessageBox.Show("Чи справді хочете закрити програму?\nЗбережіть всі зміни перед виходом.",
                                          "Confirmation",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
        public void GoMinScreen()
        {
            WindowMinimizeButtonVisibility = Visibility.Collapsed;
            WindowMaximizeButtonVisibility = Visibility.Visible;
            MyWindowState = WindowState.Normal;
        }
        public void GoFullScreen()
        {
            WindowMinimizeButtonVisibility = Visibility.Visible;
            WindowMaximizeButtonVisibility = Visibility.Collapsed;
            MyWindowState = WindowState.Maximized;
        }
        public void GoRoll()
        {
            MyWindowState =  WindowState.Minimized;
        }


    }
}
