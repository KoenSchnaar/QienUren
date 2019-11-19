using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrenRegistratieQien.Data;
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
        public void GetEmployees()
        {

        }
        
        //eerst register via de website?? daarna deze koppelen ofzo?
        public void AddEmployee(EmployeeModel employee)
        {
            //opzoeken 
        }
    }
}
