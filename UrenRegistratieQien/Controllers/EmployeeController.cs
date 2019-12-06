using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IHostingEnvironment he;

        public EmployeeController(IClientRepository ClientRepo, 
                                  IDeclarationFormRepository DeclarationRepo, 
                                  IEmployeeRepository EmployeeRepo, 
                                  IHourRowRepository HourRowRepo, 
                                  IHostingEnvironment he,
                                  UserManager<Employee> userManager = null)
        {
            clientRepo = ClientRepo;
            declarationRepo = DeclarationRepo;
            employeeRepo = EmployeeRepo;
            hourRowRepo = HourRowRepo;
            _userManager = userManager;
            this.he = he;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard(string year = null, string month = null, string approved = null, string submitted = null, string sortDate = null)////// de filter toepassen in de model van deze
        {
            if (await UserIsEmployeeOrTrainee() || await UserIsOutOfService())
            {
                var userId = _userManager.GetUserId(HttpContext.User); //ophalen van userId die is ingelogd
                ViewBag.userId = userId;
                ViewBag.User = await employeeRepo.GetEmployee(userId);
                await employeeRepo.CheckIfYearPassedForAllTrainees();
                ViewBag.Client = await clientRepo.GetClientByUserId(userId);
                ViewBag.AllForms = await declarationRepo.GetAllForms();
                //var inputModel = declarationRepo.GetAllFormsOfUser(userId);
                var inputModel = await declarationRepo.GetFilteredForms(year, userId, month, approved, submitted, sortDate);
                return View(inputModel);
            }
            else
            {
                return await AccessDeniedView();
            }
        }

        
        public async Task<IActionResult> Info()
        {
            if (await UserIsEmployeeOrTrainee())
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var Employee = await employeeRepo.GetEmployee(userId);
                ViewBag.Client = await clientRepo.GetClientByUserId(userId);
                return View(Employee);
            }
            else
            {
                return await AccessDeniedView();
            }
        }

        public async Task<IActionResult> ChangePicture()
        {
            if (await UserIsEmployeeOrTrainee())
            {
                return View();
            }
            else
            {
                return await AccessDeniedView();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePicture(IFormFile picture)
        {
            if (await UserIsEmployeeOrTrainee())
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                await employeeRepo.UploadPicture(picture, userId);
                return RedirectToAction("Dashboard");
            }
            else
            {
                return await AccessDeniedView();
            }
        }

        public async Task<bool> UserIsEmployeeOrTrainee()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await employeeRepo.GetEmployee(userId);

            if (user.Role == 2 || user.Role == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UserIsOutOfService()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            bool outofservice = await employeeRepo.UserIsOneMonthInactive(userId);

            if (outofservice == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<ViewResult> AccessDeniedView()
        {
            return View("~/Views/Home/AccessDenied.cshtml");
        }
    }
}
