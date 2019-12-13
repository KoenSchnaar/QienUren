using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using UrenRegistratieQien.Data;
using UrenRegistratieQien.DatabaseClasses;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment he;
        private readonly UserManager<Employee> _userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public EmployeeRepository(ApplicationDbContext context, IWebHostEnvironment he, IHttpContextAccessor httpContextAccessor, UserManager<Employee> userManager = null)
        {
            this.context = context;
            this.he = he;
            _userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public EmployeeModel EntityToEmployeeModel(Employee entity)
        {
            var newModel = new EmployeeModel
            {
                EmployeeId = entity.Id,
                ClientId = entity.ClientId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Address = entity.Address,
                Phone = entity.PhoneNumber,
                Role = entity.Role,
                ZIPCode = entity.ZIPCode,
                Residence = entity.Residence
            };
            return newModel;
        }

        public async Task<List<EmployeeModel>> GetEmployees()
        {
            List<EmployeeModel> employeeModelList = new List<EmployeeModel>();
            foreach(Employee employee in await context.Employees.ToListAsync())
            {
                employeeModelList.Add(EntityToEmployeeModel(employee));
            }
            return employeeModelList;
        }

        public async Task<EmployeeModel> GetEmployee(string id)
        {
            var databaseEmployee = await context.Employees.SingleAsync(p => p.Id == id);
            return EntityToEmployeeModel(databaseEmployee);
        }

        public async Task<EditingEmployeeModel> GetEditingEmployee(string id)
        {
            var employee = await context.Employees.SingleAsync(p => p.Id == id);
            var client = await context.Clients.FirstAsync(p => p.ClientId == employee.ClientId);

            return new EditingEmployeeModel
            {
                ClientName = client.CompanyName,
                EmployeeId = employee.Id,
                ClientId = client.ClientId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Address = employee.Address,
                Phone = employee.PhoneNumber,
                Role = employee.Role,
                ZIPCode = employee.ZIPCode,
                Residence = employee.Residence
            };
        }

        public async Task<List<EmployeeModel>> GetAllAccounts(string searchString)
        {
            var AllEmployee = new List<EmployeeModel>();
            var TempEmployee = await context.Employees.Where(p => p.Role != 1).OrderBy(x => x.FirstName).ToListAsync();
            
            if (!(searchString == null))
            {
                searchString = searchString.ToLower();
                var filterEmployee = TempEmployee.Where(x => x.FirstName.ToLower().Contains(searchString) || x.LastName.ToLower().Contains(searchString) || (x.FirstName.ToLower() + ' ' + x.LastName.ToLower()).Contains(searchString)
                   || searchString == null).OrderBy(x => x.FirstName);
                foreach (var employee in filterEmployee)
                    AllEmployee.Add(EntityToEmployeeModel(employee));
            } else
            {
                foreach (var employee in TempEmployee)
                    AllEmployee.Add(EntityToEmployeeModel(employee));
            }



            return AllEmployee;
        }

        public EmployeeModel GetEmployeeByName(string name)
        {
            Employee returnEmployee = new Employee();
            foreach(Employee employee in context.Employees)
            {
                if(employee.FirstName+" "+employee.LastName == name)
                {
                    returnEmployee = employee;
                }
            }
            return EntityToEmployeeModel(returnEmployee);
        }

        public async Task DeleteEmployee(string id)
        {
            var employee = await context.Employees.SingleAsync(p => p.Id == id);
            List<int> declarationFormIds = new List<int>();

            foreach(DeclarationForm declarationForm in context.DeclarationForms)
            {
                if(employee.Id == declarationForm.EmployeeId)
                {
                    declarationFormIds.Add(declarationForm.DeclarationFormId);
                    context.DeclarationForms.Remove(declarationForm);
                }
            }
            foreach(HourRow hourRow in context.HourRows)
            {
                if (declarationFormIds.Contains(hourRow.DeclarationFormId))
                {
                    context.HourRows.Remove(hourRow);
                }
            }

            context.Employees.Remove(employee);

            await context.SaveChangesAsync();
        }

        public async Task EditEmployeeMail(string employeeMailold, string employeeMailnew)
        { 
            var dbEmp = await context.Employees.SingleAsync(p => p.Email == employeeMailold);
            dbEmp.Email = employeeMailnew;
            dbEmp.UserName = employeeMailnew;

            string mailNormalized = employeeMailnew.ToUpper();
            string usernameNormalized = employeeMailnew.ToUpper();

            dbEmp.NormalizedEmail = mailNormalized;
            dbEmp.NormalizedUserName = usernameNormalized;
            await context.SaveChangesAsync();
        }

        public async Task EditEmployee(EditingEmployeeModel employeeModel)
        {
            //fix client
            var client = context.Clients.First(p => p.CompanyName == employeeModel.ClientName);
            employeeModel.ClientId = client.ClientId;

            var databaseEmployee = await context.Employees.SingleAsync(p => p.Id == employeeModel.EmployeeId);
            var role = 0;
            DateTime startdaterole = DateTime.MinValue;

            if(employeeModel.RoleAsString == "Admin")
            {
                role = 1;
                startdaterole = DateTime.Now;
            }
            else if (employeeModel.RoleAsString == "Medewerker")
            {
                role = 2;
                startdaterole = DateTime.Now;
            }
            else if(employeeModel.RoleAsString == "Trainee")
            {
                role = 3;
                startdaterole = DateTime.Now;
            }
            else if (employeeModel.RoleAsString == "Inactief")
            {
                role = 4;
                startdaterole = DateTime.Now;
            }

            databaseEmployee.ClientId = employeeModel.ClientId;
            databaseEmployee.FirstName = employeeModel.FirstName;
            databaseEmployee.LastName = employeeModel.LastName;
            databaseEmployee.Email = employeeModel.Email;
            databaseEmployee.Address = employeeModel.Address;
            databaseEmployee.PhoneNumber = employeeModel.Phone;
            databaseEmployee.Role = role;
            databaseEmployee.ZIPCode = employeeModel.ZIPCode;
            databaseEmployee.Residence = employeeModel.Residence;
            databaseEmployee.StartDateRole = startdaterole;

            await context.SaveChangesAsync();

        }

        public async Task CheckIfYearPassedForAllTrainees()
        {
            List<Employee> traineeList = await context.Employees.Where(p => p.Role == 3).ToListAsync();

            foreach (Employee employee in traineeList)
            {
                if (DateTime.Now >= employee.DateRegistered.AddYears(1))
                {
                   employee.Role = 4; 
                }
            }
            await context.SaveChangesAsync();
        }

        public async Task<bool> UserIsOneMonthInactive(string employeeId)
        {
            var employee = await context.Employees.SingleAsync(e => e.Id == employeeId);
            var roleId = employee.Role;

            if (roleId == 4 && DateTime.Now >= employee.StartDateRole.AddMonths(1))
            {
                employee.OutOfService = true;
                return true;   
            }
            else
            {
                employee.OutOfService = false;
                return false;
            }
        }

        public async Task<bool> UserIsEmployeeOrTrainee()
        {
            var userId = _userManager.GetUserId(httpContextAccessor.HttpContext.User);
            var user = await GetEmployee(userId);
            
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
            var userId = _userManager.GetUserId(httpContextAccessor.HttpContext.User);
            bool outofservice = await UserIsOneMonthInactive(userId);

            if (outofservice == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<SelectList> getEmployeeSelectList()
        {
            var EmployeeList = new SelectList(await context.Employees.ToListAsync(), "Id", "FirstName");
            return EmployeeList;
        }

        public async Task<List<EmployeeModel>> GetFilteredNames()
        {
            var entities = await context.Employees.Where(p => p.Role != 1).OrderBy(df => df.FirstName).ToListAsync();
          
            List<EmployeeModel> EmployeeModelList = new List<EmployeeModel>();
            foreach (Employee employee in entities)
            {
                EmployeeModelList.Add(EntityToEmployeeModel(employee));
            }
            return EmployeeModelList;
        }

        public void UploadPicture(IFormFile picture, string userId)
        {
            if (picture != null)
            {
                var fileName = Path.Combine(he.WebRootPath + "/ProfilePictures", Path.GetFileName(userId +".png"));
                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    picture.CopyTo(stream);
                }
            }
        }

        public void UploadFile(IFormFile file, int formId)
        {
            if (file != null)
            {
                string name = Path.GetFileName(file.FileName);
                var fileName = Path.Combine(he.WebRootPath + "/Uploads", name);
                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                CreateZipFile(formId, name);
            }
        }
        public async Task<bool> UserIsAdmin()
        {
            var userId = _userManager.GetUserId(httpContextAccessor.HttpContext.User);
            var user = await GetEmployee(userId);
            if (user.Role == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CreateZipFile(int formId, string name)
        {
            string sourceFileName = name;
            string sourceFolder = he.WebRootPath + "/Uploads";
            string zipFilePath = Path.Combine(he.WebRootPath + "/Uploads", $"{formId}.zip");
            string filePath = sourceFolder + "/" + name;

            var mode = ZipArchiveMode.Update;
            if (!File.Exists(zipFilePath))
                mode = ZipArchiveMode.Create;

            using (ZipArchive archive = ZipFile.Open(zipFilePath, mode))
            {
                archive.CreateEntryFromFile(Path.Combine(sourceFolder, sourceFileName), $"{sourceFileName}");
            }
            File.Delete(filePath);
        }
    }
}
