using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrenRegistratieQien.DatabaseClasses;
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
        public string GenerateUniqueId();
        public Task ApproveForm(int formId);
        public Task RejectForm(int formId, string comment);
        public Task CalculateTotalHours(DeclarationFormModel decModel);
        public TotalsModel CalculateTotalHoursOfAll(List<DeclarationFormModel> DeclarationFormList, string Month, int Year);
        public Task CreateFormForUser(string EmployeeId, string month, int year);
        public Task ReopenForm(int formId);
        public Task<List<TotalsForChartModel>> TotalHoursForCharts(int year);
        public Task DeleteDeclarationForm(int FormId);
        public DeclarationFormModel GetFormNotAsync(int formId);
        public  Task<List<int>> GetAllYears();
        public bool IsNextMonth(string month1, string month2);
        public DeclarationFormModel GetFormFromGap(List<DeclarationFormModel> forms);
        public List<DeclarationFormModel> EntitiesToDeclarationFormModels(List<DeclarationForm> forms);
    }
}