using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using WMDesktopUI.Events;
using WMDesktopUI.Helpers;
using WMDesktopUI.Library.DataManaging.DataAccess;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Models;

namespace WMDesktopUI.ViewModels
{
	public class OrderDetailsViewModel : Screen, IHandle<OrderDetailsEventModel>
    {
		IMapper _mapper;
		private WareHouseProductModel modelSums;
		private IEventAggregator _events;
		public OrderDetailsViewModel(IEventAggregator events, IMapper mapper)
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
			_events = events;
			_mapper = mapper;
			_events.Subscribe(this);
		}

		public ICommand ReverseOrder
		{
			get
			{

				_command = new DelegateCommandHelper(CanExecute, ExecuteReverseOrder);

				return _command;
			}
		}
		private void ExecuteReverseOrder(object obj)
		{
			try
			{
				var product = obj as WareHouseProductModel;
				OrdersData data = new OrdersData();
				var orders = data.GetOrderByClientId(new
				{
					ClientId = SelectedClient.Id
				});
				orders.Where(x => x.ProductId == product.ProductId).ToList().ForEach(x => data.ReverseOrderByProduct(x));
				BindableCollection<WareHouseProductModel> found = new BindableCollection<WareHouseProductModel>();
				foreach (var item in ProductsForOrder)
				{
					if(item.ProductId != product.ProductId)
					{
						found.Add(item);
					}
				}
				ProductsForOrder = found;
				if(ProductsForOrder.Count == 0)
				{
					this.TryClose();
					_events.PublishOnUIThread(new OrderAllProductsDeletedEventModel());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Message: \n" + ex.Message + '\n' +
						"StackTrase: \n" + ex.StackTrace + '\n' +
						"InnerException: \n" + ex.InnerException);
			}
		}


		private bool CanExecute(object obj)
		{
			return true;
		}

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

		private ClientModel _selectedClient;

		public ClientModel SelectedClient
		{
			get { return _selectedClient; }
			set
			{
				_selectedClient = value;
				NotifyOfPropertyChange(() => SelectedClient);
				NotifyOfPropertyChange(() => SelectedClientString);
			}
		}

		public string SelectedClientString
		{
			get
			{
				if (SelectedClient == null)
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
				NotifyOfPropertyChange(() => SumOfNetPrices);
				NotifyOfPropertyChange(() => SumOfSellPrices);
			}
		}


		public void RefreshSums()
		{
			modelSums = CountSums();
			SumOfNetPrices = "Сума цін купівлі: " + modelSums.NetPrice.ToString("c");
			SumOfSellPrices = "Сума цін продажу: " + modelSums.SellPrice.ToString("c");
			Profit = "Прибуток: " + (modelSums.SellPrice - modelSums.NetPrice).ToString("c");
		}

		public void LoadMaxQuantities()
		{
			foreach (var item in ProductsForOrder)
			{
				MaxQuantityInStock.Add(item.QuantityInStock);
				item.QuantityInStock = 0;
			}
		}
		private void LoadOrderQuantities(List<OReverseModel> orders)
		{
			try
			{
				for (int i = 0; i < orders.Count; i++)
				{
					if (orders[i].ProductId == ProductsForOrder[i].ProductId)
					{
						ProductsForOrder[i].QuantityInStock = orders[i].ProductQuantity;
					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private List<int> MaxQuantityInStock = new List<int>();
		private DelegateCommandHelper _command;

		public void Handle(OrderDetailsEventModel order)
		{
			try
			{
				ProductsForOrder = order.OrderProducts;
				SelectedClient = order.Client;
				LoadMaxQuantities();
				LoadOrderQuantities(order.Orders);
				modelSums = CountSums();
				SumOfNetPrices = "Сума цін купівлі: " + modelSums.NetPrice.ToString("c");
				SumOfSellPrices = "Сума цін продажу: " + modelSums.SellPrice.ToString("c");
				Profit = "Прибуток: " + (modelSums.SellPrice - modelSums.NetPrice).ToString("c");
			}
			catch(Exception ex)
			{
				MessageBox.Show("Message: \n" + ex.Message + '\n' +
						"StackTrase: \n" + ex.StackTrace + '\n' +
						"InnerException: \n" + ex.InnerException);
			}
		}
	}
}
