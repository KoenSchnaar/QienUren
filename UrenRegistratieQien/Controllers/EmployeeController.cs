using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrenRegistratieQien.Repositories;

namespace UrenRegistratieQien.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IClientRepository clientRepo;
        private readonly IDeclarationFormRepository declarationRepo;
        private readonly IEmployeeRepository employerRepo;
        private readonly IHourRowRepository hourRowRepo;

        public EmployeeController(IClientRepository ClientRepo, IDeclarationFormRepository DeclarationRepo, IEmployeeRepository EmployerRepo, IHourRowRepository HourRowRepo)
        {
            clientRepo = ClientRepo;
            declarationRepo = DeclarationRepo;
            employerRepo = EmployerRepo;
            hourRowRepo = HourRowRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}