using AutoMapper;
using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WMDesktopUI.Events;
using WMDesktopUI.Helpers;
using WMDesktopUI.Library.DataManaging.DataAccess;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Models;

namespace WMDesktopUI.ViewModels
{
	public class AddProductToWareHouseViewModel : Screen, IHandle<MakeToBuyMadeEventModel>
    {
		private BindableCollection<WareHouseProductModel> _productsToAdd = new BindableCollection<WareHouseProductModel>();
		private IMapper _mapper;
		private ICommand _command;
		private IEventAggregator _events;
		private IWindowManager _windowManager;
		private IWareHouseData _wareHouseData;
		private MakeToBuyViewModel _makeToBuyViewModel;

		public AddProductToWareHouseViewModel(IMapper mapper, IWareHouseData wareHouseData, IEventAggregator events,
			IWindowManager windowManager, MakeToBuyViewModel makeToBuyViewModel)
		{
			_mapper = mapper;
			_events = events;
			_events.Subscribe(this);
			_windowManager = windowManager;
			_makeToBuyViewModel = makeToBuyViewModel;
			_wareHouseData = wareHouseData;
			LoadPlaceholder();
		}

		private bool CanExecute(object parameter)
		{
			return true;
		}
		private bool CanAddProducts
		{
			get
			{
				if (ProductsToAdd.Count > 0)
				{
					foreach (var item in ProductsToAdd)
					{
						if (InputHelper.isCorrectWareHouseProduct(item) == false)
						{
							MessageBox.Show(InputHelper.isWrongWareHouseProductMassage(item));
							return false;
						}
					}
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		private bool CanMakeToBuy
		{
			get
			{
				if (ProductsToAdd.Count > 0)
				{
					foreach (var item in ProductsToAdd)
					{
						if (InputHelper.isCorrectWareHouseProductToBuy(item) == false)
						{
							MessageBox.Show(InputHelper.isWrongWareHouseProductMassageToBuy(item));
							return false;
						}
					}
					return true;
				}
				else
				{
					return false;
				}
			}
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

		/// <summary>
		/// Methods
		/// </summary>
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
		

		public async Task MakeToBuy()
		{
			if (CanMakeToBuy)
			{
				ToBuysProductsData toBuysProductsData = new ToBuysProductsData();
				var productsToAdd = new List<WareHouseProductModel>(ProductsToAdd);
				var products = _mapper.Map<List<WHPostProductModel>>(productsToAdd);
				List<WHProductModel> productsWithID = new List<WHProductModel>();
				products.ForEach(prod => productsWithID.Add(toBuysProductsData.PostProduct(prod)));
				BindableCollection<WareHouseProductModel> productsToBuy = new BindableCollection<WareHouseProductModel>(_mapper.Map<List<WareHouseProductModel>>(productsWithID));
				_windowManager.ShowWindow(_makeToBuyViewModel);
				await _events.PublishOnUIThreadAsync(new MakeToBuyEventModel(productsToBuy));
			}
		}
		public void AddProducts()
		{
			if (CanAddProducts)
			{
				try
				{
					var productsToAdd = new List<WareHouseProductModel>(ProductsToAdd);
					var products = _mapper.Map<List<WHPostProductModel>>(productsToAdd);
					products.ForEach(prod => _wareHouseData.PostProduct(prod));
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

		public void Handle(MakeToBuyMadeEventModel message)
		{
			ProductsToAdd.Clear();
			LoadPlaceholder();
		}
	}
}
