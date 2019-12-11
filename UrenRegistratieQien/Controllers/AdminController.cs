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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace UrenRegistratieQien.Controllers
{
    //[Authorize(Policy = "AdminAccessPolicy")]
    public class AdminController : Controller
    {
        private readonly IDeclarationFormRepository declarationFormRepo;
        private readonly UserManager<Employee> _userManager;
        private readonly IEmployeeRepository employeeRepo;
        private readonly IClientRepository clientRepo;
        private readonly IHostingEnvironment he;

        public List<string> monthList { get; set; }

        public AdminController(IDeclarationFormRepository DeclarationFormRepo, 
            IEmployeeRepository EmployeeRepo, 
            IClientRepository ClientRepo,
            IHostingEnvironment he,
            UserManager<Employee> userManager = null)
        {

            _userManager = userManager;
            declarationFormRepo = DeclarationFormRepo;
            employeeRepo = EmployeeRepo;
            clientRepo = ClientRepo;
            this.he = he;
            monthList = new List<string> { "Januari", "Februari", "March", "April", "May", "June", "Juli", "August", "September", "October", "November", "December" };
        }

        public async Task<IActionResult> ReopenForm(int formId)
        {
            await declarationFormRepo.ReopenForm(formId);
            return RedirectToAction("Admin");
        }

        public async Task<IActionResult> ShowEmployees()
        {
            if (await employeeRepo.UserIsAdmin())
            {
                var employees = employeeRepo.GetFilteredNames();
                return View(employees);
            } else
            {
                return await AccessDeniedView();
            }
        }

        public async Task<IActionResult> ChangeEmployee(string EmployeeId)
        {
            if (await employeeRepo.UserIsAdmin())
            {
                var employee = await employeeRepo.GetEditingEmployee(EmployeeId);
                var editingEmployee = (EditingEmployeeModel)employee;

                var clients = clientRepo.GetAllClients();
                List<string> clientnames = new List<string>();
                foreach(ClientModel client in clients)
                {
                    clientnames.Add(client.CompanyName);
                }
                ViewBag.ListOfClients = clientnames;

                return View(employee);
            }
            else
            {
                return await AccessDeniedView();
            }
        }
        
        public async Task<IActionResult> DeleteEmployee(string employeeId)
        {
            if (await employeeRepo.UserIsAdmin())
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
        public async Task<IActionResult> EditEmployee(EditingEmployeeModel empModel)
        {
            if (await employeeRepo.UserIsAdmin())
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
            if (await employeeRepo.UserIsAdmin())
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
            if (await employeeRepo.UserIsAdmin())
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
            if (await employeeRepo.UserIsAdmin())
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
            if (await employeeRepo.UserIsAdmin())
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
            if (await employeeRepo.UserIsAdmin())
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
            if (await employeeRepo.UserIsAdmin())
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
            if (await employeeRepo.UserIsAdmin())
            {
                var form = await declarationFormRepo.GetForm(formId);
                return View(form);
            }else
            {
                return await AccessDeniedView();
            }
        }

        

        

        public async Task<IActionResult> Admin(string year, string month, string employeeName, string approved, string submitted, string totalhoursmonth, int totalhoursyear, string sortDate)
        {
            if (await employeeRepo.UserIsAdmin())
            {
                ViewBag.AllForms = await declarationFormRepo.GetAllForms();
                ViewBag.Months = monthList;
                ViewBag.sortDate = sortDate;
                var forms = await declarationFormRepo.GetAllForms();
                await employeeRepo.CheckIfYearPassedForAllTrainees();

                if (totalhoursyear == 0)
                {
                    totalhoursyear = DateTime.Now.Year;
                }
                ViewBag.TotalHours = await declarationFormRepo.CalculateTotalHoursOfAll(forms, totalhoursmonth, totalhoursyear);
      

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
            if (await employeeRepo.UserIsAdmin())
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
            if (await employeeRepo.UserIsAdmin())
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
            if (await employeeRepo.UserIsAdmin())
            {
                var forms = declarationFormRepo.GetAllFormsOfUser(employeeId);
                return View(forms);
            } else
            {
                return await AccessDeniedView();
            }
        }

        

        public async Task<ViewResult> AccessDeniedView()
        {
            return View("~/Views/Home/AccessDenied.cshtml");
        }

        public async Task<IActionResult> CreateFormForUser()
        {
            if (await employeeRepo.UserIsAdmin())
            {
                ViewBag.Employees = await employeeRepo.getEmployeeSelectList();
                return View();
            }
            else
            {
                return await AccessDeniedView();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFormForUser(string employeeId, string month, int year)
        {
            if (await employeeRepo.UserIsAdmin())
            {
                await declarationFormRepo.CreateFormForUser(employeeId, month, year);
                return RedirectToAction("CreateFormForUser");
            }
            else
            {
                return await AccessDeniedView();
            }
        }

        public async Task<IActionResult> DeleteDeclarationForm(int FormId)
        {
            if (await employeeRepo.UserIsAdmin())
            {
                await declarationFormRepo.DeleteDeclarationForm(FormId);
                return RedirectToAction("Admin");
            }
            else
            {
                return await AccessDeniedView();
            }
        }

        public async Task<IActionResult> Charts(int year)
        {
            List<TotalsForChartModel> lstModel = await declarationFormRepo.TotalHoursForCharts(year);
            ViewBag.years = await declarationFormRepo.GetAllYears();
            return View(lstModel);
        }

        [HttpGet]
        public async Task<IActionResult> SearchOverview(string searchString)
        {
            List<EmployeeModel> listAccounts = await employeeRepo.GetAllAccounts(searchString);
            return View(listAccounts);
        }
    }
}