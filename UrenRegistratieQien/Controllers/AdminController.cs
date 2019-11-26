using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrenRegistratieQien.Repositories;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDeclarationFormRepository declarationFormRepo;
        private readonly IEmployeeRepository employeeRepository;

        public AdminController(IDeclarationFormRepository DeclarationFormRepo, IEmployeeRepository employeeRepository)
        {

            declarationFormRepo = DeclarationFormRepo;
            this.employeeRepository = employeeRepository;
        }

        [HttpPost]
        public IActionResult oefenmethode(int oefengetal)
        {
            var x = oefengetal;
            return null;
        }

        public IActionResult ShowEmployees()
        {
            var employees = employeeRepository.GetEmployees();
            return View(employees);
        }

        public IActionResult ChangeEmployee(string EmployeeId)
        {
            var employee = employeeRepository.GetEmployee(EmployeeId);
            return View(employee);
        }
        
        public IActionResult ViewDeclarationForm(int formId)
        {
            var form = declarationFormRepo.GetFormByFormId(formId);
            return View(form);
        }


        public IActionResult Admin()
        {
            ViewBag.AllForms = declarationFormRepo.GetAllForms();
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
            return View(forms);
        }

        public IActionResult AdminWithParam(string employeeId)
        {
            ViewBag.AllForms = declarationFormRepo.GetAllForms();
            var forms = declarationFormRepo.GetAllFormsOfUser(employeeId);
            return View("~/Views/Admin/Admin.cshtml", forms);
        }



        public IActionResult EmployeeForms(string employeeId)
        {
            var forms = declarationFormRepo.GetAllFormsOfUser(employeeId);
            return View(forms);
        }
    }
}