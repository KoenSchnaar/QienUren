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
        public Task<int> TotalHoursWorked(List<DeclarationFormModel> DeclarationFormList, string Month, int Year);
        public Task<int> TotalHoursOvertime(List<DeclarationFormModel> DeclarationFormList, string Month, int Year);
        public Task<int> TotalHoursSickness(List<DeclarationFormModel> DeclarationFormList, string Month, int Year);
        public Task<int> TotalHoursVacation(List<DeclarationFormModel> DeclarationFormList, string Month, int Year);
        public Task<int> TotalHoursHoliday(List<DeclarationFormModel> DeclarationFormList, string Month, int Year);
        public Task<int> TotalHoursTraining(List<DeclarationFormModel> DeclarationFormList, string Month, int Year);
        public Task<int> TotalHoursOther(List<DeclarationFormModel> DeclarationFormList, string Month, int Year);

        public Task<bool> CheckIfIdMatches(string uniqueId);

        public Task<List<DeclarationFormModel>> GetAllFormsOfMonth(int month);
        public Task CreateForm(string employeeId);
        public Task<string> GenerateUniqueId();
        public Task ApproveForm(int formId);
        public Task RejectForm(int formId, string comment);
        public Task CalculateTotalHours(DeclarationFormModel decModel);
        public Task CreateFormForUser(string EmployeeId, string month, int year);
        public Task ReopenForm(int formId);

        public Task DeleteDeclarationForm(int FormId);
    }
}