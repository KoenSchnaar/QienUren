using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrenRegistratieQien.Models
{
    public class TotalsModel
    {
        public int TotalWorkedHours { get; set; }
        public int TotalOvertime { get; set; }
        public int TotalSickness { get; set; }
        public int TotalVacation { get; set; }
        public int TotalHoliday { get; set; }
        public int TotalTraining { get; set; }
        public int TotalOther { get; set; }
    }
}
