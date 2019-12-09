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

        public void DeleteEmployee(string id)
        {

            string EmployeeId = id;

        }

        public void EditEmployee(EmployeeModel employeeModel)
        {
            var implemented = true;
        }

        public Task EditEmployeeMail(string employeeMailold, string employeeMailnew)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeModel>> GetAllAccounts(string searchString)
        {
            throw new NotImplementedException();
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

        public Task<List<string>> getEmployeeNames()
        {
            throw new NotImplementedException();
        }

        public List<EmployeeModel> GetEmployees()
        {
            return new List<EmployeeModel>();
        }

        public Task<SelectList> getEmployeeSelectList()
        {
            throw new NotImplementedException();
        }

        public List<EmployeeModel> GetFilteredNames()
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(EmployeeModel employeeModel)
        {
            throw new NotImplementedException();
        }

        public Task UploadFile(IFormFile file, int formId)
        {
            throw new NotImplementedException();
        }

        public Task UploadPicture(IFormFile picture, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserIsOneMonthInactive(string employeeId)
        {
            throw new NotImplementedException();
        }

        Task IEmployeeRepository.DeleteEmployee(string id)
        {
            throw new NotImplementedException();
        }

        Task IEmployeeRepository.EditEmployee(EmployeeModel employeeModel)
        {
            throw new NotImplementedException();
        }

        Task<EmployeeModel> IEmployeeRepository.GetEmployee(string id)
        {
            throw new NotImplementedException();
        }

        Task<List<EmployeeModel>> IEmployeeRepository.GetEmployees()
        {
            throw new NotImplementedException();
        }
    }
}
