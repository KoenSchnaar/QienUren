using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrenRegistratieQien.Models
{
    public class HourRowModel
    {
        public int HourRowId { get; set; }
        public int EmployeeId { get; set; }
        public string Date { get; set; }
        public int Worked { get; set; }
        public int Overtime { get; set; }
        public int Sickness { get; set; }
        public int Vacation { get; set; }
        public int Holiday { get; set; }
        public int Training { get; set; }
        public int Other { get; set; }
        public string OtherExplanation { get; set; }

    }
}
