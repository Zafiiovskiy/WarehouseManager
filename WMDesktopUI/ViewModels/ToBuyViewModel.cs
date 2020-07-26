using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class ToBuyViewModel : Screen, IHandle<ToBuysAllProductsDeletedEventModel>
    {
		private IMapper _mapper;
		private ICommand _command;
		private IWindowManager _windowManager;
		private IEventAggregator _events;
		private IWareHouseData _wareHouseData;
		private IClientsData _clientsData;
		private IOrdersData _ordersData;
		/// <summary>
		/// Private backing fields
		/// </summary>
		private TextBlock _selectedValue;
		private string _searchBox;
		private BindableCollection<ClientModel> _clients = new BindableCollection<ClientModel>();

		public ToBuyViewModel(IMapper mapper, IWindowManager windowManager, IEventAggregator events, 
			IWareHouseData wareHouseData, IClientsData clientsData, IOrdersData ordersData)
		{
			_mapper = mapper;
			_ordersData = ordersData;
			_clientsData = clientsData;
			_windowManager = windowManager;
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
		public BindableCollection<ClientModel> Clients
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
			ToBuysData _toBuysData = new ToBuysData();
			ToBuysProductsData _toBuysProductsData = new ToBuysProductsData();
			var orders = _toBuysData.GetToBuysAllByClientId(new { ClientId = client.Id });
			List<WHProductModel> productModels = new List<WHProductModel>();
			orders.ForEach(x => productModels.Add(_toBuysProductsData.GetProductById(new { x.ProductId })));
			List<HPostModel> historyPostModels = new List<HPostModel>();
			for (int i = 0; i < orders.Count; i++)
			{
				historyPostModels.Add(new HPostModel
				{
					Sender = false,
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
				ToBuysData _toBuysData = new ToBuysData();
				var toBuys = _toBuysData.GetToBuysByClientId(new
				{
					ClientId = client.Id
				});
				toBuys.ForEach(x => _toBuysData.ReverseToBuys(x));
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
				ToBuysData toBuysData = new ToBuysData();
				var orders = toBuysData.GetToBuysByClientId(new
				{
					ClientId = client.Id
				});
				ToBuysProductsData toBuysProductsData = new ToBuysProductsData();
				List<WHProductModel> productsDB = new List<WHProductModel>();
				orders.ForEach(x => productsDB.Add(toBuysProductsData.GetProductById(new
				{
					x.ProductId
				})));
				BindableCollection<WareHouseProductModel> products = new BindableCollection<WareHouseProductModel>(_mapper.Map<List<WareHouseProductModel>>(productsDB));
				_windowManager.ShowWindow(IoC.Get<ToBuyDetailsViewModel>());
				_events.PublishOnUIThread(new ToBuyDetailsEventModel(products, client, orders));
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
				var wareHouseList = _clientsData.GetClientsHaveToBuys();
				var clients = _mapper.Map<List<ClientModel>>(wareHouseList);
				Clients = new BindableCollection<ClientModel>(clients);
			}
			catch (Exception ex)
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
			catch (Exception ex)
			{
				MessageBox.Show("Message: \n" + ex.Message + '\n' +
						"StackTrase: \n" + ex.StackTrace + '\n' +
						"InnerException: \n" + ex.InnerException);
			}
		}
		public void SearchByName()
		{
			try
			{
				if (SearchBox != null)
				{
					if (Clients?.Count > 0)
					{
						if (SelectedValue?.Text == "за Номером")
						{
							var found = Clients.Where(x => x.PhoneNumber.ToString().Contains(SearchBox)).ToList();
							BindableCollection<ClientModel> result = new BindableCollection<ClientModel>();
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
							BindableCollection<ClientModel> result = new BindableCollection<ClientModel>();
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
							BindableCollection<ClientModel> result = new BindableCollection<ClientModel>();
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
							BindableCollection<ClientModel> result = new BindableCollection<ClientModel>();
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
				else
				{
					MessageBox.Show("Введіть параметри пошуку.");
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
		public void Handle(ToBuysAllProductsDeletedEventModel message)
		{
			RefreshView();
		}
	}
}
