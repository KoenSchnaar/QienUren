using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrenRegistratieQien.Data;
using UrenRegistratieQien.DatabaseClasses;

namespace UrenRegistratieQien.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void GetEmployees()
        {

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
