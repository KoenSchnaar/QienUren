using System;
using System.Collections.Generic;
using System.Text;
using UrenRegistratieQien.Models;
using UrenRegistratieQien.Repositories;

namespace UrenRegistratieQienTest.Fakes
{
    class FakeEmployeeRepository : IEmployeeRepository
    {
        public void DeleteEmployee(string id)
        {

            string EmployeeId = id;

        }

        public void EditEmployee(EmployeeModel employeeModel)
        {
            var implemented = true;
        }

        public EmployeeModel GetEmployee(string id)
        {
            return new EmployeeModel
            {
                EmployeeId = id
            };
        }

        public EmployeeModel GetEmployeeByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<EmployeeModel> GetEmployees()
        {
            return new List<EmployeeModel>();
        }

        public void UpdateEmployee(EmployeeModel employeeModel)
        {
            throw new NotImplementedException();
        }
    }
}
