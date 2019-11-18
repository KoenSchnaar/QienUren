using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrenRegistratieQien.DatabaseClasses
{
    public class Client
    {
        public int ClientId { get; set; }
        public string CompanyName { get; set; }
        public string Contact1Name { get; set; }
        public string Contact2Name { get; set; }
        public int Contact1Phone { get; set; }
        public int Contact2Phone { get; set; }
        public string Contact1Email { get; set; }
        public string Contact2Email { get; set; }
        public int CompanyPhone { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
