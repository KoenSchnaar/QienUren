using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrenRegistratieQien.Data;

namespace UrenRegistratieQien.Repositories
{
    public class DeclarationFormRepository : IDeclarationFormRepository
    {
        private readonly ApplicationDbContext context;

        public DeclarationFormRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void GetForms()
        {

        }
    }
}
