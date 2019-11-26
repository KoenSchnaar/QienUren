using System.Collections.Generic;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public interface IEmployeeRepository
    {
        List<EmployeeModel> GetEmployees();

        EmployeeModel GetEmployee(string id);
        public EmployeeModel GetEmployeeByName(string name);

        void UpdateEmployee(EmployeeModel employeeModel);
    }
}