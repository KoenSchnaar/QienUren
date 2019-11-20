using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrenRegistratieQien.Models;
using UrenRegistratieQien.Repositories;

namespace UrenRegistratieQien.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IClientRepository clientRepo;
        private readonly IDeclarationFormRepository declarationRepo;
        private readonly IEmployeeRepository employerRepo;
        private readonly IHourRowRepository hourRowRepo;

        public EmployeeController(IClientRepository ClientRepo, IDeclarationFormRepository DeclarationRepo, IEmployeeRepository EmployeeRepo, IHourRowRepository HourRowRepo)
        {
            clientRepo = ClientRepo;
            declarationRepo = DeclarationRepo;
            employerRepo = EmployeeRepo;
            hourRowRepo = HourRowRepo;
        }
        public IActionResult Index()
        {
            return View();
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