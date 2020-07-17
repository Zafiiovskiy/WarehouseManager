using AutoMapper;
using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WMDesktopUI.Helpers;
using WMDesktopUI.Library.DataManaging.DataAccess;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Models;

namespace WMDesktopUI.ViewModels
{
	public class AddProductToWareHouseViewModel : Screen
    {
		private BindableCollection<WareHouseProductModel> _productsToAdd = new BindableCollection<WareHouseProductModel>();
		private IMapper _mapper;
		private ICommand _command;
		public ICommand UploadPhotoCommand
		{
			get
			{
				if (_command == null)
				{
					_command = new DelegateCommandHelper(CanExecute, UploadPhoto);
				}
				return _command;
			}
		}
		private bool CanExecute(object parameter)
		{
			return true;
		}
		public BindableCollection<WareHouseProductModel> ProductsToAdd
		{
			get { return _productsToAdd; }
			set
			{
				_productsToAdd = value;
				NotifyOfPropertyChange(() => ProductsToAdd);
				NotifyOfPropertyChange(() => CanAddProducts);
			}
		}


		public AddProductToWareHouseViewModel(IMapper mapper)
		{
			_mapper = mapper;
			LoadPlaceholder();
		}


		private void LoadPlaceholder()
		{
			WareHouseProductModel placeholder = new WareHouseProductModel()
			{
				FactoryNumber = "",
				Name = "",
				Set = "",
				Type = "",
				QuantityInStock = 0,
				ProductDescription = "",
				NetPrice = 0,
				SellPrice = 0
			};
			ProductsToAdd.Add(placeholder);
		}
		private void UploadPhoto(object parameter)
		{
			var index = ProductsToAdd.IndexOf(parameter as WareHouseProductModel);
			OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "Image Files (*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg";
            if (op.ShowDialog() == true)
            {
                var bitmap = new BitmapImage(new Uri(op.FileName));
				ProductsToAdd[index].Photo = ConvertHelper.ImageToByteArray(bitmap);
            }
		}
		
		private bool CanAddProducts
		{
			get
			{
				bool output = false;
				if(ProductsToAdd.Count > 0)
				{
					foreach (var item in ProductsToAdd)
					{
						output = InputHelper.isCorrectWareHouseProduct(item);
					}
				}
				return output;
			}
		}
		public void AddProducts()
		{
			if (CanAddProducts)
			{
				try
				{
					WareHouseData wareHouseData = new WareHouseData();
					var productsToAdd = new List<WareHouseProductModel>(ProductsToAdd);
					var products = _mapper.Map<List<WHPostProductModel>>(productsToAdd);
					products.ForEach(prod => wareHouseData.PostProduct(prod));
					ProductsToAdd.Clear();
					LoadPlaceholder();
					MessageBox.Show("Товар успішно додано.");
				}
				catch (Exception ex)
				{
					MessageBox.Show("Message: \n" + ex.Message + '\n' +
						"StackTrase: \n" + ex.StackTrace + '\n' +
						"InnerException: \n" + ex.InnerException);
				}
			}
		}
		public void AddRow()
		{
			LoadPlaceholder();
		}
		public void DeleteSelected()
		{
			var notDeleted = new BindableCollection<WareHouseProductModel>();
			foreach (var item in ProductsToAdd)
			{
				if(item.Selected != true)
				{
					notDeleted.Add(item);
				}
			}
			ProductsToAdd = notDeleted;
		}
	}
}
