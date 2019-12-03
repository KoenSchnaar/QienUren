using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrenRegistratieQien.DatabaseClasses;
using UrenRegistratieQien.Models;
using UrenRegistratieQien.Repositories;

namespace UrenRegistratieQien.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Employee> _userManager;

        private readonly IEmployeeRepository employeeRepo;

        public HomeController(ILogger<HomeController> logger, IEmployeeRepository EmployeeRepo, UserManager<Employee> userManager = null)
        {
            _logger = logger;
            _userManager = userManager;
            employeeRepo = EmployeeRepo;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await employeeRepo.GetEmployee(userId);
            //checkrole

            switch (user.Role)
            {
                case 1:
                    return RedirectToAction("Admin", "Admin"); //admin
                case 2:
                    return RedirectToAction("Dashboard", "Employee"); //medewerker
                case 3:
                    return RedirectToAction("Dashboard", "Employee"); //trainee
                case 4:
                    return RedirectToAction("Dashboard", "Employee"); //inactief
                default:
                    return View("~/Views/Home/AccessDenied.cshtml");
            }
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public IActionResult User()
        //{
        //    return View();
        //}
        //public IActionResult CreateUser()
        //{
        //    return View();
        //}
        //public IActionResult HourReg()
        //{
        //    return View();
        //}
    }
}
