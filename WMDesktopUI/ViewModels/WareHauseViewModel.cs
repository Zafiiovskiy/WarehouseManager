using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WMDesktopUI.Events;
using WMDesktopUI.Helpers;
using WMDesktopUI.Library.DataManaging.DataAccess;
using WMDesktopUI.Models;
using WMDesktopUI.Views;

namespace WMDesktopUI.ViewModels
{
	public class WareHauseViewModel : Screen, IHandle<OrderMadeEventModel>, IHandle<OpenWareHouseEvent>
	{
		IMapper _mapper;
		private IEventAggregator _events;
		private IWindowManager _windowManager;
		private MakeOrderViewModel _makeOrdersView;

		/// <summary>
		/// Private backing fields
		/// </summary>
		private WareHouseProductModel modelSums;
		private string _sumOfNetPrices;
		private string _sumOfSellPrices;
		private TextBlock _selectedValue;
		private string _searchBox;
		private BindableCollection<WareHouseProductModel> _wareHouseProducts = new BindableCollection<WareHouseProductModel>();


		public WareHauseViewModel(IMapper mapper, IEventAggregator events,MakeOrderViewModel makeOrderView,
			IWindowManager windowManager)
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
			_mapper = mapper;
			_events = events;
			_events.Subscribe(this);
			_windowManager = windowManager;
			_makeOrdersView = makeOrderView;
			modelSums = LoadProducts();
			SumOfNetPrices = "Сума цін купівлі: "+modelSums?.NetPrice.ToString("c");
			SumOfSellPrices = "Сума цін продажу: "+modelSums?.SellPrice.ToString("c");
		}

		/// <summary>
		/// Public fields
		/// </summary>
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
		public BindableCollection<WareHouseProductModel> WareHouseProducts
		{
			get { return _wareHouseProducts; }
			set
			{
				_wareHouseProducts = value;
				NotifyOfPropertyChange(() => WareHouseProducts);
				NotifyOfPropertyChange(() => SumOfNetPrices);
				NotifyOfPropertyChange(() => SumOfSellPrices);
			}
		}

