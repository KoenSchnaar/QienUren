using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UrenRegistratieQien.DatabaseClasses;
using UrenRegistratieQien.Models;
using UrenRegistratieQien.Repositories;

namespace UrenRegistratieQien.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IClientRepository clientRepo;
        private readonly IDeclarationFormRepository declarationRepo;
        private readonly IEmployeeRepository employeeRepo;
        private readonly IHourRowRepository hourRowRepo;
        private readonly UserManager<Employee> _userManager;

        public EmployeeController(IClientRepository ClientRepo, IDeclarationFormRepository DeclarationRepo, IEmployeeRepository EmployeeRepo, IHourRowRepository HourRowRepo, UserManager<Employee> userManager = null)
        {
            clientRepo = ClientRepo;
            declarationRepo = DeclarationRepo;
            employeeRepo = EmployeeRepo;
            hourRowRepo = HourRowRepo;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard(string year = null, string month = null, string approved = null, string submitted = null, string sortDate = null)////// de filter toepassen in de model van deze
        {
            var userId = _userManager.GetUserId(HttpContext.User); //ophalen van userId die is ingelogd
            ViewBag.userId = userId;
            ViewBag.User = employeeRepo.GetEmployee(userId);
            ViewBag.Client = clientRepo.GetClientByUserId(userId);
            ViewBag.AllForms = declarationRepo.GetAllForms();
            //var inputModel = declarationRepo.GetAllFormsOfUser(userId);
            var inputModel = declarationRepo.GetFilteredForms(year, userId, month, approved, submitted, sortDate);
            return View(inputModel);
        }
        public IActionResult Info()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var Employee = employeeRepo.GetEmployee(userId);
            ViewBag.Client = clientRepo.GetClientByUserId(userId);
            return View(Employee);
        }
        }
    }
