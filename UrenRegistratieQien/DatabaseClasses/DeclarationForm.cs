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
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public string Approved { get; set; }
        public bool Submitted { get; set; }
        public string Comment { get; set; }

        public string uniqueId { get; set; }
        public int TotalWorkedHours { get; set; }
        public int TotalOvertime { get; set; }
        public int TotalSickness { get; set; }

        public int TotalVacation { get; set; }
        //public int TotalHoliday { get; set; }
        //public int TotalTraining { get; set; }
        //public int TotalOther { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
