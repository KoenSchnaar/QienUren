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
    public class AdminControllerUnitTest
    {

        [TestMethod]
        public void AdminControllerShowEmployeesShouldReturnViewWithListOfEmployees()
        {

            //arrange
            var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());

            //act
            var actionResult = adminController.ShowEmployees();
            var viewResult = (ViewResult)actionResult;
            var viewModel = viewResult.Model;

            //assert
            Assert.IsInstanceOfType(viewModel, typeof(List<EmployeeModel>));

        }

        [TestMethod]
        public void AdminControllerChangeEmployeeShouldReturnViewWithEmployee()
        {
            //arrange
            var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());
            string EmployeeId = "1";

            //act
            var actionResult = adminController.ChangeEmployee(EmployeeId);
            var viewResult = (ViewResult)actionResult;
            var viewModel = (EmployeeModel)viewResult.Model;

            //assert
            Assert.IsTrue(viewModel.EmployeeId == "1");


        }

        [TestMethod]
        public void AdminControllerDeleteEmployeeSubmitShouldReturnRedirection()
        {
            // arrange
            var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());
            string employeeId = "1";

            //act
            var actionResult = adminController.DeleteEmployee(employeeId);
            //var viewResult = (ViewResult)actionResult;
            //var viewModel = (EmployeeModel)viewResult.Model;

            //assert
            Assert.IsInstanceOfType(actionResult, typeof(RedirectToActionResult));
            //Assert.IsTrue(viewModel.EmployeeId == "1");

        }

        [TestMethod]
        public void AdminControllerEditEmployeePostShouldReturnRedirection()
        {
            // arrange
            var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());

            //act
            var actionResult = adminController.EditEmployee(new EmployeeModel());

            //assert
            Assert.IsInstanceOfType(actionResult, typeof(RedirectToActionResult));


        }

        [TestMethod]    // deze ff met Luuk naar kijken nog
        public void AdminControllerViewDeclarationFormShouldReturnViewWithDeclarationform()
        {
            // arrange
            var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());
            int formId = 1;

            //act
            var actionResult = adminController.ViewDeclarationForm(formId);
            var viewResult = (ViewResult)actionResult;
            var viewModel = (DeclarationFormModel)viewResult.Model;

            //assert
            Assert.IsInstanceOfType(viewModel, typeof(DeclarationFormModel));
            Assert.IsTrue(viewModel.FormId == 1);
        }

        // deze even overgeslagen  //////// ook het hele totalhoursworked riddeltje etc
        // vragen of nodig is om alle methodes die hieronder staan er ook doorheen te sturen


        //[TestMethod]   
        //public void AdminControllerAdminShouldReturnView()

        //{
        //    arrange
        //   var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());
        //    string month = "Januari";
        //    string employeeName = "Henk";
        //    string approved = "Jup";
        //    string submitted = "Jup";
        //    string totalhoursmont = "JanTotaal";
        //    int totalhoursyear = 1;

        //    act
        //    var actionResult = adminController.Admin(month, employeeName, approved, submitted, totalhoursmont, totalhoursyear);
        //    var viewResult = (ViewResult)actionResult;
        //    var viewModel = (DeclarationFormModel)viewResult.Model;

        //    assert
        //    Assert.IsInstanceOfType(viewModel, typeof(DeclarationFormModel));
        //    Assert.IsTrue(viewModel.Month == "1");

        //    GetFilteredForms
        //    return View(declarationFormRepo.GetFilteredForms(employeeId, month, approved, submitted));
        //}



        [TestMethod]
        public void AdminControllerAdminWithEmployeeIdReturnsViewWithDeclarationformOfEmployee()
        {
            // arrange
            var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());
            string employeeId = "1";

            //act
            var actionResult = adminController.AdminWithEmployeeId(employeeId);
            var viewResult = (ViewResult)actionResult;
            var viewModel = viewResult.Model;

            //var viewModel2 = (DeclarationFormModel)viewResult.Model;

            //assert
            //Assert.IsTrue(viewModel2.EmployeeId == "1");
            Assert.IsInstanceOfType(viewModel, typeof(List<DeclarationFormModel>));

            //var forms = declarationFormRepo.GetAllFormsOfUser(employeeId);
            //return View("~/Views/Admin/Admin.cshtml", forms); // die form achter de komma, komt eruit via 1 input lijkt me?
        }

        //[TestMethod]
        //public void AdminWithMonthYear(string month, int year)
        //{
        //    ViewBag.AllForms = declarationFormRepo.GetAllForms();
        //    ViewBag.Months = monthList;
        //    var forms = declarationFormRepo.GetAllFormsOfMonth(MonthConverter.ConvertMonthToInt(month));
        //    return View("~/Views/Admin/Admin.cshtml", forms);
        //}


        [TestMethod]
        public void AdminControllerEmployeeFormsShouldReturnViewWithListOfFormsOfEmployeeId(string employeeId)
        {


            //arrange
            var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());
            string EmployeeId = "1";

            //act
            var actionResult = adminController.EmployeeForms(EmployeeId);
            var viewResult = (ViewResult)actionResult;
            var viewModel = viewResult.Model;


            //assert
            //Assert.IsTrue(viewModel.EmployeeId == "1");
            Assert.IsInstanceOfType(viewModel, typeof(List<DeclarationFormModel>));


            //public List<DeclarationFormModel> GetAllFormsOfUser(string userId)
            //{
            //    var declarationFormModel = new DeclarationFormModel { EmployeeId = userId };
            //    var declarationFormList = new List<DeclarationFormModel>();
            //    declarationFormList.Add(declarationFormModel);
            //    return declarationFormList;
            //}
        }

    }
}
