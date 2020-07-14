using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using WMDesktopUI.Events;
using WMDesktopUI.Library.DataManaging.DataAccess;
using WMDesktopUI.Models;

namespace WMDesktopUI.ViewModels
{
	public class MakeOrderViewModel: Screen, IHandle<OrderEventModel>
    {
		IMapper _mapper;
		private WareHouseProductModel modelSums;
		private IEventAggregator _events;
		public MakeOrderViewModel(IEventAggregator events,IMapper mapper)
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
			_events = events;
			_mapper = mapper;
			_events.Subscribe(this);

			
		}
		private WareHouseProductModel CountSums()
		{
			
			var sumOfNetPrices = ProductsForOrder.Sum(x => x.NetPrice);
			var sumOfSellPrices = ProductsForOrder.Sum(x => x.SellPrice);
			WareHouseProductModel model = new WareHouseProductModel
			{
				SellPrice = sumOfSellPrices,
				NetPrice = sumOfNetPrices
			};
			return model;
		}

		private ClientModel _selectedClient;

		public ClientModel SelectedClient
		{
			get { return _selectedClient; }
			set
			{
				_selectedClient = value;
				NotifyOfPropertyChange(() => SelectedClient);
				NotifyOfPropertyChange(() => SelectedClientString);
				NotifyOfPropertyChange(() => CanMakeOrder);
			}
		}

		public string SelectedClientString
		{
			get 
			{
				if(SelectedClient == null)
				{
					return "Оберіть клієнта";
				}
				return $"Клієнт: ({SelectedClient.PhoneNumber}) - {SelectedClient.Name} {SelectedClient.Surname}";
			}
			set
			{
				NotifyOfPropertyChange(() => SelectedClientString);
			}
		}
		private string _profit;

		public string Profit
		{
			get { return _profit; }
			set
			{
				_profit = value;
				NotifyOfPropertyChange(() => Profit);
			}
		}

		private string _sumOfNetPrices;

		public string SumOfNetPrices
		{
			get { return _sumOfNetPrices; }
			set
			{
				_sumOfNetPrices = value;
				NotifyOfPropertyChange(() => SumOfNetPrices);
			}
		}

		private string _sumOfSellPrices;

		public string SumOfSellPrices
		{
			get { return _sumOfSellPrices; }
			set
			{
				_sumOfSellPrices = value;
				NotifyOfPropertyChange(() => SumOfSellPrices);
			}
		}

		private BindableCollection<ClientModel> _clients = new BindableCollection<ClientModel>();

		public BindableCollection<ClientModel> Clients
		{
			get { return _clients; }
			set
			{
				_clients = value;
				NotifyOfPropertyChange(() => Clients);
			}
		}

		private BindableCollection<WareHouseProductModel> _productsForOrder = new BindableCollection<WareHouseProductModel>();

		public BindableCollection<WareHouseProductModel> ProductsForOrder
		{
			get { return _productsForOrder; }
			set
			{
				_productsForOrder = value;
				NotifyOfPropertyChange(() => ProductsForOrder);
			}
		}

		private TextBlock _selectedValue;

		public TextBlock SelectedValue
		{
			get { return _selectedValue; }
			set
			{
				_selectedValue = value;
				NotifyOfPropertyChange(() => SelectedValue);
			}
		}

		private string _searchBox;

		public string SearchBox
		{
			get { return _searchBox; }
			set
			{
				_searchBox = value;
				NotifyOfPropertyChange(() => SearchBox);
			}
		}
		public void SearchByName()
		{
			try
			{
				if (SelectedValue.Text == "за Номером")
				{
					var found = Clients.Where(x => !String.IsNullOrWhiteSpace(x.Name)).Where(x => x.PhoneNumber.Contains(SearchBox)).ToList();
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
				else if (SelectedValue.Text == "за Ім'ям")
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
				else if (SelectedValue.Text == "за Прізвищем")
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
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public bool CanMakeOrder
		{
			get
			{
				if(SelectedClient != null)
				{
					return true;
				}
				return false;
			}
		}
		public void MakeOrder()
		{

		}

		public void LoadClients()
		{
			ClientsData data = new ClientsData();
			var clients = data.GetClients();
			Clients = _mapper.Map<BindableCollection<ClientModel>>(clients);
		}
		public void Handle(OrderEventModel order)
		{
			LoadClients();
			ProductsForOrder = order.OrderProducts;
			modelSums = CountSums();
			SumOfNetPrices = "Сума цін купівлі: " + modelSums.NetPrice.ToString("c");
			SumOfSellPrices = "Сума цін продажу: " + modelSums.SellPrice.ToString("c");
			Profit = "Прибуток: " + (modelSums.SellPrice - modelSums.NetPrice).ToString("c");
		}
	}
}
