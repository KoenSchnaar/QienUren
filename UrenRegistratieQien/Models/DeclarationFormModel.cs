using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrenRegistratieQien.Models
{
    
    public class DeclarationFormModel
    {
        public int FormId { get; set; }
        public List<HourRowModel> HourRows { get; set; }
        public string EmployeeId { get; set; }
        public int Year { get; set; }
        public string EmployeeName { get; set; }
        public string ClientName { get; set; }
        public string Month { get; set; }
        public int monthyear { get; set; }
        public string Approved { get; set; }
        public bool Submitted { get; set; }
        public string Comment { get; set; }
        public string uniqueId { get; set; }
        public int TotalWorkedHours { get; set; }
        public int TotalOvertime { get; set; }
        public int TotalSickness { get; set; }
        public int TotalVacation { get; set; }
        public int TotalHoliday { get; set; }
        public int TotalTraining { get; set; }
        public int TotalOther { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
