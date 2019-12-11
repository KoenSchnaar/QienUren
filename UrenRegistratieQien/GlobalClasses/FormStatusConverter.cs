using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrenRegistratieQien.GlobalClasses
{
    public static class FormStatusConverter
    {
        public static string ConvertApproved(string approved)
        {
            if (approved == "Goedgekeurd")
            {
                return "Approved";
            }
            else if (approved == "Afgekeurd")
            {
                return "Rejected";
            }
            else if (approved == "In Afwachting")
            {
                return "Pending";
            }
            else
            {
                return null;
            }
        }
        
        public static string ConvertSubmitted(string submitted)
        {
            if (submitted == "Ingediend")
            {
                return "true";
            }
            else if (submitted == "Niet ingediend")
            {
                return "false";
            } else
            {
                return null;
            }
        }
    }
}
