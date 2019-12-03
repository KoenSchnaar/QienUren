using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeModel>> GetEmployees();
        Task<List<string>> getEmployeeNames();

        Task<EmployeeModel> GetEmployee(string id);
        EmployeeModel GetEmployeeByName(string name);

        Task EditEmployee(EmployeeModel employeeModel);
        Task DeleteEmployee(string id);
        Task<SelectList> getEmployeeSelectList();
    }
}