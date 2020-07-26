using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WMDesktopUI.Events;
using WMDesktopUI.Helpers;
using WMDesktopUI.Library.DataManaging.DataAccess;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Models;

namespace WMDesktopUI.ViewModels
{
	public class OrdersViewModel : Screen, IHandle<OrderAllProductsDeletedEventModel>, IHandle<OpenOrdersEvent>
    {
		private IMapper _mapper;
		private ICommand _command;
		private IWindowManager _windowManager;
		private IEventAggregator _events;
		private IWareHouseData _wareHouseData;
		private IOrdersData _ordersData;
		private IClientsData _clientsData;
		/// <summary>
		/// Private backing fields
		/// </summary>
		private TextBlock _selectedValue; 
		private string _searchBox;
		private BindableCollection<OrderClientModel> _clients = new BindableCollection<OrderClientModel>();




		public OrdersViewModel(IMapper mapper,IWindowManager windowManager, IEventAggregator events,
			IWareHouseData wareHouseData, IClientsData clientsData, IOrdersData ordersData)
		{
			_mapper = mapper;
			_windowManager = windowManager;
			_clientsData = clientsData;
			_ordersData = ordersData;
			_wareHouseData = wareHouseData;
			_events = events;
			_events.Subscribe(this);
			LoadClients();
		}
		

		/// <summary>
		/// Public fields
		/// </summary>
		public TextBlock SelectedValue
		{
			get { return _selectedValue; }
			set
			{
				_selectedValue = value;
				NotifyOfPropertyChange(() => SelectedValue);
			}
		}
		public string SearchBox
		{
			get { return _searchBox; }
			set
			{
				_searchBox = value;
				NotifyOfPropertyChange(() => SearchBox);
			}
		}
		public BindableCollection<OrderClientModel> Clients
		{
			get { return _clients; }
			set
			{
				_clients = value;
				NotifyOfPropertyChange(() => Clients);
			}
		}

		/// <summary>
		/// Commands
		/// </summary>
		public ICommand Details
		{
			get
			{

				_command = new DelegateCommandHelper(CanExecute, UploadDetails);

				return _command;
			}
		}
		public ICommand ReverseOrder
		{
			get
			{

				_command = new DelegateCommandHelper(CanExecute, ExecuteReverseOrder);

				return _command;
			}
		}
		public ICommand FinishOrder
		{
			get
			{

				_command = new DelegateCommandHelper(CanExecute, ExecuteFinishOrder);

				return _command;
			}
		}

