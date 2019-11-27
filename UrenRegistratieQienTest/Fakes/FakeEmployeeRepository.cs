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
            throw new NotImplementedException();
        }

        public void EditEmployee(EmployeeModel employeeModel)
        {
            throw new NotImplementedException();
        }

        public EmployeeModel GetEmployee(string id)
        {
            throw new NotImplementedException();
        }

        public EmployeeModel GetEmployeeByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<EmployeeModel> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(EmployeeModel employeeModel)
        {
            throw new NotImplementedException();
        }
    }
}
