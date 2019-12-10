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
                case "march": case "March": case "Maart": case "maart":
                    return 3;
                case "april": case "April":
                    return 4;
                case "may": case "May": case "Mei": case "mei":
                    return 5;
                case "june": case "June": case "Juni": case "juni":
                    return 6;
                case "juli": case "Juli":
                    return 7;
                case "august": case "August": case "Augustus": case "augustus":
                    return 8;
                case "september": case "September":
                    return 9;
                case "october": case "October": case "Oktober": case "oktober":
                    return 10;
                case "november": case "November":
                    return 11;
                case "december": case "December":
                    return 12;
                default:
                    return 0;

            }

        }

        public static string ConvertIntToMonth(int month)
        {
            switch (month)
            {
                case 1:
                    return "Januari";
                case 2:
                    return "Februari";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "Juli";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return "None";
            }
        }
    }
}
