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
                    EmployeeId = Convert.ToInt32(employee.Id),
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
                EmployeeId = Convert.ToInt32(employee.Id),
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
                if(Convert.ToInt32(employee.Id) == declarationForm.EmployeeId) //
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
    }
}
