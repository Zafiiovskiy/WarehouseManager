using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WMDesktopUI.Helpers;
using WMDesktopUI.Models;
using Xunit;
using static System.Net.Mime.MediaTypeNames;

namespace WMDesktopUI.Test
{
    public class AddProductToWareHouseViewModelTests
    {
        private byte[] goodImage = ConvertHelper.ImageToByteArray(new BitmapImage(new Uri(@"C:\Users\Andrian\Downloads\photo_2019-06-25_17-56-37.jpg")));
      
        [Theory]

        //Everything ok
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", 3, 10.50, 30.99, true)]

        //FactoryNumber wrong
        [InlineData("148627", "Горня тестове", "Toys", "Тип", 3, 10.50, 30.99, false)]
        [InlineData("148627659756789", "Горня тестове", "Toys", "Тип", 3, 10.50, 30.99, false)]
        [InlineData("14862a6597", "Горня тестове", "Toys", "Тип", 3, 10.50, 30.99, false)]
        [InlineData("", "Горня тестове", "Toys", "Тип", 3, 10.50, 30.99, false)]

        //Name wrong
        [InlineData("1486276597", "", "Toys", "Тип", 3, 10.50, 30.99, false)]
        [InlineData("1486276597", null , "Toys", "Тип", 3, 10.50, 30.99, false)]

        //Set wrong
        [InlineData("1486276597", "Горня тестове", "", "Тип", 3, 10.50, 30.99, false)]
        [InlineData("1486276597", "Горня тестове", null , "Тип", 3, 10.50, 30.99, false)]

        //Type wrong
        [InlineData("1486276597", "Горня тестове", "Toys", "", 3, 10.50, 30.99, false)]
        [InlineData("1486276597", "Горня тестове", "Toys", null , 3, 10.50, 30.99, false)]

        //QuantityInStock wrong
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", 0, 10.50, 30.99, false)]
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", -1, 10.50, 30.99, false)]
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", int.MinValue , 10.50, 30.99, false)]

        //QuantityInStock right
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", int.MaxValue , 10.50, 30.99, true)]

        //NetPrice wrong
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", 3, -0.01, 30.99, false)]
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", 3, 0, 30.99, false)]

        //SellPrice wrong
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", 3, 10.50, -0.01, false)]
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", 3, 10.50, 0, false)]


        public void isCorrectWareHouseProduct_ShouldCheck(string factoryNumber, string name, string set, string type,
             int quantityInStock, decimal netPrice, decimal sellPrice, bool _expected)
        {
            WareHouseProductModel product = new WareHouseProductModel()
            {
                FactoryNumber = factoryNumber,
                Name = name,
                Set = set,
                Type = type,
                Photo = goodImage,
                QuantityInStock = quantityInStock,
                NetPrice = netPrice,
                SellPrice = sellPrice
            };

            bool actual = InputHelper.isCorrectWareHouseProduct(product);

            bool expected = _expected;
        }
    }
}
