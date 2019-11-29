using System;
using System.Collections.Generic;
using System.Text;
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

        public bool CheckIfIdMatches(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public void CreateForm(string employeeId)
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

        public DeclarationFormModel GetFormByFormId(int formId)
        {
            return new DeclarationFormModel
            {
                FormId = formId
            };
        }

        public List<DeclarationFormModel> GetNotApprovedForms()
        {
            throw new NotImplementedException();
        }

        public void RejectForm(int formId, string comment)
        {
            throw new NotImplementedException();
        }

        public void SubmitDeclarationForm(DeclarationFormModel formModel)
        {

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
    }
}
