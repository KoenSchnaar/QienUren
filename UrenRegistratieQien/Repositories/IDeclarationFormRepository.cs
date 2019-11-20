using System.Collections.Generic;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public interface IDeclarationFormRepository
    {

        public DeclarationFormModel GetForm(string userId, string month);

        public List<DeclarationFormModel> GetAllForms(string userId);

    }
}