using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrenRegistratieQien.Exceptions
{
    public class MoreThan24HoursException: ApplicationException 
    {
        public MoreThan24HoursException() : base("Error: Het dagtotaal mag niet meer dan 24 uur zijn.")
        {
        }
    }
}
