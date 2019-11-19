using System.Collections.Generic;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public interface IDeclarationFormRepository
    {

        public DeclarationFormModel GetForm(int userId, string month);

        public List<DeclarationFormModel> GetAllForms(int userId);

    }
}