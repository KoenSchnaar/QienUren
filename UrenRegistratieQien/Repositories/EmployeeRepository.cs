using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IHostingEnvironment he;

        public EmployeeRepository(ApplicationDbContext context, IHostingEnvironment he)
        {
            this.context = context;
            this.he = he;
        }
        public async Task<List<EmployeeModel>> GetEmployees()
        {
            List<EmployeeModel> employeeModelList = new List<EmployeeModel>();
            foreach(IdentityUser employee in context.Users)
            {
                var employeeCasted = (Employee)employee;

                EmployeeModel newEmployee = new EmployeeModel
                {
                    EmployeeId = employee.Id,
                    ClientId = employeeCasted.ClientId,
                    FirstName = employeeCasted.FirstName,
                    LastName = employeeCasted.LastName,
                    Email = employeeCasted.Email,
                    Address = employeeCasted.Address,
                    Phone = employeeCasted.Phone,
                    Role = employeeCasted.Role
                };

                employeeModelList.Add(newEmployee);

            }

            return employeeModelList;
        }

        public async Task<EmployeeModel> GetEmployee(string id)
        {
            var employee = context.Users.Single(p => p.Id == id);

            var employeeCasted = (Employee)employee;

            return new EmployeeModel
            {
                EmployeeId = employee.Id,
                ClientId = employeeCasted.ClientId,
                FirstName = employeeCasted.FirstName,
                LastName = employeeCasted.LastName,
                Email = employeeCasted.Email,
                Address = employeeCasted.Address,
                Phone = employeeCasted.Phone,
                Role = employeeCasted.Role,
                ZIPCode = employeeCasted.ZIPCode,
                Residence = employeeCasted.Residence
            };
        }

        public async Task<List<EmployeeModel>> GetAllAccounts(string searchString)
        {
            var Allemployee = new List<EmployeeModel>();
           
            var TempEmployee = context.Employees.Where(p => p.Role != 1);

            foreach (var employee in await TempEmployee.Where
                (x => x.FirstName.Contains(searchString) || x.LastName.Contains(searchString) || (x.FirstName + ' ' + x.LastName).Contains(searchString)
               || searchString == null).OrderBy(x => x.FirstName).ToListAsync())

                Allemployee.Add(new EmployeeModel
                {
                    EmployeeId = employee.Id,
                    ClientId = employee.ClientId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Address = employee.Address,
                    Phone = employee.Phone,
                    Role = employee.Role,
                    ZIPCode = employee.ZIPCode,
                    Residence = employee.Residence
                });

            return Allemployee;
        }


        public EmployeeModel GetEmployeeByName(string name)
        {
            var employees = context.Users;
            Employee returnEmployee = new Employee();
            foreach(Employee employee in employees)
            {
                if(employee.FirstName+" "+employee.LastName == name)
                {
                    returnEmployee = employee;
                }
            }
            return new EmployeeModel
            {
                EmployeeId = returnEmployee.Id,
                ClientId = returnEmployee.ClientId,
                FirstName = returnEmployee.FirstName,
                LastName = returnEmployee.LastName,
                Email = returnEmployee.Email,
                Address = returnEmployee.Address,
                Phone = returnEmployee.Phone,
                Role = returnEmployee.Role,
                ZIPCode = returnEmployee.ZIPCode,
                Residence = returnEmployee.Residence
            };
        }

        public async Task DeleteEmployee(string id)
        {
            var employee = context.Users.Single(p => p.Id == id);

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



            context.Users.Remove(employee);
            context.SaveChanges();
        }

        public async Task EditEmployeeMail(string employeeMailold, string employeeMailnew)
        {
             
            var dbEmp = context.Users.Single(p => p.Email == employeeMailold);
            var CastedDatabaseEmployee = (Employee)dbEmp;
            CastedDatabaseEmployee.Email = employeeMailnew;
            CastedDatabaseEmployee.UserName = employeeMailnew;

            string mailNormalized = employeeMailnew.ToUpper();
            string usernameNormalized = employeeMailnew.ToUpper();

            CastedDatabaseEmployee.NormalizedEmail = mailNormalized;
            CastedDatabaseEmployee.NormalizedUserName = usernameNormalized;
            context.SaveChanges();
        }

        public async Task EditEmployee(EmployeeModel employeeModel)
        {
            var databaseEmployee = context.Users.Single(p => p.Id == employeeModel.EmployeeId);
            var CastedDatabaseEmployee = (Employee)databaseEmployee;
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

            CastedDatabaseEmployee.ClientId = employeeModel.ClientId;
            CastedDatabaseEmployee.FirstName = employeeModel.FirstName;
            CastedDatabaseEmployee.LastName = employeeModel.LastName;
            CastedDatabaseEmployee.Email = employeeModel.Email;
            CastedDatabaseEmployee.Address = employeeModel.Address;
            CastedDatabaseEmployee.Phone = employeeModel.Phone;
            CastedDatabaseEmployee.Role = role;
            CastedDatabaseEmployee.ZIPCode = employeeModel.ZIPCode;
            CastedDatabaseEmployee.Residence = employeeModel.Residence;
            CastedDatabaseEmployee.StartDateRole = startdaterole;

            context.SaveChanges();

        }

        public async Task CheckIfYearPassedForAllTrainees()
        {
            List<Employee> traineeList = context.Employees.Where(p => p.Role == 3).ToList();

            foreach (Employee employee in traineeList)
            {
                if (DateTime.Now >= employee.DateRegistered.AddYears(1))
                {
                   employee.Role = 4; 
                }
            }
            context.SaveChanges();
        }

        public async Task<bool> UserIsOneMonthInactive(string employeeId)
        {
            var employee = context.Employees.Single(e => e.Id == employeeId);
            var roleId = employee.Role;

            if (roleId == 4 && DateTime.Now >= employee.StartDateRole.AddMonths(1))
            {
                return employee.OutOfService = true;   
            }
            else
            {
                return employee.OutOfService = false;
            }
        }

        public async Task<SelectList> getEmployeeSelectList()
        {
            var EmployeeList = new SelectList(context.Employees, "Id", "FirstName");

            return EmployeeList;
        }

        public List<EmployeeModel> GetFilteredNames()
        {
            var entities = context.Employees.Where(p => p.Role != 1).OrderBy(df => df.FirstName).ToList();
          
            List<EmployeeModel> EmployeeModelList = new List<EmployeeModel>();
            foreach (Employee employee in entities)
            {
                var EmployeeModel = new EmployeeModel
                {

                    EmployeeId = employee.Id,
                    ClientId = employee.ClientId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Address = employee.Address,
                    Phone = employee.Phone,
                    Role = employee.Role,
                    ZIPCode = employee.ZIPCode,
                    Residence = employee.Residence
                };
                EmployeeModelList.Add(EmployeeModel);



            }
            return EmployeeModelList;
        }
        public async Task<List<string>> getEmployeeNames()
        {
            throw new NotImplementedException();
        }

        public async Task UploadPicture(IFormFile picture, string userId)
        {
            if (picture != null)
            {
                var fileName = Path.Combine(he.WebRootPath + "/ProfilePictures", Path.GetFileName(userId +".png"));
                picture.CopyTo(new FileStream(fileName, FileMode.Create));
            }
        }

        public async Task UploadFile(IFormFile file, int formId)
        {
            if (file != null)
            {
                var fileName = Path.Combine(he.WebRootPath + "/Uploads", formId+"-" + Path.GetFileName(file.FileName));
                file.CopyTo(new FileStream(fileName, FileMode.Create));
            }
        }
    }
}
