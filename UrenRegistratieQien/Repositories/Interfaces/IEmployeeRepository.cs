using System.Collections.Generic;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public interface IEmployeeRepository
    {
        List<EmployeeModel> GetEmployees();

        EmployeeModel GetEmployee(string id);
        public EmployeeModel GetEmployeeByName(string name);

        public void EditEmployeeMail(string employeeMailold, string employeeMailnew);
        public void EditEmployee(EmployeeModel employeeModel);
        public void DeleteEmployee(string id);
    }
}