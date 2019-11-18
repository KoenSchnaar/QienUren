using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrenRegistratieQien.DatabaseClasses
{
    public class DeclarationForm
    {
        public int DeclarationFormId { get; set; }
        public List<HourRow> HourRows { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string Month { get; set; }
        public bool Approved { get; set; }
        public bool Submitted { get; set; }
        public string Comment { get; set; }
    }
}
