using System;
using System.Collections.Generic;
using System.Text;
using UrenRegistratieQien.Models;
using UrenRegistratieQien.Repositories;

namespace UrenRegistratieQienTest.Fakes
{
    class FakeDeclarationFormRepository : IDeclarationFormRepository
    {
        public void EditDeclarationForm(DeclarationFormModel formModel)
        {
            throw new NotImplementedException();
        }

        public List<DeclarationFormModel> GetAllForms()
        {
            throw new NotImplementedException();
        }

        public List<DeclarationFormModel> GetAllFormsOfMonth(int month)
        {
            throw new NotImplementedException();
        }

        public List<DeclarationFormModel> GetAllFormsOfUser(string userId)
        {
            throw new NotImplementedException();
        }

        public List<DeclarationFormModel> GetFilteredForms(string employeeId, string month, string approved, string submitted)
        {
            throw new NotImplementedException();
        }

        public DeclarationFormModel GetForm(string userId, string month)
        {
            throw new NotImplementedException();
        }

        public DeclarationFormModel GetForm(int declarationFormId, string userId)
        {
            throw new NotImplementedException();
        }

        public DeclarationFormModel GetFormByFormId(int formId)
        {
            throw new NotImplementedException();
        }

        public List<DeclarationFormModel> GetNotApprovedForms()
        {
            throw new NotImplementedException();
        }

        public void SubmitDeclarationForm(DeclarationFormModel formModel)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursHoliday(List<DeclarationFormModel> DeclarationFormList, string Month)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursOther(List<DeclarationFormModel> DeclarationFormList, string Month)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursOvertime(List<DeclarationFormModel> DeclarationFormList, string Month)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursSickness(List<DeclarationFormModel> DeclarationFormList, string Month)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursTraining(List<DeclarationFormModel> DeclarationFormList, string Month)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursVacation(List<DeclarationFormModel> DeclarationFormList, string Month)
        {
            throw new NotImplementedException();
        }

        public int TotalHoursWorked(List<DeclarationFormModel> DeclarationFormList, string Month)
        {
            throw new NotImplementedException();
        }
    }
}
