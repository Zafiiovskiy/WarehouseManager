using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WMDesktopUI.Library.DataManaging.DataAccess;
using WMDesktopUI.Models;


namespace WMDesktopUI.ViewModels
{
	public class WareHauseViewModel : Conductor<object>
	{
		IMapper _mapper;
		public WareHauseViewModel(IMapper mapper)
		{
			_mapper = mapper;
			LoadProducts();
		}
		private void LoadProducts()
		{
			WareHouseData wareHouseData = new WareHouseData();
			var wareHouseList = wareHouseData.GetProducts();
			var products = _mapper.Map<List<WareHouseProductModel>>(wareHouseList);
			WareHouseProducts = new BindableCollection<WareHouseProductModel>(products);
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
				NotifyOfPropertyChange(() => CanRefreshView);
			}
		}
		

		///Buttons

		public void RefreshView()
		{
			LoadProducts();
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
						MessageBox.Show("Зміни успішно збережено");
					}
				}

				NotifyOfPropertyChange(() => CanRefreshView);
			}
			catch
			{
				MessageBox.Show("Щось пішло не так із збереженням змін.");
			}
		}
	}
}
