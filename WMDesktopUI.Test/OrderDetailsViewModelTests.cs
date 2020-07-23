using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WMDesktopUI.Helpers;
using WMDesktopUI.Models;
using Xunit;

namespace WMDesktopUI.Test
{
    public class OrderDetailsViewModelTests
    {
        private byte[] goodImage = ConvertHelper.ImageToByteArray(new BitmapImage(new Uri(@"C:\Users\Andrian\Downloads\photo_2019-06-25_17-56-37.jpg")));
        private const decimal testMax = 79228162514264337;
        [Theory]

        //Everything ok
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", 3, 7, 10, 10.50, 30.99, true)]

        //FactoryNumber wrong
        [InlineData("148627", "Горня тестове", "Toys", "Тип", 3, 7, 10, 10.50, 30.99, false)]
        [InlineData("148627659756789", "Горня тестове", "Toys", "Тип", 3, 7, 10, 10.50, 30.99, false)]
        [InlineData("14862a6597", "Горня тестове", "Toys", "Тип", 3, 7, 10, 10.50, 30.99, false)]
        [InlineData("", "Горня тестове", "Toys", "Тип", 3, 7, 10, 10.50, 30.99, false)]

        //FactoryNumber right
        [InlineData(" 1486276597", "Горня тестове", "Toys", "Тип", 3, 7, 10, 10.50, 30.99, true)]
        [InlineData("148 62 76 597", "Горня тестове", "Toys", "Тип", 3, 7, 10, 10.50, 30.99, true)]


        //Name wrong
        [InlineData("1486276597", "", "Toys", "Тип", 3, 7, 10, 10.50, 30.99, false)]
        [InlineData("1486276597", null, "Toys", "Тип", 3, 7, 10, 10.50, 30.99, false)]

        //Set wrong
        [InlineData("1486276597", "Горня тестове", "", "Тип", 3, 7, 10, 10.50, 30.99, false)]
        [InlineData("1486276597", "Горня тестове", null, "Тип", 3, 7, 10, 10.50, 30.99, false)]

        //Type wrong
        [InlineData("1486276597", "Горня тестове", "Toys", "", 3, 7, 10, 10.50, 30.99, false)]
        [InlineData("1486276597", "Горня тестове", "Toys", null, 3, 7, 10, 10.50, 30.99, false)]

        //Quantities wrong
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", -1, 7, 10, 10.50, 30.99, false)]
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", int.MinValue, 7, 10, 10.50, 30.99, false)]
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", 11, 7, 10, 10.50, 30.99, false)]

        //Quantities right
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", int.MaxValue, 7, int.MaxValue, 10.50, 30.99, true)]

        //NetPrice wrong
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", 3, 7, 10, -0.01, 30.99, false)]
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", 3, 7, 10, 0, 30.99, false)]

        //SellPrice wrong
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", 3, 7, 10, 10.50, -0.01, false)]
        [InlineData("1486276597", "Горня тестове", "Toys", "Тип", 3, 7, 10, 10.50, 0, false)]


        public void isCorrectWareHouseProductInOrderDetails_ShouldCheck(string factoryNumber, string name, string set, string type,
             int quantityInStock, int currentQuantity, int maxQuantity,  decimal netPrice, decimal sellPrice, bool _expected)
        {
            DetailsOrderProductModel product = new DetailsOrderProductModel()
            {
                FactoryNumber = factoryNumber,
                Name = name,
                Set = set,
                Type = type,
                Photo = goodImage,
                QuantityInStock = quantityInStock,
                CurrentQuantityInStock = currentQuantity,
                MaxQuantity = maxQuantity,
                NetPrice = netPrice,
                SellPrice = sellPrice
            };

            bool actual = InputHelper.isCorrectWareHouseProductInOrderDetails(product);

            bool expected = _expected;
            Assert.Equal(actual, expected);
        }
    }
}
