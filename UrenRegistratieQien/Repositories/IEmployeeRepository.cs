using System.Collections.Generic;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public interface IEmployeeRepository
    {
        List<EmployeeModel> GetEmployees();

        EmployeeModel GetEmployee(string id);

        void UpdateEmployee(EmployeeModel employeeModel);
    }
}