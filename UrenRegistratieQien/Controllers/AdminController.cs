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
        public async Task<IActionResult> ReopenForm(int formId)
        {
            await declarationFormRepo.ReopenForm(formId);
            return RedirectToAction("Admin");
        }

        public async Task<IActionResult> ShowEmployees()
        {
            if (await UserIsAdmin())
            {
                var employees = await employeeRepo.GetEmployees();
                return View(employees);
            } else
            {
                return await AccessDeniedView();
            }
        }

        public async Task<IActionResult> ChangeEmployee(string EmployeeId)
        {
            if (await UserIsAdmin())
            {
                var employee = await employeeRepo.GetEmployee(EmployeeId);
                return View(employee);
            }
            else
            {
                return await AccessDeniedView();
            }
        }
        
        public async Task<IActionResult> DeleteEmployee(string employeeId)
        {
            if (await UserIsAdmin())
            {
                await employeeRepo.DeleteEmployee(employeeId);
                return RedirectToAction("ShowEmployees");
            }
            else
            {
                return await AccessDeniedView();
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(EmployeeModel empModel)
        {
            if (await UserIsAdmin())
            {
                await employeeRepo.EditEmployee(empModel);
                return RedirectToAction("ShowEmployees");
            }
            else
            {
                return await AccessDeniedView();
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditEmployeeMailAdres(string employeeMailold, string employeeMailnew)
        {
            await employeeRepo.EditEmployeeMail(employeeMailold, employeeMailnew);
            return RedirectToAction("ShowEmployees");
        }

        public async Task<IActionResult> ShowClients()
        {
            if (await UserIsAdmin())
            {
                var clients = clientRepo.GetAllClients();
                return View(clients);
            }
            else
            {
                return await AccessDeniedView();
            }
        }

        public async Task<IActionResult> AddClient()
        {
            if (await UserIsAdmin())
            {
                return View(new ClientModel());

            } else
            {
                return await AccessDeniedView();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(ClientModel clientModel)
        {
            if (await UserIsAdmin())
            {

                await clientRepo.AddNewClient(clientModel);
                return RedirectToAction("ShowClients");
            } else
            {
                return await AccessDeniedView();
            }
        }

        public async Task<IActionResult> ChangeClient(int clientId)
        {
            if (await UserIsAdmin())
            {
                return View(await clientRepo.GetClient(clientId));
            } else
            {
                return await AccessDeniedView();
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditClient(ClientModel clientModel)
        {
            if (await UserIsAdmin())
            {
                await clientRepo.EditAClient(clientModel);
                return RedirectToAction("ShowClients");
            } else
            {
                return await AccessDeniedView();
            }
        }
        public async Task<IActionResult> DeleteClient(int clientId)
        {
            if (await UserIsAdmin())
            {
                await clientRepo.DeleteClient(clientId);
                return RedirectToAction("ShowClients");
            } else
            {
                return await AccessDeniedView();
            }
        }

        public async Task<IActionResult> ViewDeclarationForm(int formId)
        {
            if (await UserIsAdmin())
            {
                var form = await declarationFormRepo.GetForm(formId);
                return View(form);
            } else
            {
                return await AccessDeniedView();
            }
        }

        public async Task<IActionResult> Admin(string year, string month, string employeeName, string approved, string submitted, string totalhoursmonth, int totalhoursyear, string sortDate)
        {
            if (await UserIsAdmin())
            {
                ViewBag.AllForms = await declarationFormRepo.GetAllForms();
                ViewBag.Months = monthList;
                ViewBag.sortDate = sortDate;
                var forms = await declarationFormRepo.GetAllForms();

                if (totalhoursyear == 0)
                {
                    totalhoursyear = DateTime.Now.Year;
                }
               
                ViewBag.TotalHoursWorked = await declarationFormRepo.TotalHoursWorked(forms, totalhoursmonth, totalhoursyear);
                ViewBag.TotalHoursOvertime = await declarationFormRepo.TotalHoursOvertime(forms, totalhoursmonth, totalhoursyear);
                ViewBag.TotalHoursSickness = await declarationFormRepo.TotalHoursSickness(forms, totalhoursmonth, totalhoursyear);
                ViewBag.TotalHoursVacation = await declarationFormRepo.TotalHoursVacation(forms, totalhoursmonth, totalhoursyear);
                ViewBag.TotalHoursHoliday = await declarationFormRepo.TotalHoursHoliday(forms, totalhoursmonth, totalhoursyear);
                ViewBag.TotalHoursTraining = await declarationFormRepo.TotalHoursTraining(forms, totalhoursmonth, totalhoursyear);
                ViewBag.TotalHoursOther = await declarationFormRepo.TotalHoursOther(forms, totalhoursmonth, totalhoursyear);

                string employeeId;
                if (employeeName != null)
                {
                    employeeId = employeeRepo.GetEmployeeByName(employeeName).EmployeeId;
                }
                else
                {
                    employeeId = null;
                }
                return View(await declarationFormRepo.GetFilteredForms(year, employeeId, month, approved, submitted, sortDate));
            } else
            {
                return await AccessDeniedView();
            }
        }

        public async Task<IActionResult> AdminWithEmployeeId(string employeeId)
        {
            if (await UserIsAdmin())
            {
                ViewBag.AllForms = await declarationFormRepo.GetAllForms();
                ViewBag.Months = monthList;
                var forms = declarationFormRepo.GetAllFormsOfUser(employeeId);
                return View("~/Views/Admin/Admin.cshtml", forms);
            } else
            {
                return await AccessDeniedView();
            }
        }

        public async Task<IActionResult> AdminWithMonthYear(string month, int year)
        {
            if (await UserIsAdmin())
            {
                ViewBag.AllForms = await declarationFormRepo.GetAllForms();
                ViewBag.Months = monthList;
                var forms = await declarationFormRepo.GetAllFormsOfMonth(MonthConverter.ConvertMonthToInt(month));
                return View("~/Views/Admin/Admin.cshtml", forms);
            } else
            {
                return await AccessDeniedView();
            }
        }

        public async Task<IActionResult> EmployeeForms(string employeeId)
        {
            if (await UserIsAdmin())
            {
                var forms = declarationFormRepo.GetAllFormsOfUser(employeeId);
                return View(forms);
            } else
            {
                return await AccessDeniedView();
            }
        }

        public async Task<bool> UserIsAdmin()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await employeeRepo.GetEmployee(userId);

            if (user.Role == 1)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public async Task<ViewResult> AccessDeniedView()
        {
            return View("~/Views/Home/AccessDenied.cshtml");
        }

        public async Task<IActionResult> CreateFormForUser()
        {
            ViewBag.Employees = await employeeRepo.getEmployeeSelectList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFormForUser(string employeeId, string month, int year)
        {
            await declarationFormRepo.CreateFormForUser(employeeId, month, year);
            return RedirectToAction("Admin");
        }

        public async Task<IActionResult> DeleteDeclarationForm(int FormId)
        {
            if (await UserIsAdmin())
            {
                await declarationFormRepo.DeleteDeclarationForm(FormId);
                return RedirectToAction("Admin");
            }
            else
            {
                return await AccessDeniedView();
            }
        }
    }
}