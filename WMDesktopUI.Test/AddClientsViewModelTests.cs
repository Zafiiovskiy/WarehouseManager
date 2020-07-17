using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMDesktopUI.Helpers;
using WMDesktopUI.Models;
using WMDesktopUI.ViewModels;
using Xunit;

namespace WMDesktopUI.Test
{
    public class AddClientsViewModelTests
    {

        [Theory]
        //Everything ok
        [InlineData("0953370000", "Andrian", "Zafiiovskiy", "Somewhere", true)]
        [InlineData("9999999999", "Albert", "Einshtein", "Germany, Munich, Golz str. 5", true)]


        //PhoneNumber wrong
        [InlineData("", "Andrian", "Zafiiovskiy", "Somewhere", false)]
        [InlineData("09533700000000", "Andrian", "Zafiiovskiy", "Somewhere", false)]
        [InlineData("953370000", "Andrian", "Zafiiovskiy", "Somewhere", false)]
        [InlineData("0953a70000", "Andrian", "Zafiiovskiy", "Somewhere", false)]

        //Name wrong
        [InlineData("0953370000", "", "Zafiiovskiy", "Somewhere", false)]
        [InlineData("0953370000", "An9rian", "Zafiiovskiy", "Somewhere", false)]
        [InlineData("0953370000", "andrian", "Zafiiovskiy", "Somewhere", false)]

        //Surname wrong
        [InlineData("0953370000", "Andrian", "", "Somewhere", false)]
        [InlineData("0953370000", "Andrian", "Zafii9vskiy", "Somewhere", false)]
        [InlineData("0953370000", "Andrian", "zafiiovskiy", "Somewhere", false)]

        //Adress wrong
        [InlineData("0953370000", "Andrian", "Zafiiovskiy", "", false)]





        public void isCorrectClient_ShouldCheck(string phoneNumber, string name, string surname, string adress,bool expected)
        {
            ClientModel clientModel = new ClientModel()
            {
                PhoneNumber = phoneNumber,
                Name = name,
                Surname = surname,
                Adress = adress
            };

            bool actual = InputHelper.isCorrectClient(clientModel);

            Assert.Equal(expected, actual);
        }
    }
}
