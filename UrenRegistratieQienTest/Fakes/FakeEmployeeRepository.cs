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
            return null;
        }

        public Task EditEmployee(EditingEmployeeModel employeeModel)
        {
            return null;
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
            return Task.FromResult(new EditingEmployeeModel());
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
            return Task.FromResult(new List<EmployeeModel>());
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
            return Task.FromResult(true);
        }

        public Task<bool> UserIsEmployeeOrTrainee()
        {
            return Task.FromResult(true);
        }

        public Task<bool> UserIsOneMonthInactive(string employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserIsOutOfService()
        {
            return Task.FromResult(false);
        }
    }
}
