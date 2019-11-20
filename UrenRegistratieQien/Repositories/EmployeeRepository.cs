using Microsoft.AspNetCore.Identity;
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
        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> employeeModelList = new List<EmployeeModel>();
            foreach(IdentityUser employee in context.Users)
            {
                var employeeCasted = (Employee)employee;
                var roleBridge = context.UserRoles.Single(p => p.UserId == employee.Id);
                var roleName = context.Roles.Single(p => p.Id == roleBridge.RoleId).Name;

                EmployeeModel newEmployee = new EmployeeModel
                {
                    EmployeeId = employee.Id,
                    ClientId = employeeCasted.ClientId,
                    FirstName = employeeCasted.FirstName,
                    LastName = employeeCasted.LastName,
                    Email = employeeCasted.Email,
                    Address = employeeCasted.Address,
                    Phone = employeeCasted.Phone,
                    Role = roleName
                };

                employeeModelList.Add(newEmployee);

            }

            return employeeModelList;
        }

        public EmployeeModel GetEmployee(string id)
        {
            var employee = context.Users.Single(p => p.Id == id);

            var employeeCasted = (Employee)employee;
            var roleBridge = context.UserRoles.Single(p => p.UserId == id);
            var roleName = context.Roles.Single(p => p.Id == roleBridge.RoleId).Name;

            return new EmployeeModel
            {
                EmployeeId = employee.Id,
                ClientId = employeeCasted.ClientId,
                FirstName = employeeCasted.FirstName,
                LastName = employeeCasted.LastName,
                Email = employeeCasted.Email,
                Address = employeeCasted.Address,
                Phone = employeeCasted.Phone,
                Role = roleName
            };
            

        }

        public void DeleteEmployee(string id)
        {
            var employee = context.Users.Single(p => p.Id == id);
            

            foreach(DeclarationForm declarationForm in context.DeclarationForms)
            {
                if(employee.Id == declarationForm.EmployeeId) //
                {
                    foreach(HourRow hourRow in context.HourRows)
                    {
                        if(hourRow.DeclarationFormId == declarationForm.DeclarationFormId)
                        {
                            context.HourRows.Remove(hourRow);
                        }
                    }

                    context.DeclarationForms.Remove(declarationForm);
                }
            }

            context.Users.Remove(employee);
            context.SaveChanges();
        }


        public void UpdateEmployee(EmployeeModel employeeModel)
        {
            var databaseEmployee = context.Users.Single(p => p.Id == employeeModel.EmployeeId);
            var CastedDatabaseEmployee = (Employee)databaseEmployee;

            var UserRole = context.UserRoles.Single(p => employeeModel.EmployeeId == p.UserId);
            var roleId = context.Roles.Single(p => employeeModel.Role == p.Name).Id;

            CastedDatabaseEmployee.ClientId = employeeModel.ClientId;
            CastedDatabaseEmployee.FirstName = employeeModel.FirstName;
            CastedDatabaseEmployee.LastName = employeeModel.LastName;
            CastedDatabaseEmployee.Email = employeeModel.Email;
            CastedDatabaseEmployee.Address = employeeModel.Address;
            CastedDatabaseEmployee.Phone = employeeModel.Phone;
            UserRole.RoleId = roleId;

            context.SaveChanges();

        }

        //nog toevoegen bij het createn: dateregistered
        //nog toevoegen bij edit user: dateregistered

        public void CheckIfYearPassedForAllTrainees()
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
    }
}
