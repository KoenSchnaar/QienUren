using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrenRegistratieQien.Models
{
    public class AdminOverviewModel
    {
        public Months SelectMonths { get; set; }
    }

    public enum Months
    {
        Januari, Februari, March, April, May, June, Juli, August, September, October, November, December
    }
}
