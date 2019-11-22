using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UrenRegistratieQien.Controllers;
using UrenRegistratieQien.DatabaseClasses;
using UrenRegistratieQien.Models;
using UrenRegistratieQienTest.Fakes;

namespace UrenRegistratieQienTest
{
    //[TestClass]
    //public class UnitTest1
    //{


    //    [TestMethod]
    //    public void EmployeeControllerIndexShouldReturnView()
    //    {
    //        //arrange
    //        var employeeController = new EmployeeController(new FakeClientRepository(), new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeHourRowRepository());
    //        //act
    //        var result = employeeController.Index();
    //        //assert
    //        Assert.IsInstanceOfType(result, typeof(ViewResult));
    //    }


    //    [TestMethod]
    //    public void EmployeeControllerAllClientsShouldReturnViewWithListOfClients()
    //    {
    //        //arrange
    //        var employeeController = new EmployeeController(new FakeClientRepository(), new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeHourRowRepository());

    //        //act
    //        var actionResult = employeeController.AllClients();
    //        var viewResult = (ViewResult)actionResult;
    //        var viewModel = viewResult.Model;

    //        //assert
    //        Assert.IsInstanceOfType(viewModel, typeof(List<ClientModel>));


    //    }
    //}
}
