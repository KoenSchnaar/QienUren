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
    public class AdminControllerUnitTest
    {

        [TestMethod]
        public async Task AdminControllerShowEmployeesShouldReturnViewWithListOfEmployees()
        {

            //arrange
            var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());

            //act
            var actionResult = await adminController.ShowEmployees();
            var viewResult = (ViewResult)actionResult;
            var viewModel = viewResult.Model;

            //assert
            Assert.IsInstanceOfType(viewModel, typeof(List<EmployeeModel>));
        }

        [TestMethod]
        public async Task AdminControllerChangeEmployeeShouldReturnViewWithEmployee()
        {
            //arrange
            var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());
            string EmployeeId = "1";

            //act
            var actionResult = await adminController.ChangeEmployee(EmployeeId);
            var viewResult = (ViewResult)actionResult;
            var viewModel = (EmployeeModel)viewResult.Model;

            //assert
            Assert.IsTrue(viewModel.EmployeeId == "1");


        }

        [TestMethod]
        public async Task AdminControllerDeleteEmployeeSubmitShouldReturnRedirection()
        {
            // arrange
            var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());
            string employeeId = "1";

            //act
            var actionResult = await adminController.DeleteEmployee(employeeId);
            //var viewResult = (ViewResult)actionResult;
            //var viewModel = (EmployeeModel)viewResult.Model;

            //assert
            Assert.IsInstanceOfType(actionResult, typeof(RedirectToActionResult));
            //Assert.IsTrue(viewModel.EmployeeId == "1");

        }

        [TestMethod]
        public async Task AdminControllerEditEmployeePostShouldReturnRedirection()
        {
            // arrange
            var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());

            //act
            var actionResult = await adminController.EditEmployee(new EditingEmployeeModel());

            //assert
            Assert.IsInstanceOfType(actionResult, typeof(RedirectToActionResult));


        }

        [TestMethod]    // deze ff met Luuk naar kijken nog
        public async Task AdminControllerViewDeclarationFormShouldReturnViewWithDeclarationform()
        {
            // arrange
            var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());
            int formId = 1;

            //act
            var actionResult = await adminController.ViewDeclarationForm(formId);
            var viewResult = (ViewResult)actionResult;
            var viewModel = (DeclarationFormModel)viewResult.Model;

            //assert
            Assert.IsInstanceOfType(viewModel, typeof(DeclarationFormModel));
            Assert.IsTrue(viewModel.FormId == 1);
        }

        [TestMethod]
        public async Task AdminControllerShowClientsShouldReturnViewWithListOfClients()
        {
            //arrange
            var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());

            //act
            var actionResult = await adminController.ShowClients();
            var viewResult = (ViewResult)actionResult;
            var viewModel = viewResult.Model;

            //assert
            Assert.IsInstanceOfType(viewModel, typeof(List<ClientModel>));
        }


        public async Task AdminControllerAdminInputModelShouldBeListOfDeclarationForms()
        {
            //arrange
            var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());

            //act
            var actionResult = await adminController.Admin();
            var viewResult = (ViewResult)actionResult;
            var viewModel = viewResult.Model;

            //assert
            Assert.IsInstanceOfType(viewModel, typeof(List<DeclarationFormModel>));

        }

        [TestMethod]
        public async Task EmployeeControllerAddClientViewResultShouldHaveClientModel()
        {
            //arrange
            var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());

            //act
            var actionResult = await adminController.AddClient();
            var viewResult = (ViewResult)actionResult;
            var viewModel = viewResult.Model;

            //assert
            Assert.IsInstanceOfType(viewModel, typeof(ClientModel));
        }

        [TestMethod]
        public async Task EmployeeControllerPostAddClientShouldReturnRedirect()
        {
            //arrange
            var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());


            //act
            var actionResult = await adminController.AddClient(new ClientModel());


            //assert
            Assert.IsInstanceOfType(actionResult, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task EmployeeControllerPostEditClientShouldReturnRedirect()
        {
            //arrange
            var adminController = new AdminController(new FakeDeclarationFormRepository(), new FakeEmployeeRepository(), new FakeClientRepository());


            //act
            var actionResult = await adminController.EditClient(new ClientModel());


            //assert
            Assert.IsInstanceOfType(actionResult, typeof(RedirectToActionResult));
        }
    }
}
