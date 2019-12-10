using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrenRegistratieQien.GlobalClasses
{
    public static class DayConverter
    {
        public static string DayToDag(string day)
        {
            switch (day)
            {
                case "Monday":
                    return "Maandag";
                case "Tuesday":
                    return "Dinsdag";
                case "Wednesday":
                    return "Woensdag";
                case "Thursday":
                    return "Donderdag";
                case "Friday":
                    return "Vrijdag";
                case "Saturday":
                    return "Zaterdag";
                case "Sunday":
                    return "Zondag";
                default:
                    return "Geen geldige dag opgegeven";
            }
        }
    }
}
