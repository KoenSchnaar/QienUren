using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UrenRegistratieQien.Controllers;
using UrenRegistratieQien.DatabaseClasses;
using UrenRegistratieQien.Models;
using UrenRegistratieQienTest.Fakes;


namespace UrenRegistratieQienTest
{
    [TestClass]
    public class HomeControllerUnitTest
    {


        [TestMethod]
        public void EmployeeControllerIndexShouldReturnView()
        {
            //arrange
            var HomeController = new HomeController(null, new FakeEmployeeRepository());
            //act
            var result = HomeController.Index();
            //assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
