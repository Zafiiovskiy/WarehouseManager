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
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Models;

namespace WMDesktopUI.ViewModels
{
	public class MakeOrderViewModel: Screen, IHandle<OrderEventModel>
    {
		private IMapper _mapper;
		private IEventAggregator _events;
		/// <summary>
		/// Private backing fields
		/// </summary>
		private WareHouseProductModel modelSums;
		private ClientModel _selectedClient;
		private string _searchBox;
		private string _profit;
		private string _sumOfNetPrices;
		private string _sumOfSellPrices;
		private BindableCollection<ClientModel> _clients = new BindableCollection<ClientModel>();
		private BindableCollection<WareHouseProductModel> _productsForOrder = new BindableCollection<WareHouseProductModel>();
		private TextBlock _selectedValue;

		/// <summary>
		/// Private fields
		/// </summary>
		private List<int> MaxQuantityInStock = new List<int>();

		public MakeOrderViewModel(IEventAggregator events,IMapper mapper)
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
			_events = events;
			_mapper = mapper;
			_events.Subscribe(this);
			RefreshSums();
		}


		/// <summary>
		/// Public fields
		/// </summary>
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
		public string Profit
		{
			get { return _profit; }
			set
			{
				_profit = value;
				NotifyOfPropertyChange(() => Profit);
			}
		}
		public string SumOfNetPrices
		{
			get { return _sumOfNetPrices; }
			set
			{
				_sumOfNetPrices = value;
				NotifyOfPropertyChange(() => SumOfNetPrices);
			}
		}
		public string SumOfSellPrices
		{
			get { return _sumOfSellPrices; }
			set
			{
				_sumOfSellPrices = value;
				NotifyOfPropertyChange(() => SumOfSellPrices);
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
		public BindableCollection<WareHouseProductModel> ProductsForOrder
		{
			get { return _productsForOrder; }
			set
			{
				_productsForOrder = value;
				NotifyOfPropertyChange(() => ProductsForOrder);
				NotifyOfPropertyChange(() => SumOfNetPrices);
				NotifyOfPropertyChange(() => SumOfSellPrices);
			}
		}
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


		/// <summary>
		/// Methods 
		/// </summary>
		private WareHouseProductModel CountSums()
		{

			var sumOfNetPrices = ProductsForOrder.Sum(x => x.NetPrice * x.QuantityInStock);
			var sumOfSellPrices = ProductsForOrder.Sum(x => x.SellPrice * x.QuantityInStock);
			WareHouseProductModel model = new WareHouseProductModel
			{
				SellPrice = sumOfSellPrices,
				NetPrice = sumOfNetPrices
			};
			return model;
		}
		public void SearchByName()
		{
			try
			{
				if (SearchBox != null)
				{
					if (Clients != null)
					{
						if (SelectedValue?.Text == "за Номером")
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
						else
						{
							MessageBox.Show("Виберіть параметер пошуку.");
						}
					}
					else
					{
						MessageBox.Show("Немає клієнтів. Внесіть клієнтів, щоб шукати.");
					}
				}
				else
				{
					MessageBox.Show("Введіть параметер пошуку, щоб шукати.");
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		public void RefreshSums()
		{
			modelSums = CountSums();
			SumOfNetPrices = "Сума цін купівлі: " + modelSums.NetPrice.ToString("c");
			SumOfSellPrices = "Сума цін продажу: " + modelSums.SellPrice.ToString("c");
			Profit = "Прибуток: " + (modelSums.SellPrice - modelSums.NetPrice).ToString("c");
		}
		public bool CanMakeOrder
		{
			get
			{
				bool result = true;
				if(SelectedClient != null)
				{
					result = true;
				}
				else
				{
					result = false;
				}
				return result;
			}
		}
		public void MakeOrder()
		{
			bool isOrdarable = true;
			for (int i = 0; i < ProductsForOrder.Count; i++)
			{
				if (MaxQuantityInStock[i] < ProductsForOrder[i].QuantityInStock || ProductsForOrder[i].QuantityInStock <= 0)
				{
					isOrdarable = false;
					MessageBox.Show($"Кількість товару '{ProductsForOrder[i].Name}' рівна нулю або перевищує кількість на складі.");
				}
			}
			if (isOrdarable)
			{
				try
				{
					OrdersData data = new OrdersData();
					List<OrderModel> orderModels = new List<OrderModel>();
					foreach (var item in ProductsForOrder)
					{
						OrderModel order = new OrderModel()
						{
							ProductId = item.ProductId,
							ProductQuantity = item.QuantityInStock,
							ProductNetPrice = item.NetPrice,
							ProductSellPrice = item.SellPrice,
							ClientId = SelectedClient.Id
						};
						orderModels.Add(order);
					}
					var orders = _mapper.Map<List<OPostModel>>(orderModels);
					foreach (var item in orders)
					{
						data.PostOrder(item);
					}
					MessageBox.Show("Покупку успішно додано.");
					_events.PublishOnUIThread(new OrderMadeEventModel());
					this.TryClose();
				}
				catch(Exception ex)
				{
					MessageBox.Show("Message: \n" + ex.Message + '\n' +
						"StackTrase: \n" + ex.StackTrace + '\n' +
						"InnerException: \n" + ex.InnerException);
				}
			}
		}
		public void LoadClients()
		{
			try
			{
				ClientsData data = new ClientsData();
				var clients = data.GetClients();
				Clients = _mapper.Map<BindableCollection<ClientModel>>(clients);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Message: \n" + ex.Message + '\n' +
						"StackTrase: \n" + ex.StackTrace + '\n' +
						"InnerException: \n" + ex.InnerException);
			}
		}
		public void LoadMaxQuantities()
		{
			foreach (var item in ProductsForOrder)
			{
				MaxQuantityInStock.Add(item.QuantityInStock);
				item.QuantityInStock = 0;
			}
		}

		/// <summary>
		/// Event handlers
		/// </summary>
		public void Handle(OrderEventModel order)
		{
			LoadClients();
			ProductsForOrder = order.OrderProducts;
			LoadMaxQuantities();
			modelSums = CountSums();
			SumOfNetPrices = "Сума цін купівлі: " + modelSums.NetPrice.ToString("c");
			SumOfSellPrices = "Сума цін продажу: " + modelSums.SellPrice.ToString("c");
			Profit = "Прибуток: " + (modelSums.SellPrice - modelSums.NetPrice).ToString("c");
		}
	}
}
