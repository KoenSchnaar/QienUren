using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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

        public EmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
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


        public async Task EditEmployee(EmployeeModel employeeModel)
        {
            var databaseEmployee = context.Users.Single(p => p.Id == employeeModel.EmployeeId);
            var CastedDatabaseEmployee = (Employee)databaseEmployee;
            var role = 0;

            if(employeeModel.RoleAsString == "Admin")
            {
                role = 1;
            }
            else if (employeeModel.RoleAsString == "Medewerker")
            {
                role = 2;
            }
            else if(employeeModel.RoleAsString == "Trainee")
            {
                role = 3;
            }
            else if (employeeModel.RoleAsString == "Inactief")
            {
                role = 4;
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

            context.SaveChanges();

        }



        public async Task CheckIfYearPassedForAllTrainees()
        {
            List<Employee> Trainees = new List<Employee>();
            foreach(Employee employee in context.Users)
            {
                var roleBridge = context.UserRoles.Single(p => p.UserId == employee.Id);



                var roleName = context.Roles.Single(p => p.Id == roleBridge.RoleId).Name;

                if(roleName == "Trainee")
                {
                    var Date = DateTime.Now;

                    if (employee.DateRegistered == Date.AddYears(-1))
                    {
                        roleBridge.RoleId = "4";

                        context.SaveChanges();
                    }
                }
            }
        }

        public async Task<SelectList> getEmployeeSelectList()
        {
            var EmployeeList = new SelectList(context.Employees, "Id", "FirstName");

            return EmployeeList;
        }

        public async Task<List<string>> getEmployeeNames()
        {
            throw new NotImplementedException();
        }
    }
}
