using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrenRegistratieQien.DatabaseClasses;
using UrenRegistratieQien.Models;
using UrenRegistratieQien.Repositories;

namespace UrenRegistratieQien.Controllers
{
    public class DeclarationFormController : Controller
    {
        private readonly IClientRepository clientRepo;
        private readonly IDeclarationFormRepository declarationRepo;
        private readonly IEmployeeRepository employeeRepo;
        private readonly IHourRowRepository hourRowRepo;
        private readonly UserManager<Employee> _userManager;

        public DeclarationFormController(IClientRepository ClientRepo, IDeclarationFormRepository DeclarationRepo, IEmployeeRepository EmployeeRepo, IHourRowRepository HourRowRepo, UserManager<Employee> userManager = null)
        {
            clientRepo = ClientRepo;
            declarationRepo = DeclarationRepo;
            employeeRepo = EmployeeRepo;
            hourRowRepo = HourRowRepo;
            _userManager = userManager;
        }

        
        public IActionResult HourReg(int declarationFormId, string userId, int year, string month)
        {
            hourRowRepo.AddHourRows(year, month, declarationFormId);
            var inputModel = declarationRepo.GetForm(declarationFormId, userId);
            return View(inputModel);
        }

        [HttpPost]
        public IActionResult HourReg(DeclarationFormModel decModel)
        {
            declarationRepo.EditDeclarationForm(decModel);
            return RedirectToAction("Dashboard", "Employee");
        }

        [HttpPost]
        public IActionResult HourRegSubmit(DeclarationFormModel decModel)
        {
            declarationRepo.EditDeclarationForm(decModel);
            declarationRepo.SubmitDeclarationForm(decModel);
            return RedirectToAction("Dashboard", "Employee");
        }



    }
}