		private void ExecuteFinishOrder(object obj)
		{
			var client = obj as ClientModel;
			var orders = _ordersData.GetOrderAllByClientId(new { ClientId = client.Id });
			List<WHProductModel> productModels = new List<WHProductModel>();
			orders.ForEach(x => productModels.Add(_wareHouseData.GetProductById(new { x.ProductId })));
			List<HPostModel> historyPostModels = new List<HPostModel>();
			for (int i = 0; i < orders.Count; i++)
			{
				historyPostModels.Add(new HPostModel
				{
					Sender = true,
					ProductId = orders[i].ProductId,
					FactoryNumber = productModels[i].FactoryNumber,
					Name = productModels[i].Name,
					Set = productModels[i].Set,
					Type = productModels[i].Type,
					Photo = productModels[i].Photo,
					ProductQuantity = orders[i].ProductQuantity,
					ProductDescription = productModels[i].ProductDescription,
					ProductNetPrice = orders[i].ProductNetPrice,
					ProductSellPrice = orders[i].ProductSellPrice,
					OrderDateTime = orders[i].OrderDateTime,
					ClientId = client.Id
				});
			}
			HistoryData historyData = new HistoryData();
			foreach (var item in historyPostModels)
			{
				historyData.PostHistory(item);
			}
			RefreshView();
		}
		private void ExecuteReverseOrder(object obj)
		{
			try
			{
				var client = obj as ClientModel;
				OrdersData data = new OrdersData();
				var orders = data.GetOrderByClientId(new
				{
					ClientId = client.Id
				});
				orders.ForEach(x => data.ReverseOrders(x));
				RefreshView();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Message: \n" + ex.Message + '\n' +
						"StackTrase: \n" + ex.StackTrace + '\n' +
						"InnerException: \n" + ex.InnerException);
			}
		}
		private void UploadDetails(object obj)
		{
			try
			{
				var client = obj as ClientModel;
				_windowManager.ShowWindow(IoC.Get<OrderDetailsViewModel>());
				_events.PublishOnUIThread(new OrderDetailsEventModel(client));
			}
			catch (Exception ex)
			{
				MessageBox.Show("Message: \n" + ex.Message + '\n' +
						"StackTrase: \n" + ex.StackTrace + '\n' +
						"InnerException: \n" + ex.InnerException);
			}

		}
		private bool CanExecute(object parameter)
		{
			return true;
		}
		private void LoadClients()
		{
			try
			{
				var wareHouseList = _clientsData.GetClientsHaveOrders();
				var clients = _mapper.Map<List<OrderClientModel>>(wareHouseList);
				clients.ForEach(x => x.DateTimeOrder = _ordersData.GetOrderTimeByClient(new { ClientId = x.Id }).ToLocalTime());
				Clients = new BindableCollection<OrderClientModel>(clients);
			}
			catch(Exception ex)
			{
				MessageBox.Show("Message: \n" + ex.Message + '\n' +
						"StackTrase: \n" + ex.StackTrace + '\n' +
						"InnerException: \n" + ex.InnerException);
			}
		}
		public void RefreshView()
		{
			try
			{
				LoadClients();
				SearchBox = "";
				this.Refresh();
			}
			catch(Exception ex)
			{
				MessageBox.Show("Message: \n" + ex.Message + '\n' +
						"StackTrase: \n" + ex.StackTrace + '\n' +
						"InnerException: \n" + ex.InnerException);
			}
		}
		public void SearchByName()
		{
			try {
				if (Clients?.Count > 0)
				{
					if (SelectedValue?.Text == "за Номером")
					{
						var found = Clients.Where(x => x.PhoneNumber.ToString().Contains(SearchBox)).ToList();
						BindableCollection<OrderClientModel> result = new BindableCollection<OrderClientModel>();
						foreach (var item in Clients)
						{
							if (found.Contains(item))
							{
								result.Add(item);
							}
						}
						if (result.Count > 0)
						{
							Clients = result;
						}
						else
						{
							MessageBox.Show("Жодного результату за вашим запитом.");
						}
					}
					else if (SelectedValue?.Text == "за Ім'ям")
					{
						var found = Clients.Where(x => !String.IsNullOrWhiteSpace(x.Name)).Where(x => x.Name.Contains(SearchBox)).ToList();
						BindableCollection<OrderClientModel> result = new BindableCollection<OrderClientModel>();
						foreach (var item in Clients)
						{
							if (found.Contains(item))
							{
								result.Add(item);
							}
						}
						Clients = result;
					}
					else if (SelectedValue?.Text == "за Прізвищем")
					{
						var found = Clients.Where(x => !String.IsNullOrWhiteSpace(x.Surname)).Where(x => x.Surname.Contains(SearchBox)).ToList();
						BindableCollection<OrderClientModel> result = new BindableCollection<OrderClientModel>();
						foreach (var item in Clients)
						{
							if (found.Contains(item))
							{
								result.Add(item);
							}
						}
						if (result.Count > 0)
						{
							Clients = result;
						}
						else
						{
							MessageBox.Show("Жодного результату за вагим запитом.");
						}
					}
					else if (SelectedValue?.Text == "за Адресою")
					{
						var found = Clients.Where(x => !String.IsNullOrWhiteSpace(x.Adress)).Where(x => x.Adress.Contains(SearchBox)).ToList();
						BindableCollection<OrderClientModel> result = new BindableCollection<OrderClientModel>();
						foreach (var item in Clients)
						{
							if (found.Contains(item))
							{
								result.Add(item);
							}
						}
						if (result.Count > 0)
						{
							Clients = result;
						}
						else
						{
							MessageBox.Show("Жодного результату за вагим запитом.");
						}
					}
					else
					{
						MessageBox.Show("Жодного результату.");
					}
				}
				else
				{
					MessageBox.Show("Немає клієнтів із покупками.");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Message: \n" + ex.Message + '\n' +
						"StackTrase: \n" + ex.StackTrace + '\n' +
						"InnerException: \n" + ex.InnerException);
			}
		}

		/// <summary>
		/// Event handlers
		/// </summary>

		public void Handle(OrderAllProductsDeletedEventModel message)
		{
			RefreshView();
		}
		public void Handle(OpenOrdersEvent message)
		{
			RefreshView();
		}
	}
}

