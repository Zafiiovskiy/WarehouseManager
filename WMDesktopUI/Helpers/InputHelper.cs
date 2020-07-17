using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMDesktopUI.Models;

namespace WMDesktopUI.Helpers
{
    public static class InputHelper
    {
		private static bool IsDigitsOnlyAndPlus(string str)
		{
			foreach (char c in str)
			{
				if (c < '0' || c > '9' && c != '+')
				{
					return false;
				}
			}
			return true;
		}
		private static bool IsDigitsOnly(string str)
		{
			foreach (char c in str)
			{
				if (c < '0' || c > '9')
				{
					return false;
				}
			}

			return true;
		}
		public static bool isCorrectClient(ClientModel clientModel)
		{
			if (String.IsNullOrEmpty(clientModel.PhoneNumber) ||
				IsDigitsOnlyAndPlus(clientModel.PhoneNumber) == false || 
				clientModel.PhoneNumber.Length != 10)
			{
				return false;
			}
			if (String.IsNullOrEmpty(clientModel.Name) ||
				clientModel.Name.All(Char.IsLetter) == false ||
				char.IsUpper(clientModel.Name[0]) == false)
			{
				return false;
			}
			if (String.IsNullOrEmpty(clientModel.Surname) ||
				clientModel.Surname.All(Char.IsLetter) == false ||
				char.IsUpper(clientModel.Surname[0]) == false)
			{
				return false;
			}
			if (String.IsNullOrEmpty(clientModel.Adress))
			{
				return false;
			}

			return true;
		}
		public static string isWrongClientMessage(ClientModel clientModel){
			string message = "Неправильно введено: \n";
			if (String.IsNullOrEmpty(clientModel.PhoneNumber) ||
				IsDigitsOnlyAndPlus(clientModel.PhoneNumber) == false ||
				clientModel.PhoneNumber.Length != 10)
			{
				message += $"Номер: {clientModel.PhoneNumber}.\n";
			}
			if (String.IsNullOrEmpty(clientModel.Name) ||
				clientModel.Name.All(Char.IsLetter) == false ||
				char.IsUpper(clientModel.Name[0]) == false)
			{
				message += $"Ім'я: {clientModel.Name}.\n";
			}
			if (String.IsNullOrEmpty(clientModel.Surname) ||
				clientModel.Surname.All(Char.IsLetter) == false ||
				char.IsUpper(clientModel.Surname[0]) == false)
			{
				message += $"Прізвище: {clientModel.Surname}.\n";
			}
			if (String.IsNullOrEmpty(clientModel.Adress))
			{
				message += $"Ви забули ввести адресу для {clientModel.Name}.\n";
			}

			return message;
		}
		public static bool isCorrectWareHouseProduct(WareHouseProductModel wareHouseProductModel)
		{
			if (String.IsNullOrEmpty(wareHouseProductModel.FactoryNumber) || 
				IsDigitsOnly(wareHouseProductModel.FactoryNumber) == false ||
				wareHouseProductModel.FactoryNumber.Length != 10)
			{
				return false;
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.Name))
			{
				return false;
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.Set))
			{
				return false;
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.Type))
			{
				return false;
			}
			if (wareHouseProductModel.Photo == null)
			{
				return false;
			}
			if (wareHouseProductModel.QuantityInStock < 0)
			{
				return false;
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.ProductDescription))
			{
				return false;
			}
			if (wareHouseProductModel.NetPrice <= 0)
			{
				return false;
			}
			if (wareHouseProductModel.SellPrice <= 0)
			{
				return false;
			}
			return true;
		}
	}
}
