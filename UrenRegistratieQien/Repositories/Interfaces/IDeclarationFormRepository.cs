using System;
using System.Collections.Generic;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public interface IDeclarationFormRepository
    {
        public List<DeclarationFormModel> GetFilteredForms(string year, string employeeId, string month, string approved, string submitted);
        public List<DeclarationFormModel> GetNotApprovedForms();
        public List<DeclarationFormModel> GetAllForms();
        public DeclarationFormModel GetFormByFormId(int formId);

        public DeclarationFormModel GetForm(int declarationFormId, string userId);
        public List<DeclarationFormModel> GetAllFormsOfUser(string userId);
        public void EditDeclarationForm(DeclarationFormModel formModel);
        public void SubmitDeclarationForm(DeclarationFormModel formModel);
        public int TotalHoursWorked(List<DeclarationFormModel> DeclarationFormList, string Month, int Year);
        public int TotalHoursOvertime(List<DeclarationFormModel> DeclarationFormList, string Month, int Year);
        public int TotalHoursSickness(List<DeclarationFormModel> DeclarationFormList, string Month, int Year);
        public int TotalHoursVacation(List<DeclarationFormModel> DeclarationFormList, string Month, int Year);
        public int TotalHoursHoliday(List<DeclarationFormModel> DeclarationFormList, string Month, int Year);
        public int TotalHoursTraining(List<DeclarationFormModel> DeclarationFormList, string Month, int Year);
        public int TotalHoursOther(List<DeclarationFormModel> DeclarationFormList, string Month, int Year);

        public bool CheckIfIdMatches(string uniqueId);

        public List<DeclarationFormModel> GetAllFormsOfMonth(int month);
        public void CreateForm(string employeeId);
        public string GenerateUniqueId();
        public void ApproveForm(int formId);
        public void RejectForm(int formId, string comment);
        //public Tuple<int, int, int> CalculateTotalHours(DeclarationFormModel decModel);
        public void CalculateTotalHours(DeclarationFormModel decModel);


    }
}