using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrenRegistratieQien.Models
{
    public class AdminOverviewModel
    {
        public List<DeclarationFormModel> declarationFormModels { get; set; }
        public int employeeId { get; set; }
        public string month { get; set; }
        public bool approved { get; set; }
        public bool submitted { get; set; }
        public int year { get; set; }
    }
}
