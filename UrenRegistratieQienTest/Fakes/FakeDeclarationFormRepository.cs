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

        public List<DeclarationFormModel> GetAllFormsOfUser(string userId)
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
    }
}
