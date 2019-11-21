using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrenRegistratieQien.Models
{
    public class EmployeeModel
    {
        public string EmployeeId { get; set; }
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string Role { get; set; }
        public string ZIPCode { get; set; }
        public string Residence { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}
