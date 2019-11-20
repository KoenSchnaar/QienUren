using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrenRegistratieQien.GlobalClasses
{
    public static class MonthConverter
    {
        public static int ConvertMonthToInt(string month)
        {
            
            switch (month)
            {
                case "januari": case "Januari":
                    return 1;
                case "februari": case "Februari":
                    return 2;
                case "march": case "March":
                    return 3;
                case "april": case "April":
                    return 4;
                case "may": case "May":
                    return 5;
                case "juni": case "Juni":
                    return 6;
                case "juli": case "Juli":
                    return 7;
                case "august": case "August":
                    return 8;
                case "september": case "September":
                    return 9;
                case "october": case "October":
                    return 10;
                case "november": case "November":
                    return 11;
                case "december": case "December":
                    return 12;
                default:
                    return 0;

            }

        }
    }
}
