using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
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
		private IMapper _mapper;
		private IEventAggregator _events;
		private IWareHouseData _wareHouseData;
		private IOrdersData _ordersData;
		/// <summary>
		/// Private backing fields
		/// </summary>
		private WareHouseProductModel modelSums;
		private DelegateCommandHelper _command;
		private ClientModel _selectedClient;
		private BindableCollection<ClientModel> _clients = new BindableCollection<ClientModel>();
		private BindableCollection<DetailsOrderProductModel> _productsForOrder = new BindableCollection<DetailsOrderProductModel>();
		private string _sumOfSellPrices;
		private string _sumOfNetPrices;
		private string _profit;

		/// <summary>
		/// Private fields
		/// </summary>
		public OrderDetailsViewModel(IEventAggregator events, IMapper mapper, IWareHouseData wareHouseData, IOrdersData ordersData)
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
			_events = events;
			_wareHouseData = wareHouseData;
			_ordersData = ordersData;
			_mapper = mapper;
			_events.Subscribe(this);
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
		public BindableCollection<DetailsOrderProductModel> ProductsForOrder
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

		/// <summary>
		/// Commands
		/// </summary>
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
				var product = obj as DetailsOrderProductModel;
				var orders = _ordersData.GetOrderByClientId(new
				{
					ClientId = SelectedClient.Id
				});
				orders.Where(x => x.ProductId == product.ProductId).ToList().ForEach(x => _ordersData.ReverseOrderByProduct(x));
				BindableCollection<DetailsOrderProductModel> found = new BindableCollection<DetailsOrderProductModel>();
			
				foreach (var item in ProductsForOrder)
				{
					if (item.ProductId != product.ProductId)
					{
						found.Add(item);
					}
				}
				ProductsForOrder = found;
				if (ProductsForOrder.Count == 0)
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
		public void RefreshSums()
		{
			try
			{
				modelSums = CountSums();
				SumOfNetPrices = "Сума цін купівлі: " + modelSums.NetPrice.ToString("c");
				SumOfSellPrices = "Сума цін продажу: " + modelSums.SellPrice.ToString("c");
				Profit = "Прибуток: " + (modelSums.SellPrice - modelSums.NetPrice).ToString("c");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Message: \n" + ex.Message + '\n' +
						"StackTrase: \n" + ex.StackTrace + '\n' +
						"InnerException: \n" + ex.InnerException);
			}
		}
		private BindableCollection<DetailsOrderProductModel> LoadProducts(List<OReverseModel> orders)
		{
			List<WHProductModel> _products = new List<WHProductModel>();

			orders.ForEach(x => _products.Add(_wareHouseData.GetProductById(new
			{
				x.ProductId
			})));
			var products = _mapper.Map<List<DetailsOrderProductModel>>(_products);
			products.ForEach(x => x.CurrentQuantityInStock = x.QuantityInStock);
			products.ForEach(x => x.MaxQuantity += x.QuantityInStock);
			foreach (var item in orders)
			{
				products.Where(x => x.ProductId == item.ProductId).ToList().ForEach(x => x.QuantityInStock = item.ProductQuantity);
				products.Where(x => x.ProductId == item.ProductId).ToList().ForEach(x => x.MaxQuantity += x.QuantityInStock);
			}
			return new BindableCollection<DetailsOrderProductModel>(products);
		}
		private List<OReverseModel> LoadOrders()
		{
			var orders = _ordersData.GetOrderByClientId(new { ClientId = SelectedClient.Id });


			return orders;
		}
		
		private void LoadOrder(OrderDetailsEventModel order)
		{
			SelectedClient = order.Client;
			var orders = LoadOrders();
			ProductsForOrder = LoadProducts(orders);
		}
		public void SaveChanges()
		{
			int counter = 0;
			foreach (var product in ProductsForOrder)
			{
				if (0 < product.QuantityInStock && product.QuantityInStock <= product.MaxQuantity)
				{
					if (InputHelper.isCorrectWareHouseProductInOrderDetails(product))
					{
						OUpdateModel updateModel = new OUpdateModel()
						{
							ProductId = product.ProductId,
							OrderQuantity = product.QuantityInStock,
							WareHouseQuantity = product.MaxQuantity - product.QuantityInStock,
							ProductNetPrice = product.NetPrice,
							ProductSellPrice = product.SellPrice,
							ClientId = SelectedClient.Id
						};
						_ordersData.UpdateOrder(updateModel);
						counter++;
					}
					else
					{
						MessageBox.Show(InputHelper.isWrongWareHouseProductMassageInOrderDetails(product));
						this.TryClose();
						break;
					}
				}
				else
				{
					MessageBox.Show($"На складі є {product.MaxQuantity} товару: {product.Name}.Введіть інше число або поповніть склад.");
					this.TryClose();
					break;
				}
			}
			if (counter == ProductsForOrder.Count)
			{
				MessageBox.Show("Покупку успішно оновлено.");
				this.TryClose();
			}
		}



		/// <summary>
		/// Event handlers
		/// </summary>
		public void Handle(OrderDetailsEventModel order)
		{
			try
			{
				LoadOrder(order);
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
