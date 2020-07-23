using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
			wareHouseProductModel.FactoryNumber = Regex.Replace(wareHouseProductModel.FactoryNumber, @"\s+", "");

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
		public static string isWrongWareHouseProductMassage(WareHouseProductModel wareHouseProductModel)
		{
			wareHouseProductModel.FactoryNumber = Regex.Replace(wareHouseProductModel.FactoryNumber, @"\s+", "");

			string message = "Неправильно введено: \n";
			if (String.IsNullOrEmpty(wareHouseProductModel.FactoryNumber) ||
				IsDigitsOnly(wareHouseProductModel.FactoryNumber) == false ||
				wareHouseProductModel.FactoryNumber.Length != 10)
			{
				message += $"Заводський номер: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.Name))
			{
				message += $"Ви забули ввести ім'я: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.Set))
			{
				message += $"Ви забули ввести сервіз: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.Type))
			{
				message += $"Ви забули ввести тип: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (wareHouseProductModel.Photo == null)
			{
				message += $"Ви забули вибрати фото: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (wareHouseProductModel.QuantityInStock < 0)
			{
				message += $"Неправильно введена кількість: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (wareHouseProductModel.NetPrice <= 0)
			{
				message += $"Неправильно введена ціна покупки: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (wareHouseProductModel.SellPrice <= 0)
			{
				message += $"Неправильно введена ціна продажу: {wareHouseProductModel.FactoryNumber}.\n";
			}
			return message;
		}
		public static bool isCorrectWareHouseProductToBuy(WareHouseProductModel wareHouseProductModel)
		{
			wareHouseProductModel.FactoryNumber = Regex.Replace(wareHouseProductModel.FactoryNumber, @"\s+", "");

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
			if (wareHouseProductModel.NetPrice < 0)
			{
				return false;
			}
			if (wareHouseProductModel.SellPrice < 0)
			{
				return false;
			}
			return true;
		}
		public static string isWrongWareHouseProductMassageToBuy(WareHouseProductModel wareHouseProductModel)
		{
			wareHouseProductModel.FactoryNumber = Regex.Replace(wareHouseProductModel.FactoryNumber, @"\s+", "");

			string message = "Неправильно введено: \n";
			if (String.IsNullOrEmpty(wareHouseProductModel.FactoryNumber) ||
				IsDigitsOnly(wareHouseProductModel.FactoryNumber) == false ||
				wareHouseProductModel.FactoryNumber.Length != 10)
			{
				message += $"Заводський номер: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.Name))
			{
				message += $"Ви забули ввести ім'я: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.Set))
			{
				message += $"Ви забули ввести сервіз: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.Type))
			{
				message += $"Ви забули ввести тип: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (wareHouseProductModel.Photo == null)
			{
				message += $"Ви забули вибрати фото: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (wareHouseProductModel.QuantityInStock < 0)
			{
				message += $"Неправильно введена кількість: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (wareHouseProductModel.NetPrice < 0)
			{
				message += $"Неправильно введена ціна покупки: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (wareHouseProductModel.SellPrice < 0)
			{
				message += $"Неправильно введена ціна продажу: {wareHouseProductModel.FactoryNumber}.\n";
			}
			return message;
		}
		public static bool isCorrectWareHouseProductInOrderDetails(DetailsOrderProductModel wareHouseProductModel)
		{
			wareHouseProductModel.FactoryNumber = Regex.Replace(wareHouseProductModel.FactoryNumber, @"\s+", "");
			if(wareHouseProductModel.QuantityInStock > wareHouseProductModel.MaxQuantity || wareHouseProductModel.QuantityInStock < 0)
			{
				return false;
			}
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
			if (wareHouseProductModel.QuantityInStock < 0 && wareHouseProductModel.MaxQuantity < wareHouseProductModel.QuantityInStock)
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
		public static string isWrongWareHouseProductMassageInOrderDetails(DetailsOrderProductModel wareHouseProductModel)
		{
			wareHouseProductModel.FactoryNumber = Regex.Replace(wareHouseProductModel.FactoryNumber, @"\s+", "");

			string message = "Неправильно введено: \n";
			if (wareHouseProductModel.QuantityInStock > wareHouseProductModel.MaxQuantity || wareHouseProductModel.QuantityInStock < 0)
			{
				message += $"Збій кількостей товару: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.FactoryNumber) ||
				IsDigitsOnly(wareHouseProductModel.FactoryNumber) == false ||
				wareHouseProductModel.FactoryNumber.Length != 10)
			{
				message += $"Заводський номер: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.Name))
			{
				message += $"Ви забули ввести ім'я: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.Set))
			{
				message += $"Ви забули ввести сервіз: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (String.IsNullOrEmpty(wareHouseProductModel.Type))
			{
				message += $"Ви забули ввести тип: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (wareHouseProductModel.Photo == null)
			{
				message += $"Ви забули вибрати фото: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (wareHouseProductModel.QuantityInStock < 0 && wareHouseProductModel.MaxQuantity < wareHouseProductModel.QuantityInStock)
			{
				message += $"Неправильно введена кількість: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (wareHouseProductModel.NetPrice <= 0)
			{
				message += $"Неправильно введена ціна покупки: {wareHouseProductModel.FactoryNumber}.\n";
			}
			if (wareHouseProductModel.SellPrice <= 0)
			{
				message += $"Неправильно введена ціна продажу: {wareHouseProductModel.FactoryNumber}.\n";
			}
			return message;
		}

		public static bool areUpdatableProducts(BindableCollection<WareHouseProductModel> wareHouseProducts)
		{
			foreach (var item in wareHouseProducts)
			{
				if(item.WasUpdated == true)
				{
					return true;
				}
			}
			return false;
		}
	}
}
