using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;
using WMDesktopUI.Helpers;
using WMDesktopUI.Library.DataManaging.DataAccess;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Models;

namespace WMDesktopUI.ViewModels
{
	public class AddClientsViewModel: Screen
    {
		private BindableCollection<ClientModel> _clientsToAdd = new BindableCollection<ClientModel>();
		private IMapper _mapper;
		private string WrongClientMessage { get; set; }
		private bool CanAddClients
		{
			get
			{
				if (ClientsToAdd.Count > 0)
				{
					foreach (var item in ClientsToAdd)
					{
						if (InputHelper.isCorrectClient(item) == false)
						{
							WrongClientMessage = InputHelper.isWrongClientMessage(item);
							return false;
						}
					}
				}
				return true;
			}
		}
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
					MessageBox.Show("Message: \n" + ex.Message + '\n' +
						"StackTrase: \n" + ex.StackTrace + '\n' +
						"InnerException: \n" + ex.InnerException);
				}
			}
			else
			{
				MessageBox.Show(WrongClientMessage);
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
