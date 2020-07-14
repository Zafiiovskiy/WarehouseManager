using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WMDesktopUI.Helpers;
using WMDesktopUI.Library.DataManaging.DataAccess;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Models;

namespace WMDesktopUI.ViewModels
{
    public class AddClientsViewModel: Screen
    {
		private BindableCollection<ClientModel> _clientsToAdd = new BindableCollection<ClientModel>();
		IMapper _mapper;
		

		public BindableCollection<ClientModel> ClientsToAdd
		{
			get { return _clientsToAdd; }
			set
			{
				_clientsToAdd = value;
				NotifyOfPropertyChange(() => ClientsToAdd);
				NotifyOfPropertyChange(() => CanAddClients);
			}
		}


		public AddClientsViewModel(IMapper mapper)
		{
			_mapper = mapper;
			LoadPlaceholder();
		}

		private void LoadPlaceholder()
		{
			ClientModel placeholder = new ClientModel()
			{
				PhoneNumber = "",
				Name = "",
				Surname = "",
				Adress = ""
			};
			ClientsToAdd.Add(placeholder);
		}
		
		
		private bool isCorrect(ClientModel clientModel)
		{
			if (String.IsNullOrEmpty(clientModel.PhoneNumber))
			{
				MessageBox.Show("Номер введено неправильно.");
				return false;
			}
			if (String.IsNullOrEmpty(clientModel.Name))
			{
				MessageBox.Show("Ви забули ввести ім'я.");
				return false;
			}
			if (String.IsNullOrEmpty(clientModel.Surname))
			{
				MessageBox.Show("Ви забули ввести прізвище.");
				return false;
			}
			if (String.IsNullOrEmpty(clientModel.Adress))
			{
				MessageBox.Show("Ви забули ввести адрес.");
				return false;
			}
			
			return true;
		}
		private bool CanAddClients
		{
			get
			{
				bool output = false;
				if (ClientsToAdd.Count > 0)
				{
					foreach (var item in ClientsToAdd)
					{
						output = isCorrect(item);
					}
				}
				return output;
			}
		}
		public void AddClients()
		{
			if (CanAddClients)
			{
				try
				{
					ClientsData clientsData = new ClientsData();
					var clientsToAdd = new List<ClientModel>(ClientsToAdd);
					var clients = _mapper.Map<List<CPostModel>>(clientsToAdd);
					clients.ForEach(client => clientsData.PostClient(client));
					ClientsToAdd.Clear();
					LoadPlaceholder();
					MessageBox.Show("Клієнтів успішно додано.");
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}
		public void AddRow()
		{
			LoadPlaceholder();
		}
		public void DeleteSelected()
		{
			var notDeleted = new BindableCollection<ClientModel>();
			foreach (var item in ClientsToAdd)
			{
				if (item.Selected != true)
				{
					notDeleted.Add(item);
				}
			}
			ClientsToAdd = notDeleted;
		}
	}
}
