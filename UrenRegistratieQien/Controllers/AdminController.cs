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



        public IActionResult Admin(string month, string employeeName, string approved, string submitted, string totalhoursmonth, int totalhoursyear)
        {

            ViewBag.AllForms = declarationFormRepo.GetAllForms();
            ViewBag.Months = monthList;
            var forms = declarationFormRepo.GetAllForms();

            if (totalhoursyear == 0)
            {
                totalhoursyear = DateTime.Now.Year;
            }



            ViewBag.TotalHoursWorked = declarationFormRepo.TotalHoursWorked(forms, totalhoursmonth, totalhoursyear);
            ViewBag.TotalHoursOvertime = declarationFormRepo.TotalHoursOvertime(forms, totalhoursmonth, totalhoursyear);
            ViewBag.TotalHoursSickness = declarationFormRepo.TotalHoursSickness(forms, totalhoursmonth, totalhoursyear);
            ViewBag.TotalHoursVacation = declarationFormRepo.TotalHoursVacation(forms, totalhoursmonth, totalhoursyear);
            ViewBag.TotalHoursHoliday = declarationFormRepo.TotalHoursHoliday(forms, totalhoursmonth, totalhoursyear);
            ViewBag.TotalHoursTraining = declarationFormRepo.TotalHoursTraining(forms, totalhoursmonth, totalhoursyear);
            ViewBag.TotalHoursOther = declarationFormRepo.TotalHoursOther(forms, totalhoursmonth, totalhoursyear);

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