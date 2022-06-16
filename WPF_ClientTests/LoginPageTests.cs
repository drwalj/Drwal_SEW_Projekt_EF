using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPF_Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drwal_SEW_Projekt_EF.Controllers;
using WPF_Client.Annotations;
using VodLib.models;
using VodLib.data;

namespace WPF_Client.Tests
{
    [TestClass()]
    public class LoginPageTests
    {
        [TestMethod()]
        public async Task GetClientWithId_shouldReturnTheRightClient()
        {
            var suspect = await RestHelper.GetClientWithIDAsync(69);
            Assert.AreEqual(suspect.firstname, "admin");

        }
        [TestMethod()]
        public async Task GetAllDeliveryMethods_ShouldReturnAllDeliveryMethods()
        {
            var suspect = await RestHelper.GetAllDeliveryMethodsAsync();
            Assert.AreEqual(suspect.Count(), 3);

        }
        [TestMethod()]
        public async Task TwoUsersWithTheSameNameCantExist()
        {
            var suspect = await RestHelper.PostNewClientAsync("admin","admin");
            Assert.AreEqual(suspect, "-404");

        }
    }
}