		/// <summary>
		/// Methods
		/// </summary>
		private WareHouseProductModel LoadProducts()
		{
			try
			{
				WareHouseData wareHouseData = new WareHouseData();
				var wareHouseList = wareHouseData.GetProducts();
				var products = _mapper.Map<List<WareHouseProductModel>>(wareHouseList);
				WareHouseProducts = new BindableCollection<WareHouseProductModel>(products);
				var sumOfNetPrices = wareHouseList.Sum(x => x.NetPrice * x.QuantityInStock);
				var sumOfSellPrices = wareHouseList.Sum(x => x.SellPrice * x.QuantityInStock);
				WareHouseProductModel model = new WareHouseProductModel
				{
					SellPrice = sumOfSellPrices,
					NetPrice = sumOfNetPrices
				};
				return model;
			}
			catch (Exception ex)
			{

				MessageBox.Show("Message: \n" + ex.Message + '\n' +
						"StackTrase: \n" + ex.StackTrace + '\n' +
						"InnerException: \n" + ex.InnerException);
				return new WareHouseProductModel
				{
					SellPrice = 0,
					NetPrice = 0
				};
			}
		}
		public void RefreshView()
		{
			modelSums = LoadProducts();
			SumOfNetPrices = "Сума цін купівлі = " + modelSums.NetPrice.ToString("c");
			SumOfSellPrices = "Сума цін продажу = " + modelSums.SellPrice.ToString("c");
			this.Refresh();
		}
		public void SearchByName()
		{
			try
			{
				if (WareHouseProducts?.Count > 0)
				{
					if (SelectedValue?.Text == "за Заводським номером")
					{
						var found = WareHouseProducts.Where(x => !String.IsNullOrWhiteSpace(x.FactoryNumber)).Where(x => x.FactoryNumber.Contains(SearchBox)).ToList();
						BindableCollection<WareHouseProductModel> result = new BindableCollection<WareHouseProductModel>();
						foreach (var item in WareHouseProducts)
						{
							if (found.Contains(item))
							{
								result.Add(item);
							}
						}
						if (result.Count > 0)
						{
							WareHouseProducts = result;
						}
						else
						{
							MessageBox.Show("Жодного результату за вашим запитом.");
						}
					}
					else if (SelectedValue?.Text == "за Назвою")
					{
						var found = WareHouseProducts.Where(x => !String.IsNullOrWhiteSpace(x.Name)).Where(x => x.Name.Contains(SearchBox)).ToList();
						BindableCollection<WareHouseProductModel> result = new BindableCollection<WareHouseProductModel>();
						foreach (var item in WareHouseProducts)
						{
							if (found.Contains(item))
							{
								result.Add(item);
							}
						}
						WareHouseProducts = result;
					}
					else if (SelectedValue?.Text == "за Сервізом")
					{
						var found = WareHouseProducts.Where(x => !String.IsNullOrWhiteSpace(x.Set)).Where(x => x.Set.Contains(SearchBox)).ToList();
						BindableCollection<WareHouseProductModel> result = new BindableCollection<WareHouseProductModel>();
						foreach (var item in WareHouseProducts)
						{
							if (found.Contains(item))
							{
								result.Add(item);
							}
						}
						if (result.Count > 0)
						{
							WareHouseProducts = result;
						}
						else
						{
							MessageBox.Show("Жодного результату за вагим запитом.");
						}
					}
					else if (SelectedValue?.Text == "за Типом")
					{
						var found = WareHouseProducts.Where(x => !String.IsNullOrWhiteSpace(x.Type)).Where(x => x.Type.Contains(SearchBox)).ToList();
						BindableCollection<WareHouseProductModel> result = new BindableCollection<WareHouseProductModel>();
						foreach (var item in WareHouseProducts)
						{
							if (found.Contains(item))
							{
								result.Add(item);
							}
						}
						if (result.Count > 0)
						{
							WareHouseProducts = result;
						}
						else
						{
							MessageBox.Show("Жодного результату за вагим запитом.");
						}
					}
					else
					{
						MessageBox.Show("Оберіть параметер пошуку.");
					}
				}
				else
				{
					MessageBox.Show("Додайте товар, щоб шукати.");
				}
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
			try
			{
				WareHouseData data = new WareHouseData();
				foreach (var item in WareHouseProducts)
				{
					if (item.WasUpdated == true)
					{
						if (InputHelper.isCorrectWareHouseProduct(item))
						{
							var productToUpdate = _mapper.Map<WHProductModel>(item);
							data.UpdateProduct(productToUpdate);
							item.WasUpdated = false;
							RefreshView();
						}
						else
						{
							MessageBox.Show(InputHelper.isWrongWareHouseProductMassage(item));
							break;
						}
					}
				}

				MessageBox.Show("Зміни успішно збережено");

			}
			catch (Exception ex)
			{

				MessageBox.Show("Message: \n" + ex.Message + '\n' +
						"StackTrase: \n" + ex.StackTrace + '\n' +
						"InnerException: \n" + ex.InnerException);
			}
		}
		private BindableCollection<WareHouseProductModel> GetProductsToOrder()
		{
			BindableCollection<WareHouseProductModel> output = new BindableCollection<WareHouseProductModel>();
			foreach (var item in WareHouseProducts)
			{
				if (item.Selected)
				{
					output.Add(item);
				}
			}
			if (output.Count > 0)
			{
				return output;
			}
			else
			{
				return null;
			}
		}
		public async Task MakeOrder()
		{
			try
			{
				if (GetProductsToOrder() != null)
				{
					_windowManager.ShowWindow(_makeOrdersView);
					await _events.PublishOnUIThreadAsync(new OrderEventModel(GetProductsToOrder()));
				}
				else
				{
					MessageBox.Show("Потрібно обрати товар,  щоб зробити замовлення.");
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
		public void Handle(OrderMadeEventModel message)
		{
			RefreshView();
		}
		public void Handle(OpenWareHouseEvent message)
		{
			RefreshView();
		}
	}
}
