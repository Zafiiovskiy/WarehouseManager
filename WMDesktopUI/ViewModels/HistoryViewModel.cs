using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WMDesktopUI.Events;
using WMDesktopUI.Library.DataManaging.DataAccess;
using WMDesktopUI.Models;

namespace WMDesktopUI.ViewModels
{
    public class HistoryViewModel: Screen, IHandle<OpenHistoryEvent>
    {
		private IMapper _mapper;
		private IClientsData _clientsData;
		private BindableCollection<HistoryModel> _historyModels;
		private bool _isOpenPopup;
		private string _clientName;
		private string _clientSurname;
		private string _clientNumber;
		private string _productName;
		private string _productFactoryNumber;
		private string _productSet;
		private decimal _sumOfNetPrices;
		private decimal _sumOfSellPrices;
		private decimal _sumOfEarnings;

		public HistoryViewModel(IMapper mapper, IClientsData clientsData)
		{
			_mapper = mapper;
			_clientsData = clientsData;
			RefreshView();
		}
		public BindableCollection<HistoryModel> HistoryModels
		{
			get { return _historyModels; }
			set 
			{
				_historyModels = value;
				NotifyOfPropertyChange(() => HistoryModels);
			}
		}
		public bool isOpenPopup
		{
			get 
			{
				return _isOpenPopup; 
			}
			set 
			{ 
				_isOpenPopup = value;
				NotifyOfPropertyChange(() => isOpenPopup);
			}
		}
		public string ClientName { get { return _clientName; } set { _clientName = value; NotifyOfPropertyChange(() => ClientName); } }
		public string ClientSurname { get { return _clientSurname; }  set { _clientSurname = value; NotifyOfPropertyChange(() => ClientSurname); } }
		public string ClientNumber { get { return _clientNumber; } set { _clientNumber = value; NotifyOfPropertyChange(() => ClientNumber); } }
		public string ProductName { get { return _productName; } set { _productName = value; NotifyOfPropertyChange(() => ProductName); } }
		public string ProductFactoryNumber { get { return _productFactoryNumber; } set { _productFactoryNumber = value; NotifyOfPropertyChange(() => ProductFactoryNumber); } }
		public string ProductSet { get { return _productSet; } set { _productSet = value; NotifyOfPropertyChange(() => ProductSet); } }
		public decimal SumOfNetPrices { get { return _sumOfNetPrices; } set { _sumOfNetPrices = value; NotifyOfPropertyChange(() => SumOfNetPrices); } }
		public decimal SumOfSellPrices { get { return _sumOfSellPrices; } set { _sumOfSellPrices = value; NotifyOfPropertyChange(() => SumOfSellPrices); } }
		public decimal SumOfEarnings { get { return _sumOfEarnings; } set { _sumOfEarnings = value; NotifyOfPropertyChange(() => SumOfEarnings); } }


		public void Search()
		{
			if (SearchParametersEmpty == false)
			{
				FinishSearch();
			}
			else
			{
				isOpenPopup = true;
			}
		}
		private bool SearchParametersEmpty
		{
			get
			{
				if(String.IsNullOrEmpty(ClientName) &&
					String.IsNullOrEmpty(ClientNumber) &&
					String.IsNullOrEmpty(ClientSurname) &&
					String.IsNullOrEmpty(ProductFactoryNumber) &&
					String.IsNullOrEmpty(ProductName) &&
					String.IsNullOrEmpty(ProductSet))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		private void EmptySearchParameters()
		{
			ClientName = ""; ClientNumber = ""; ClientSurname = "";
			ProductFactoryNumber = ""; ProductName = ""; ProductSet = "";
		}
		private void FinishSearch()
		{
			if (String.IsNullOrEmpty(ClientNumber) == false)
			{
				HistoryModels = new BindableCollection<HistoryModel>(HistoryModels.ToList().Where(x => x.Client.PhoneNumber.Contains(ClientNumber)));
			}
			if (String.IsNullOrEmpty(ClientName) == false)
			{
				HistoryModels = new BindableCollection<HistoryModel>(HistoryModels.ToList().Where(x => x.Client.Name.Contains(ClientName))); 
			}
			if (String.IsNullOrEmpty(ClientSurname) == false)
			{
				HistoryModels = new BindableCollection<HistoryModel>(HistoryModels.ToList().Where(x => x.Client.Surname.Contains(ClientSurname)));
			}
			if (String.IsNullOrEmpty(ProductFactoryNumber) == false)
			{
				HistoryModels = new BindableCollection<HistoryModel>(HistoryModels.ToList().Where(x => x.FactoryNumber.Contains(ProductFactoryNumber)));
			}
			if (String.IsNullOrEmpty(ProductName) == false)
			{
				HistoryModels = new BindableCollection<HistoryModel>(HistoryModels.ToList().Where(x => x.Name.Contains(ProductName)));
			}
			if (String.IsNullOrEmpty(ProductSet) == false)
			{
				HistoryModels = new BindableCollection<HistoryModel>(HistoryModels.ToList().Where(x => x.Set.Contains(ProductSet)));
			}
			isOpenPopup = false;
			EmptySearchParameters();
			if (HistoryModels.Count == 0)
			{
				RefreshView();
				MessageBox.Show("Жодного результату за вашим пошуком");
			}
		}
		public void RefreshView()
		{
			isOpenPopup = false;
			LoadModels();
			EmptySearchParameters();
			CountSums();
		}
		private void LoadModels()
		{
			HistoryData _historyData = new HistoryData();
			var historyModels = _mapper.Map<List<HistoryModel>>(_historyData.GetHistory());
			historyModels.ForEach(x => x.OrderDateTime = x.OrderDateTime.ToLocalTime());
			historyModels.ForEach(x => x.FinishDateTime = x.FinishDateTime.ToLocalTime());
			historyModels.ForEach(x => x.Client = _mapper.Map<ClientModel>(_clientsData.GetClientsById(new {x.ClientId })));
			HistoryModels = new BindableCollection<HistoryModel>(historyModels);
		}
		private void CountSums()
		{
			SumOfEarnings = 0;
			SumOfNetPrices = 0;
			SumOfSellPrices = 0;
			HistoryModels.ToList().ForEach(x => SumOfSellPrices += x.ProductSellPrice * x.ProductQuantity);
			HistoryModels.ToList().ForEach(x => SumOfNetPrices += x.ProductNetPrice * x.ProductQuantity);
			SumOfEarnings = SumOfSellPrices - SumOfNetPrices;
		}


		public void Handle(OpenHistoryEvent message)
		{
			RefreshView();
		}
	}
}
