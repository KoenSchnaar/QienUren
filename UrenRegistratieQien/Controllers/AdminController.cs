using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrenRegistratieQien.Repositories;
using UrenRegistratieQien.Models;
using UrenRegistratieQien.GlobalClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UrenRegistratieQien.DatabaseClasses;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UrenRegistratieQien.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IDeclarationFormRepository declarationFormRepo;
        private readonly UserManager<Employee> _userManager;
        private readonly IEmployeeRepository employeeRepo;
        private readonly IClientRepository clientRepo;
        public List<string> monthList { get; set; }

        public AdminController(IDeclarationFormRepository DeclarationFormRepo, IEmployeeRepository EmployeeRepo, IClientRepository ClientRepo, UserManager<Employee> userManager = null)
        {

            _userManager = userManager;
            declarationFormRepo = DeclarationFormRepo;
            employeeRepo = EmployeeRepo;
            clientRepo = ClientRepo;
            monthList = new List<string> { "Januari", "Februari", "March", "April", "May", "June", "Juli", "August", "September", "October", "November", "December" };
        }

        [HttpPost]
        public IActionResult UnsubmitForm(int formId)
        {
            declarationFormRepo.UnsubmitForm(formId);
            return RedirectToAction("Admin");
        }

        public IActionResult ShowEmployees()
        {
            if (UserIsAdmin())
            {
                var employees = employeeRepo.GetEmployees();
                return View(employees);
            } else
            {
                return AccessDeniedView();
            }
            
        }

        public IActionResult ChangeEmployee(string EmployeeId)
        {
            if (UserIsAdmin())
            {
                var employee = employeeRepo.GetEmployee(EmployeeId);
                return View(employee);
            }
            else
            {
                return AccessDeniedView();
            }

        }
        
        public IActionResult DeleteEmployee(string employeeId)
        {
            if (UserIsAdmin())
            {
                employeeRepo.DeleteEmployee(employeeId);
                return RedirectToAction("ShowEmployees");
            }
            else
            {
                return AccessDeniedView();
            }

        }

        [HttpPost]
        public IActionResult EditEmployee(EmployeeModel empModel)
        {
            if (UserIsAdmin())
            {
                employeeRepo.EditEmployee(empModel);
                return RedirectToAction("ShowEmployees");
            }
            else
            {
                return AccessDeniedView();
            }

        }

        public IActionResult ShowClients()
        {
            if (UserIsAdmin())
            {
                var clients = clientRepo.GetAllClients();
                return View(clients);
            }
            else
            {
                return AccessDeniedView();
            }

        }

        public IActionResult AddClient()
        {
            if (UserIsAdmin())
            {
                return View(new ClientModel());

            } else
            {
                return AccessDeniedView();
            }
        }

        [HttpPost]
        public IActionResult AddClient(ClientModel clientModel)
        {
            if (UserIsAdmin())
            {

                clientRepo.AddNewClient(clientModel);
                return RedirectToAction("ShowClients");
            } else
            {
                return AccessDeniedView();
            }
        }

        public IActionResult ChangeClient(int clientId)
        {
            if (UserIsAdmin())
            {

                return View(clientRepo.GetClient(clientId));
            } else
            {
                return AccessDeniedView();
            }
        }

        [HttpPost]
        public IActionResult EditClient(ClientModel clientModel)
        {
            if (UserIsAdmin())
            {
                clientRepo.EditAClient(clientModel);
                return RedirectToAction("ShowClients");

            } else
            {
                return AccessDeniedView();
            }
        }
        public IActionResult DeleteClient(int clientId)
        {
            if (UserIsAdmin())
            {
                clientRepo.DeleteClient(clientId);
                return RedirectToAction("ShowClients");

            } else
            {
                return AccessDeniedView();
            }
        }

        public IActionResult ViewDeclarationForm(int formId)
        {
            if (UserIsAdmin())
            {
                var form = declarationFormRepo.GetForm(formId);
                return View(form);
            } else
            {
                return AccessDeniedView();
            }
        }

        public IActionResult Admin(string year, string month, string employeeName, string approved, string submitted, string totalhoursmonth, int totalhoursyear, string sortDate)

        {
            if (UserIsAdmin())
            {

                ViewBag.AllForms = declarationFormRepo.GetAllForms();
                ViewBag.Months = monthList;
                ViewBag.sortDate = sortDate;
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
                if (employeeName != null)
                {
                    employeeId = employeeRepo.GetEmployeeByName(employeeName).EmployeeId;
                }
                else
                {
                    employeeId = null;
                }
                return View(declarationFormRepo.GetFilteredForms(year, employeeId, month, approved, submitted, sortDate));
            } else
            {
                return AccessDeniedView();
            }
        }

        public IActionResult AdminWithEmployeeId(string employeeId)
        {
            if (UserIsAdmin())
            {
                ViewBag.AllForms = declarationFormRepo.GetAllForms();
                ViewBag.Months = monthList;
                var forms = declarationFormRepo.GetAllFormsOfUser(employeeId);
                return View("~/Views/Admin/Admin.cshtml", forms);
            } else
            {
                return AccessDeniedView();
            }


        }

        public IActionResult AdminWithMonthYear(string month, int year)
        {
            if (UserIsAdmin())
            {
                ViewBag.AllForms = declarationFormRepo.GetAllForms();
                ViewBag.Months = monthList;
                var forms = declarationFormRepo.GetAllFormsOfMonth(MonthConverter.ConvertMonthToInt(month));
                return View("~/Views/Admin/Admin.cshtml", forms);

            } else
            {
                return AccessDeniedView();
            }
        }



        public IActionResult EmployeeForms(string employeeId)
        {
            if (UserIsAdmin())
            {
                var forms = declarationFormRepo.GetAllFormsOfUser(employeeId);
                return View(forms);
            } else
            {
                return AccessDeniedView();
            }
        }

        public bool UserIsAdmin()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = employeeRepo.GetEmployee(userId);

            if (user.Role == 1)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public ViewResult AccessDeniedView()
        {
            return View("~/Views/Home/AccessDenied.cshtml");
        }

        public IActionResult CreateFormForUser()
        {
            ViewBag.Employees = employeeRepo.getEmployeeSelectList();
            return View();
        }

        [HttpPost]
        public IActionResult CreateFormForUser(string employeeId, string month, int year)
        {
            declarationFormRepo.CreateFormForUser(employeeId, month, year);
            return RedirectToAction("Admin");
        }

    }
}