using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrenRegistratieQien.Data;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public class DeclarationFormRepository : IDeclarationFormRepository
    {
        private readonly ApplicationDbContext context;

        public DeclarationFormRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void GetForm()
        {

        }

        public List<DeclarationFormModel> GetAllForms(int userId)
        {
            var entities = context.DeclarationForms.Where(h => h.EmployeeId == userId).ToList();
            var forms = new List<DeclarationFormModel>();
            foreach (var form in forms)
            {
                new DeclarationFormModel
                {
                    FormId =

                    EmployeeId =
                    Month = 

                }
            }
        }
    }
}
