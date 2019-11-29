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

        public IActionResult Dashboard()
        {
            var userId = _userManager.GetUserId(HttpContext.User); //ophalen van userId die is ingelogd
            ViewBag.User = employeeRepo.GetEmployee(userId);
            ViewBag.Client = clientRepo.GetClientByUserId(userId);
            var inputModel = declarationRepo.GetAllFormsOfUser(userId);
            return View(inputModel);
        }

        //public IActionResult DashboardTotal(int formId, string totalhoursmonth, int totalhoursyear)
        //{
        //    var userId = _userManager.GetUserId(HttpContext.User); //ophalen van userId die is ingelogd
        //    ViewBag.User = employeeRepo.GetEmployee(userId);
        //    ViewBag.Client = clientRepo.GetClientByUserId(userId);
        //    var x = declarationRepo.TotalHoursWorkedByFormId(formId, totalhoursmonth, totalhoursyear);
        //    ViewBag.TotalHoursWorkedByFormId = declarationRepo.TotalHoursWorkedByFormId(formId, totalhoursmonth, totalhoursyear);
        //    var inputModel = declarationRepo.GetAllFormsOfUser(userId);
        //    return View(inputModel);



            ////var form = declarationRepo.GetFormByFormId(formId);
            //declarationRepo.TotalHoursWorkedByFormId(formId, totalhoursmonth, totalhoursyear);
            //if (totalhoursyear == 0)
            //{
            //    totalhoursyear = DateTime.Now.Year;
            //}

            //return View("Dashboard");
            //return View(declarationRepo.GetAllFormsOfUser(employeeId));
        }
    }
//}