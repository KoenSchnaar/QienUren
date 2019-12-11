using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrenRegistratieQien.DatabaseClasses;
using UrenRegistratieQien.Exceptions;
using UrenRegistratieQien.GlobalClasses;
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

        
        public async Task<IActionResult> HourReg(int declarationFormId, string userId, int year, string month, string errorMessage = null)
        {
            if (await employeeRepo.UserIsEmployeeOrTrainee() || !await employeeRepo.UserIsOutOfService())
            {
                await hourRowRepo.AddHourRows(year, month, declarationFormId);
                ViewBag.User = await employeeRepo.GetEmployee(userId);
                if (errorMessage != null)
                {
                    ViewBag.ErrorMessage = errorMessage;
                }
                else
                {
                    ViewBag.ErrorMessage = "";
                }
                var inputModel = await declarationRepo.GetForm(declarationFormId);
                return View(inputModel);
            }
            else
            {
                return await AccessDeniedView();
            }
        }

        [HttpPost]
        public async Task<IActionResult> HourReg(DeclarationFormModel decModel, IFormFile file)
        {
            if (await employeeRepo.UserIsEmployeeOrTrainee())
            {
                employeeRepo.UploadFile(file, decModel.FormId);

                try
                {
                    await declarationRepo.EditDeclarationForm(decModel);
                }
                catch (MoreThan24HoursException e)
                {
                    return RedirectToAction("HourReg", new { declarationFormId = decModel.FormId,  userId = decModel.EmployeeId, year = decModel.Year, month = decModel.Month, errorMessage = e.Message });
                }

                await declarationRepo.CalculateTotalHours(decModel);
                return RedirectToAction("Dashboard", "Employee");
            }
            else
            {
                return await AccessDeniedView();
            }
        }

        public async Task<IActionResult> CreateForm(string employeeId)
        {
            if (await employeeRepo.UserIsEmployeeOrTrainee())
            {
                await declarationRepo.CreateForm(employeeId);
                return RedirectToAction("Dashboard", "Employee");
            }
            else
            {
                return await AccessDeniedView();
            }
        }

        public async Task<ViewResult> AccessDeniedView()
        {
            return View("~/Views/Home/AccessDenied.cshtml");
        }

        //public FileContentResult DownloadTotalHoursCSV(int totalWorked, int totalOvertime, int totalSickness, int totalVacation, int totalHoliday, int totalTraining, int totalOther) //eventueel filters meenemen..
        //{
        //    List<string> downloadableList = new List<string>
        //    {
        //        Convert.ToString(totalWorked),
        //        Convert.ToString(totalOvertime),
        //        Convert.ToString(totalSickness),
        //        Convert.ToString(totalVacation),
        //        Convert.ToString(totalHoliday),
        //        Convert.ToString(totalTraining),
        //        Convert.ToString(totalOther)
        //    };
        //    Download download = new Download();
        //    string fileName = "Totalhours.txt";
        //    string header = "gewerkt, overuren, ziekte, vakantie, feestdagen, training, anders";
        //    download.MakeCSV(header, downloadableList, fileName);


        //    byte[] fileBytes = System.IO.File.ReadAllBytes("Downloads/" + fileName);
        //    return File(fileBytes, "text/plain", fileName);
        //}

    }
}
