using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using UrenRegistratieQien.Models;
using UrenRegistratieQien.Repositories;

namespace UrenRegistratieQienTest.Fakes
{
    class FakeEmployeeRepository : IEmployeeRepository
    {
        public Task CheckIfYearPassedForAllTrainees()
        {
            throw new NotImplementedException();
        }

        public void CreateZipFile(int formId, string name)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEmployee(string id)
        {
            throw new NotImplementedException();
        }

        public Task EditEmployee(EditingEmployeeModel employeeModel)
        {
            throw new NotImplementedException();
        }

        public Task EditEmployeeMail(string employeeMailold, string employeeMailnew)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeModel>> GetAllAccounts(string searchString)
        {
            throw new NotImplementedException();
        }

        public Task<EditingEmployeeModel> GetEditingEmployee(string id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeModel> GetEmployee(string id)
        {
            throw new NotImplementedException();
        }

        public EmployeeModel GetEmployeeByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeModel>> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public Task<SelectList> getEmployeeSelectList()
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeModel>> GetFilteredNames()
        {
            throw new NotImplementedException();
        }

        public void UploadFile(IFormFile file, int formId)
        {
            throw new NotImplementedException();
        }

        public void UploadPicture(IFormFile picture, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserIsAdmin()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserIsEmployeeOrTrainee()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserIsOneMonthInactive(string employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserIsOutOfService()
        {
            throw new NotImplementedException();
        }
    }
}
