using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrenRegistratieQien.Repositories;
using UrenRegistratieQien.Models;
using UrenRegistratieQien.GlobalClasses;

namespace UrenRegistratieQien.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDeclarationFormRepository declarationFormRepo;

        private readonly IEmployeeRepository employeeRepo;
        public List<string> monthList { get; set; }

        public AdminController(IDeclarationFormRepository DeclarationFormRepo, IEmployeeRepository EmployeeRepo)
        {

            declarationFormRepo = DeclarationFormRepo;
            employeeRepo = EmployeeRepo;
            monthList = new List<string> { "Januari", "Februari", "March", "April", "May", "June", "Juli", "August", "September", "October", "November", "December" };
        }

        [HttpPost]
        public IActionResult oefenmethode(int oefengetal)
        {
            var x = oefengetal;
            return null;
        }

        public IActionResult ShowEmployees()
        {
            var employees = employeeRepo.GetEmployees();
            return View(employees);
        }

        public IActionResult ChangeEmployee(string EmployeeId)
        {
            var employee = employeeRepo.GetEmployee(EmployeeId);
            return View(employee);
        }
        
        public IActionResult ViewDeclarationForm(int formId)
        {
            var form = declarationFormRepo.GetFormByFormId(formId);
            return View(form);
        }



        public IActionResult Admin(string month, string employeeName, string approved, string submitted)
        {

            ViewBag.AllForms = declarationFormRepo.GetAllForms();
            ViewBag.Months = monthList;
            var forms = declarationFormRepo.GetAllForms();

            // door Januari parameter uit de IRepo en Repo (+if statement) eruit te halen, kan je sowieso alle uren inzien. 
            // Nu geeft ie alleen januari zoals hieronder staat, selectie andere maand is nog niet werkend
            ViewBag.TotalHoursWorked = declarationFormRepo.TotalHoursWorked(forms, "Januari");
            ViewBag.TotalHoursOvertime = declarationFormRepo.TotalHoursOvertime(forms, "Januari");
            ViewBag.TotalHoursSickness = declarationFormRepo.TotalHoursSickness(forms, "Januari");
            ViewBag.TotalHoursVacation = declarationFormRepo.TotalHoursVacation(forms, "Januari");
            ViewBag.TotalHoursHoliday = declarationFormRepo.TotalHoursHoliday(forms, "Januari");
            ViewBag.TotalHoursTraining = declarationFormRepo.TotalHoursTraining(forms, "Januari");
            ViewBag.TotalHoursOther = declarationFormRepo.TotalHoursOther(forms, "Januari");


            string employeeId;
            if(employeeName != null)
            {
                employeeId = employeeRepo.GetEmployeeByName(employeeName).EmployeeId;
            } else
            {
                employeeId = null;
            }
            return View(declarationFormRepo.GetFilteredForms(employeeId, month, approved, submitted));
        }

        public IActionResult AdminWithEmployeeId(string employeeId)
        {

            ViewBag.AllForms = declarationFormRepo.GetAllForms();
            ViewBag.Months = monthList;
            var forms = declarationFormRepo.GetAllFormsOfUser(employeeId);
            return View("~/Views/Admin/Admin.cshtml", forms);
        }

        public IActionResult AdminWithMonthYear(string month, int year)
        {
            ViewBag.AllForms = declarationFormRepo.GetAllForms();
            ViewBag.Months = monthList;
            var forms = declarationFormRepo.GetAllFormsOfMonth(MonthConverter.ConvertMonthToInt(month));
            return View("~/Views/Admin/Admin.cshtml", forms);
        }



        public IActionResult EmployeeForms(string employeeId)
        {
            var forms = declarationFormRepo.GetAllFormsOfUser(employeeId);
            return View(forms);
        }
    }
}