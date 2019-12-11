using Microsoft.AspNetCore.Http;
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
        List<EmployeeModel> GetFilteredNames();
        Task<EmployeeModel> GetEmployee(string id);
        Task<EditingEmployeeModel> GetEditingEmployee(string id);
        EmployeeModel GetEmployeeByName(string name);
        Task<List<EmployeeModel>> GetAllAccounts(string searchString);
        //Task EditEmployee(EmployeeModel employeeModel);
        Task DeleteEmployee(string id);
        Task<SelectList> getEmployeeSelectList();
        Task EditEmployeeMail(string employeeMailold, string employeeMailnew);
        Task UploadPicture(IFormFile picture, string userId);
        Task<bool> UserIsOneMonthInactive(string employeeId);
        Task CheckIfYearPassedForAllTrainees();
        Task UploadFile(IFormFile file, int formId);
        Task EditEmployee(EditingEmployeeModel employeeModel);
        Task CreateZipFile(int formId, string name);
        Task<bool> UserIsEmployeeOrTrainee();
        Task<bool> UserIsOutOfService();
        Task<bool> UserIsAdmin();
    }
}