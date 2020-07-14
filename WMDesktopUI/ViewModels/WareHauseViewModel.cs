using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using WMDesktopUI.Events;
using WMDesktopUI.Library.DataManaging.DataAccess;
using WMDesktopUI.Models;
using WMDesktopUI.Views;

namespace WMDesktopUI.ViewModels
{
	public class WareHauseViewModel : Screen
	{
		IMapper _mapper;
		private WareHouseProductModel modelSums;
		private IEventAggregator _events;
		private IWindowManager _windowManager;
		private MakeOrderViewModel _makeOrdersView;
		public WareHauseViewModel(IMapper mapper, IEventAggregator events,MakeOrderViewModel makeOrderView,
			IWindowManager windowManager)
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
			_mapper = mapper;
			_events = events;
			_windowManager = windowManager;
			_makeOrdersView = makeOrderView;
			modelSums = LoadProducts();
			SumOfNetPrices = "Сума цін купівлі: "+modelSums.NetPrice.ToString("c");
			SumOfSellPrices = "Сума цін продажу: "+modelSums.SellPrice.ToString("c");
		}
		private WareHouseProductModel LoadProducts()
		{
			WareHouseData wareHouseData = new WareHouseData();
			var wareHouseList = wareHouseData.GetProducts();
			var products = _mapper.Map<List<WareHouseProductModel>>(wareHouseList);
			WareHouseProducts = new BindableCollection<WareHouseProductModel>(products);
			var sumOfNetPrices = wareHouseList.Sum(x => x.NetPrice);
			var sumOfSellPrices = wareHouseList.Sum(x => x.SellPrice);
			WareHouseProductModel model = new WareHouseProductModel
			{
				SellPrice = sumOfSellPrices,
				NetPrice = sumOfNetPrices
			};
			return model;
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

		private BindableCollection<WareHouseProductModel> _wareHouseProducts = new BindableCollection<WareHouseProductModel>();

		public BindableCollection<WareHouseProductModel> WareHouseProducts
		{
			get { return _wareHouseProducts; }
			set
			{
				_wareHouseProducts = value;
				NotifyOfPropertyChange(() => WareHouseProducts);
			}
		}
		

		///Buttons

		public void RefreshView()
		{
			modelSums = LoadProducts();
			SumOfNetPrices = "Сума цін купівлі = " + modelSums.NetPrice.ToString("c");
			SumOfSellPrices = "Сума цін продажу = " + modelSums.SellPrice.ToString("c");
			this.Refresh();
		}
		
		public void SearchByName()
		{
			if (SelectedValue.Text == "за Заводським номером")
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
			else if (SelectedValue.Text == "за Назвою")
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
			else if (SelectedValue.Text == "за Сервізом")
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
			else if(SelectedValue.Text == "за Типом")
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
						var productToUpdate = _mapper.Map<WHProductModel>(item);
						data.UpdateProduct(productToUpdate);
						item.WasUpdated = false;
						RefreshView();
						MessageBox.Show("Зміни успішно збережено");
					}
				}
				
			}
			catch
			{
				MessageBox.Show("Щось пішло не так із збереженням змін.");
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
			return output;
		}
		public void MakeOrder()
		{
			try
			{
				_windowManager.ShowWindow(_makeOrdersView);
				_events.PublishOnUIThread(new OrderEventModel(GetProductsToOrder()));
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
