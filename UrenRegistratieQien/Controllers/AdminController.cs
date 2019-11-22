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

        public AdminController(IDeclarationFormRepository DeclarationFormRepo)
        {

            declarationFormRepo = DeclarationFormRepo;
            
        }


        
        public IActionResult ViewDeclarationForm(int formId)
        {
            var form = declarationFormRepo.GetFormByFormId(formId);
            return View(form);
        }


        public IActionResult Admin()
        {

            var forms = declarationFormRepo.GetAllForms();
            return View(forms);
        }


        [HttpPost]
        public IActionResult Admin(string employeeId)
        {

            var forms = declarationFormRepo.GetAllFormsOfUser(employeeId);
            return View(forms);
        }



        public IActionResult EmployeeForms(string employeeId)
        {
            var forms = declarationFormRepo.GetAllFormsOfUser(employeeId);
            return View(forms);
        }
    }
}