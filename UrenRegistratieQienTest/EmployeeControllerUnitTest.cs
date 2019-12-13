using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrenRegistratieQien.Controllers;
using UrenRegistratieQien.DatabaseClasses;
using UrenRegistratieQien.Models;
using UrenRegistratieQienTest.Fakes;


namespace UrenRegistratieQienTest
{
    [TestClass]
    public class EmployeeControllerUnitTest
    {
        [TestMethod]
        public async Task EmployeeControllerHourRegShouldReturnView()
        {
            //arrange
            var EmployeeController = new EmployeeController(new FakeClientRepository(), new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeHourRowRepository(), null);
            int declarationFormId = 1;
            string userId = "1";
            int year = 2019;
            string month = "November";

            //act
            var result = await EmployeeController.HourReg(declarationFormId, userId, year, month);
            //assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }


        [TestMethod]
        public async Task EmployeeControllerHourRegShouldReturnViewWithDeclarationFormModel()
        {
            //arrange
            var EmployeeController = new EmployeeController(new FakeClientRepository(), new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeHourRowRepository(), null);
            int declarationFormId = 1;
            string userId = "1";
            int year = 2019;
            string month = "November";

            //act
            var actionResult = await EmployeeController.HourReg(declarationFormId, userId, year, month);
            var viewResult = (ViewResult)actionResult;
            var viewModel = viewResult.Model;

            //assert
            Assert.IsInstanceOfType(viewModel, typeof(DeclarationFormModel));
        }

        [TestMethod]
        public async Task EmployeeControllerHourRegShouldReturnViewWithCorrectData()
        {
            //arrange
            var EmployeeController = new EmployeeController(new FakeClientRepository(), new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeHourRowRepository(), null);
            int declarationFormId = 1;
            string userId = "1";
            int year = 2019;
            string month = "November";

            //act
            var actionResult = await EmployeeController.HourReg(declarationFormId, userId, year, month);
            var viewResult = (ViewResult)actionResult;
            var viewModel = (DeclarationFormModel)viewResult.Model;

            //assert
            Assert.IsTrue(viewModel.FormId == 1 && viewModel.EmployeeId == "1");
        }

        [TestMethod]
        public async Task EmployeeControllerHourRegPostShouldReturnRedirection()
        {
            //arrange
            var EmployeeController = new EmployeeController(new FakeClientRepository(), new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeHourRowRepository(), null);
            int declarationFormId = 1;
            string userId = "1";
            int year = 2019;
            string month = "November";

            //act
            var result = EmployeeController.HourReg(declarationFormId, userId, year, month);
            //assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task EmployeeControllerHourRegSubmitShouldReturnRedirection()
        {
            //arrange
            var EmployeeController = new EmployeeController(new FakeClientRepository(), new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeHourRowRepository(), null);
            int declarationFormId = 1;
            string userId = "1";
            int year = 2019;
            string month = "November";

            //act
            var result = EmployeeController.HourReg(declarationFormId, userId, year, month);
            //assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
    }
}
