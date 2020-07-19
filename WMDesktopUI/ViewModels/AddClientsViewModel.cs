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
		private IMapper _mapper;
		private IClientsData _clientsData;
		/// <summary>
		/// Private backing fields
		/// </summary>
		private BindableCollection<ClientModel> _clientsToAdd = new BindableCollection<ClientModel>();
		

		/// <summary>
		/// Private fields
		/// </summary>
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
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		public AddClientsViewModel(IMapper mapper, IClientsData clientsData)
		{
			_mapper = mapper;
			_clientsData = clientsData;
			LoadPlaceholder();
		}

		/// <summary>
		/// Public fields
		/// </summary>
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

		/// <summary>
		/// Methods
		/// </summary>
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
					var clientsToAdd = new List<ClientModel>(ClientsToAdd);
					var clients = _mapper.Map<List<CPostModel>>(clientsToAdd);
					clients.ForEach(client => _clientsData.PostClient(client));
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
