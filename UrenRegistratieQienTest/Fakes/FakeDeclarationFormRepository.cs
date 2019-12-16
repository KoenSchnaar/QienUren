using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrenRegistratieQien.Models;
using UrenRegistratieQien.Repositories;

namespace UrenRegistratieQienTest.Fakes
{
    class FakeDeclarationFormRepository : IDeclarationFormRepository
    {
        public Task ApproveForm(int formId)
        {
            throw new NotImplementedException();
        }

        public Task CalculateTotalHours(DeclarationFormModel decModel)
        {
            throw new NotImplementedException();
        }

        public TotalsModel CalculateTotalHoursOfAll(List<DeclarationFormModel> DeclarationFormList, string Month, int Year)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckIfIdMatches(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public Task CreateForm(string employeeId)
        {
            throw new NotImplementedException();
        }

        public Task CreateFormForUser(string EmployeeId, string month, int year)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDeclarationForm(int FormId)
        {
            throw new NotImplementedException();
        }

        public Task EditDeclarationForm(DeclarationFormModel formModel)
        {
            throw new NotImplementedException();
        }

        public string GenerateUniqueId()
        {
            throw new NotImplementedException();
        }

        public Task<List<DeclarationFormModel>> GetAllForms()
        {
            throw new NotImplementedException();
        }

        public Task<List<DeclarationFormModel>> GetAllFormsOfMonth(int month)
        {
            throw new NotImplementedException();
        }

        public Task<List<DeclarationFormModel>> GetAllFormsOfUser(string userId)
        {
            var declarationFormModel = new DeclarationFormModel { EmployeeId = userId };
            var declarationFormList = new List<DeclarationFormModel>();
            declarationFormList.Add(declarationFormModel);
            return Task.FromResult(declarationFormList);
        }
        
        public Task<List<int>> GetAllYears()
        {
            throw new NotImplementedException();
        }

        public Task<List<DeclarationFormModel>> GetFilteredForms(string year, string employeeId, string month, string approved, string submitted, string sortDate)
        {
            throw new NotImplementedException();
        }

        public Task<DeclarationFormModel> GetForm(int formId)
        {
            return Task.FromResult(new DeclarationFormModel { FormId = formId });
        }

        public DeclarationFormModel GetFormNotAsync(int formId)
        {
            throw new NotImplementedException();
        }

        public Task RejectForm(int formId, string comment)
        {
            throw new NotImplementedException();
        }

        public Task ReopenForm(int formId)
        {
            throw new NotImplementedException();
        }

        public Task SubmitDeclarationForm(DeclarationFormModel formModel)
        {
            throw new NotImplementedException();
        }

        public Task<List<TotalsForChartModel>> TotalHoursForCharts(int year)
        {
            throw new NotImplementedException();
        }
    }
}
