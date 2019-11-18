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
        public int EmployeeId { get; set; }
        public string Month { get; set; }
        public bool Approved { get; set; }
        public bool Submitted { get; set; }
        public string Comment { get; set; }
    }
}
