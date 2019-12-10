using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrenRegistratieQien.Models
{
    public class AdminOverviewModel
    {
        public Months SelectMonths { get; set; }
        public Status SelectStatus { get; set; }
    }


    public enum Months
    {
        Januari, Februari, Maart, April, Mei, Juni, Juli, Augustus, September, Oktober, November, December
    }

    public enum Status 
    {
        Approved, Rejected, Pending
    }
}
