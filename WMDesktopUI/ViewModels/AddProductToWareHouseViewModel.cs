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
		IMapper _mapper;
		ICommand _command;

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
				ProductsToAdd[index].Photo = getJPGFromImageControl(bitmap);
            }
		}
		private byte[] getJPGFromImageControl(BitmapImage imageC)
		{
			MemoryStream memStream = new MemoryStream();
			JpegBitmapEncoder encoder = new JpegBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(imageC));
			encoder.Save(memStream);
			return memStream.ToArray();
		}
		private bool isCorrect(WareHouseProductModel wareHouseProductModel)
		{
			if(String.IsNullOrEmpty(wareHouseProductModel.FactoryNumber))
			{
				MessageBox.Show($"{wareHouseProductModel.FactoryNumber} - заводський номер введено неправильно.\nМає містити 10 чисел! Не може бути пустим.");
				return false;
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.Name))
			{
				MessageBox.Show("Ви забули ввести назву продукту!");
				return false;
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.Set))
			{
				MessageBox.Show("Ви забули ввести сервіз продукту!");
				return false;
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.Type))
			{
				MessageBox.Show("Ви забули ввести тип продукту!");
				return false;
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.Set))
			{
				MessageBox.Show("Ви забули ввести сервіз продукту!");
				return false;
			}
			if (wareHouseProductModel.Photo == null)
			{
				MessageBox.Show("Ви забули вибрати фото продукту!");
				return false;
			}
			if (wareHouseProductModel.QuantityInStock < 0)
			{
				MessageBox.Show("Ви забули ввести кількість продукту!");
				return false;
			}
			if (wareHouseProductModel.NetPrice < 0)
			{
				MessageBox.Show("Ви забули ввести ціну купівлі продукту!");
				return false;
			}
			if (wareHouseProductModel.SellPrice <= 0)
			{
				MessageBox.Show("Ви забули ввести ціну продажу продукту!");
				return false;
			}
			return true; 
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
						output = isCorrect(item);
					}
				}
				return output;
			}
		}
		public void AddProducts()
		{
			if (CanAddProducts)
			{
				WareHouseData wareHouseData = new WareHouseData();
				var productsToAdd = new List<WareHouseProductModel>(ProductsToAdd);
				var products = _mapper.Map<List<WHPostProductModel>>(productsToAdd);
				products.ForEach(prod => wareHouseData.PostProduct(prod));
				ProductsToAdd.Clear();
				LoadPlaceholder();
				MessageBox.Show("Товар успішно додано.");
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
		public void CloseView()
		{
			this.TryClose();
		}
	}
}
