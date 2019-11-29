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

        public IActionResult Dashboard(string year = null, string month = null, string approved = null, string submitted = null)////// de filter toepassen in de model van deze
        {
            var userId = _userManager.GetUserId(HttpContext.User); //ophalen van userId die is ingelogd
            ViewBag.userId = userId;
            ViewBag.User = employeeRepo.GetEmployee(userId);
            ViewBag.Client = clientRepo.GetClientByUserId(userId);
            ViewBag.AllForms = declarationRepo.GetAllForms();
            //var inputModel = declarationRepo.GetAllFormsOfUser(userId);
            var inputModel = declarationRepo.GetFilteredForms(year, userId, month, approved, submitted);
            return View(inputModel);
        }
        public IActionResult Info()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var Employee = employeeRepo.GetEmployee(userId);
            ViewBag.Client = clientRepo.GetClientByUserId(userId);
            return View(Employee);
        }

        public IActionResult AllClients()
        {
            var clients = clientRepo.GetAllClients();
            return View(clients);
        }

        public IActionResult AddClient()
        {
            return View(new ClientModel());
        }

        [HttpPost]
        public IActionResult AddClient(ClientModel clientModel)
        {
            clientRepo.AddNewClient(clientModel);
            return RedirectToAction("AllClients");
        }

        //public IActionResult EditClient()
        //{
        //   return View(clientRepo.GetClient()); //methode om 1 client te krijgen
        //}

        [HttpPost]
        public IActionResult EditClient(ClientModel clientModel)
        {
            clientRepo.EditAClient(clientModel);
            return RedirectToAction("AllClients");
        }

        
    }
}