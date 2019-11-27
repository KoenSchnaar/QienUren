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
    public class DeclarationFormTests
    {


        [TestMethod]
        public void DeclarationFormControllerHourRegShouldReturnView()
        {
            //arrange
            var DeclarationFormController = new DeclarationFormController(new FakeClientRepository(), new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeHourRowRepository());
            int declarationFormId = 1;
            string userId = "1";
            int year = 2019;
            string month = "November";

            //act
            var result = DeclarationFormController.HourReg(declarationFormId, userId, year, month);
            //assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }


        [TestMethod]
        public void DeclarationFormControllerHourRegShouldReturnViewWithDeclarationFormModel()
        {
            //arrange
            var DeclarationFormController = new DeclarationFormController(new FakeClientRepository(), new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeHourRowRepository());
            int declarationFormId = 1;
            string userId = "1";
            int year = 2019;
            string month = "November";

            //act
            var actionResult = DeclarationFormController.HourReg(declarationFormId, userId, year, month);
            var viewResult = (ViewResult)actionResult;
            var viewModel = viewResult.Model;

            //assert
            Assert.IsInstanceOfType(viewModel, typeof(DeclarationFormModel));
        }

        [TestMethod]
        public void DeclarationFormControllerHourRegShouldReturnViewWithCorrectData()
        {
            //arrange
            var DeclarationFormController = new DeclarationFormController(new FakeClientRepository(), new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeHourRowRepository());
            int declarationFormId = 1;
            string userId = "1";
            int year = 2019;
            string month = "November";

            //act
            var actionResult = DeclarationFormController.HourReg(declarationFormId, userId, year, month);
            var viewResult = (ViewResult)actionResult;
            var viewModel = (DeclarationFormModel)viewResult.Model;

            //assert
            Assert.IsTrue(viewModel.FormId == 1 && viewModel.EmployeeId == "1");
        }

        [TestMethod]
        public void DeclarationFormControllerHourRegPostShouldReturnRedirection()
        {
            //arrange
            var DeclarationFormController = new DeclarationFormController(new FakeClientRepository(), new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeHourRowRepository());
            //act
            var result = DeclarationFormController.HourReg(new DeclarationFormModel());
            //assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void DeclarationFormControllerHourRegSubmitShouldReturnRedirection()
        {
            //arrange
            var DeclarationFormController = new DeclarationFormController(new FakeClientRepository(), new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeHourRowRepository());
            //act
            var result = DeclarationFormController.HourRegSubmit(new DeclarationFormModel());
            //assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

    }
}