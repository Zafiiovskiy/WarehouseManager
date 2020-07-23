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
	class ToBuyDetailsViewModel : Screen, IHandle<ToBuyDetailsEventModel>
    {
		private IMapper _mapper;
		private IEventAggregator _events;
		/// <summary>
		/// Private backing fields
		/// </summary>
		private WareHouseProductModel modelSums;
		private DelegateCommandHelper _command;
		private ClientModel _selectedClient;
		private BindableCollection<ClientModel> _clients = new BindableCollection<ClientModel>();
		private BindableCollection<WareHouseProductModel> _productsForOrder = new BindableCollection<WareHouseProductModel>();
		private string _sumOfSellPrices;
		private string _sumOfNetPrices;
		private string _profit;

		/// <summary>
		/// Private fields
		/// </summary>
		public ToBuyDetailsViewModel(IEventAggregator events, IMapper mapper)
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
			_events = events;
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
		public BindableCollection<WareHouseProductModel> ProductsToBuy
		{
			get { return _productsForOrder; }
			set
			{
				_productsForOrder = value;
				NotifyOfPropertyChange(() => ProductsToBuy);
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
				ToBuysData toBuysData = new ToBuysData();
				var product = obj as WareHouseProductModel;
				var orders = toBuysData.GetToBuyByClientId(new
				{
					ClientId = SelectedClient.Id
				});
				orders.Where(x => x.ProductId == product.ProductId).ToList().ForEach(x => toBuysData.ReverseToBuyByProduct(x));
				BindableCollection<WareHouseProductModel> found = new BindableCollection<WareHouseProductModel>();
				foreach (var item in ProductsToBuy)
				{
					if (item.ProductId != product.ProductId)
					{
						found.Add(item);
					}
				}
				ProductsToBuy = found;
				if (ProductsToBuy.Count == 0)
				{
					this.TryClose();
					_events.PublishOnUIThread(new ToBuysAllProductsDeletedEventModel());
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

			var sumOfNetPrices = ProductsToBuy.Sum(x => x.NetPrice * x.QuantityInStock);
			var sumOfSellPrices = ProductsToBuy.Sum(x => x.SellPrice * x.QuantityInStock);
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
		public void SaveChanges()
		{
			ToBuysData toBuysData = new ToBuysData();
			int counter = 0;
			foreach (var item in ProductsToBuy)
			{
				if (InputHelper.isCorrectWareHouseProductToBuy(item))
				{
					OPostModel postModel = new OPostModel()
					{
						ProductId = item.ProductId,
						ProductQuantity = item.QuantityInStock,
						ProductNetPrice = item.NetPrice,
						ProductSellPrice = item.SellPrice,
						ClientId = SelectedClient.Id
					};
					toBuysData.UpdateToBuy(postModel);
					counter++;
				}
				else
				{
					MessageBox.Show(InputHelper.isWrongWareHouseProductMassageToBuy(item));
					break;
				}
			}
			if(counter == ProductsToBuy.Count)
			{
				MessageBox.Show("Замовлення успішно оновлено.");
			}
		}

		/// <summary>
		/// Event handlers
		/// </summary>
		public void Handle(ToBuyDetailsEventModel toBuys)
		{
			try
			{
				ProductsToBuy = toBuys.ToBuyProducts;
				SelectedClient = toBuys.Client;
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
	}
}

