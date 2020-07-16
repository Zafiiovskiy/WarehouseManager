﻿using AutoMapper;
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
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Models;

namespace WMDesktopUI.ViewModels
{
    public class ClientsViewModel : Screen, IHandle<OpenClientsEvent>
    {
		IMapper _mapper;
		private IEventAggregator _events;
		public ClientsViewModel(IMapper mapper, IEventAggregator events)
		{
			_events = events;
			_events.Subscribe(this);
			_mapper = mapper;
			LoadClients();
		}
		private void LoadClients()
		{
			ClientsData clientsData = new ClientsData();
			var wareHouseList = clientsData.GetClients();
			var clients = _mapper.Map<List<ClientModel>>(wareHouseList);
			Clients = new BindableCollection<ClientModel>(clients);
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

		private BindableCollection<ClientModel> _clients = new BindableCollection<ClientModel>();

		public BindableCollection<ClientModel> Clients
		{
			get { return _clients; }
			set
			{
				_clients = value;
				NotifyOfPropertyChange(() => Clients);
			}
		}


		///Buttons

		public void RefreshView()
		{
			try
			{
				LoadClients();
				SearchBox = "";
				this.Refresh();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void SearchByName()
		{
			try
			{
				if (SelectedValue.Text == "за Номером")
				{
					var found = Clients.Where(x => x.PhoneNumber.ToString().Contains(SearchBox)).ToList();
					BindableCollection<ClientModel> result = new BindableCollection<ClientModel>();
					foreach (var item in Clients)
					{
						if (found.Contains(item))
						{
							result.Add(item);
						}
					}
					if (result.Count > 0)
					{
						Clients = result;
					}
					else
					{
						MessageBox.Show("Жодного результату за вашим запитом.");
					}
				}
				else if (SelectedValue.Text == "за Ім'ям")
				{
					var found = Clients.Where(x => !String.IsNullOrWhiteSpace(x.Name)).Where(x => x.Name.Contains(SearchBox)).ToList();
					BindableCollection<ClientModel> result = new BindableCollection<ClientModel>();
					foreach (var item in Clients)
					{
						if (found.Contains(item))
						{
							result.Add(item);
						}
					}
					Clients = result;
				}
				else if (SelectedValue.Text == "за Прізвищем")
				{
					var found = Clients.Where(x => !String.IsNullOrWhiteSpace(x.Surname)).Where(x => x.Surname.Contains(SearchBox)).ToList();
					BindableCollection<ClientModel> result = new BindableCollection<ClientModel>();
					foreach (var item in Clients)
					{
						if (found.Contains(item))
						{
							result.Add(item);
						}
					}
					if (result.Count > 0)
					{
						Clients = result;
					}
					else
					{
						MessageBox.Show("Жодного результату за вагим запитом.");
					}
				}
				else if (SelectedValue.Text == "за Адресою")
				{
					var found = Clients.Where(x => !String.IsNullOrWhiteSpace(x.Adress)).Where(x => x.Adress.Contains(SearchBox)).ToList();
					BindableCollection<ClientModel> result = new BindableCollection<ClientModel>();
					foreach (var item in Clients)
					{
						if (found.Contains(item))
						{
							result.Add(item);
						}
					}
					if (result.Count > 0)
					{
						Clients = result;
					}
					else
					{
						MessageBox.Show("Жодного результату за вагим запитом.");
					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		public void SaveChanges()
		{
			try
			{
				ClientsData data = new ClientsData();
				foreach (var item in Clients)
				{
					if (item.WasUpdated == true)
					{
						var clientToUpdate = _mapper.Map<CModel>(item);
						data.UpdateClient(clientToUpdate);
						item.WasUpdated = false;
					}
				}

				MessageBox.Show("Зміни успішно збережено");

			}
			catch
			{
				MessageBox.Show("Щось пішло не так із збереженням змін.");
			}
		}

		public void Handle(OpenClientsEvent message)
		{
			RefreshView();
		}
	}
}

