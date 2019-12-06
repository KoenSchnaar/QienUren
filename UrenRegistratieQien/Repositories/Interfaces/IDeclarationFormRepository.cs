using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public interface IDeclarationFormRepository
    {
        public Task<List<DeclarationFormModel>> GetFilteredForms(string year, string employeeId, string month, string approved, string submitted, string sortDate);
        //public List<DeclarationFormModel> GetNotApprovedForms();
        public Task<List<DeclarationFormModel>> GetAllForms();
        public Task<DeclarationFormModel> GetForm(int formId);

        public Task<List<DeclarationFormModel>> GetAllFormsOfUser(string userId);
        public Task EditDeclarationForm(DeclarationFormModel formModel);
        public Task SubmitDeclarationForm(DeclarationFormModel formModel);

        public Task<bool> CheckIfIdMatches(string uniqueId);
        public Task<List<DeclarationFormModel>> GetAllFormsOfMonth(int month);
        public Task CreateForm(string employeeId);
        public Task<string> GenerateUniqueId();
        public Task ApproveForm(int formId);
        public Task RejectForm(int formId, string comment);
        public Task CalculateTotalHours(DeclarationFormModel decModel);
        public Task<TotalsModel> CalculateTotalHoursOfAll(List<DeclarationFormModel> DeclarationFormList, string Month, int Year);
        public Task CreateFormForUser(string EmployeeId, string month, int year);
        public Task ReopenForm(int formId);
        public Task<List<TotalsForChartModel>> TotalHoursForCharts();
        public Task DeleteDeclarationForm(int FormId);
        public DeclarationFormModel GetFormNotAsync(int formId);

    }
}