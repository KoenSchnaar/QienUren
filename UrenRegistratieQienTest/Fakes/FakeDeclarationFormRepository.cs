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
        public void ApproveForm(int formId)
        {
            throw new NotImplementedException();
        }

        public void CalculateTotalHours(DeclarationFormModel decModel)
        {
            throw new NotImplementedException();
        }

        public Task<TotalsModel> CalculateTotalHoursOfAll(List<DeclarationFormModel> DeclarationFormList, string Month, int Year)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfIdMatches(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public void CreateForm(string employeeId)
        {
            throw new NotImplementedException();
        }

        public void CreateFormForUser(DeclarationFormModel inputModel)
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

        public void EditDeclarationForm(DeclarationFormModel formModel)
        {

        }

        public string GenerateUniqueId()
        {
            throw new NotImplementedException();
        }

        public List<DeclarationFormModel> GetAllForms()
        {
            return new List<DeclarationFormModel>();
        }

        public List<DeclarationFormModel> GetAllFormsOfMonth(int month)
        {
            throw new NotImplementedException();
        }

        public List<DeclarationFormModel> GetAllFormsOfUser(string userId)
        {
            var declarationFormModel = new DeclarationFormModel { EmployeeId = userId };
            var declarationFormList = new List<DeclarationFormModel>();
            declarationFormList.Add(declarationFormModel);
            return declarationFormList;
        }

        public List<DeclarationFormModel> GetFilteredForms(string employeeId, string month, string approved, string submitted)
        {
            return new List<DeclarationFormModel>();
        }

        public List<DeclarationFormModel> GetFilteredForms(string year, string employeeId, string month, string approved, string submitted)
        {
            throw new NotImplementedException();
        }

        public Task<List<DeclarationFormModel>> GetFilteredForms(string year, string employeeId, string month, string approved, string submitted, string sortDate)
        {
            throw new NotImplementedException();
        }

        public DeclarationFormModel GetForm(string userId, string month)
        {
            return new DeclarationFormModel();
        }

        public DeclarationFormModel GetForm(int declarationFormId, string userId)
        {
            return new DeclarationFormModel
            {
                FormId = declarationFormId,
                EmployeeId = userId,
                EmployeeName = "TestName",
                Month = "Januari"
            };
        }

        public DeclarationFormModel GetForm(int formId)
        {
            throw new NotImplementedException();
        }

        public DeclarationFormModel GetFormByFormId(int formId)
        {
            return new DeclarationFormModel
            {
                FormId = formId
            };
        }

        public DeclarationFormModel GetFormNotAsync(int formId)
        {
            throw new NotImplementedException();
        }

        public List<DeclarationFormModel> GetNotApprovedForms()
        {
            throw new NotImplementedException();
        }

        public void RejectForm(int formId, string comment)
        {
            throw new NotImplementedException();
        }

        public Task ReopenForm(int formId)
        {
            throw new NotImplementedException();
        }

        public void SubmitDeclarationForm(DeclarationFormModel formModel)
        {

        }

        public Task<List<TotalsForChartModel>> TotalHoursForCharts()
        {
            throw new NotImplementedException();
        }

        public int TotalHoursHoliday(List<DeclarationFormModel> DeclarationFormList, string Month)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursHoliday(List<DeclarationFormModel> DeclarationFormList, string Month, int Year)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursOther(List<DeclarationFormModel> DeclarationFormList, string Month)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursOther(List<DeclarationFormModel> DeclarationFormList, string Month, int Year)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursOvertime(List<DeclarationFormModel> DeclarationFormList, string Month)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursOvertime(List<DeclarationFormModel> DeclarationFormList, string Month, int Year)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursSickness(List<DeclarationFormModel> DeclarationFormList, string Month)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursSickness(List<DeclarationFormModel> DeclarationFormList, string Month, int Year)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursTraining(List<DeclarationFormModel> DeclarationFormList, string Month)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursTraining(List<DeclarationFormModel> DeclarationFormList, string Month, int Year)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursVacation(List<DeclarationFormModel> DeclarationFormList, string Month)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursVacation(List<DeclarationFormModel> DeclarationFormList, string Month, int Year)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursWorked(List<DeclarationFormModel> DeclarationFormList, string Month)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursWorked(List<DeclarationFormModel> DeclarationFormList, string Month, int Year)
        {
            throw new NotImplementedException();
        }

        Task IDeclarationFormRepository.ApproveForm(int formId)
        {
            throw new NotImplementedException();
        }

        Task IDeclarationFormRepository.CalculateTotalHours(DeclarationFormModel decModel)
        {
            throw new NotImplementedException();
        }

        Task<bool> IDeclarationFormRepository.CheckIfIdMatches(string uniqueId)
        {
            throw new NotImplementedException();
        }

        Task IDeclarationFormRepository.CreateForm(string employeeId)
        {
            throw new NotImplementedException();
        }

        Task IDeclarationFormRepository.EditDeclarationForm(DeclarationFormModel formModel)
        {
            throw new NotImplementedException();
        }

        Task<string> IDeclarationFormRepository.GenerateUniqueId()
        {
            throw new NotImplementedException();
        }

        Task<List<DeclarationFormModel>> IDeclarationFormRepository.GetAllForms()
        {
            throw new NotImplementedException();
        }

        Task<List<DeclarationFormModel>> IDeclarationFormRepository.GetAllFormsOfMonth(int month)
        {
            throw new NotImplementedException();
        }

        Task<List<DeclarationFormModel>> IDeclarationFormRepository.GetAllFormsOfUser(string userId)
        {
            throw new NotImplementedException();
        }

        Task<DeclarationFormModel> IDeclarationFormRepository.GetForm(int formId)
        {
            throw new NotImplementedException();
        }

        Task IDeclarationFormRepository.RejectForm(int formId, string comment)
        {
            throw new NotImplementedException();
        }

        Task IDeclarationFormRepository.SubmitDeclarationForm(DeclarationFormModel formModel)
        {
            throw new NotImplementedException();
        }
    }
}
