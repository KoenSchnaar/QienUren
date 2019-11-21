using System.Collections.Generic;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public interface IDeclarationFormRepository
    {
        public List<DeclarationFormModel> GetAllForms();
        public DeclarationFormModel GetFormByFormId(int formId);

        public DeclarationFormModel GetForm(string userId, string month);

        public List<DeclarationFormModel> GetAllFormsOfUser(string userId);

    }
}