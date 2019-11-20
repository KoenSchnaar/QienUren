using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQienTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ClientRepositoryGetAllClientsShouldReturnListOfClientModel()
        {
            //arrange
            var clientRepository = new Fakes.FakeClientRepository();

            //act
            var result = clientRepository.GetAllClients();

            //assert
            Assert.IsInstanceOfType(result, typeof(List<ClientModel>));
        }
        
        [TestMethod]
        public void ClientRepositoryGetAllClientsAtIndexZeroCompanyNameShouldBeCompanyName()
        {
            //arrange
            var clientRepository = new Fakes.FakeClientRepository();

            //act
            var result = clientRepository.GetAllClients()[0].CompanyName;


            //assert
            Assert.AreEqual("companyname", result);
        }
    }
}
