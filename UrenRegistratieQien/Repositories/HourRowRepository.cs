
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrenRegistratieQien.Data;
using UrenRegistratieQien.DatabaseClasses;
using UrenRegistratieQien.GlobalClasses;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public class HourRowRepository : IHourRowRepository
    {
        private readonly ApplicationDbContext context;

        public HourRowRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public List<HourRowModel> GetHourRows(string userId, string month)
        {
            var entity = context.DeclarationForms.Single(h => h.EmployeeId == userId && h.Month == month);
            var hourRows = new List<HourRowModel>();

            foreach (var row in entity.HourRows)
            {
                var newRow = new HourRowModel
                {
                    EmployeeId = userId,
                    Date = row.Date,
                    Worked = row.Worked,
                    Overtime = row.Overtime,
                    Sickness = row.Sickness,
                    Vacation = row.Vacation,
                    Holiday = row.Holiday,
                    Training = row.Training,
                    Other = row.Other,
                    OtherExplanation = row.OtherExplanation
                };
                hourRows.Add(newRow);
            }
            return hourRows;
        }



        public List<HourRow> AddHourRows(int year, string month, int declarationFormId)
        {
            var hourRowList = new List<HourRow>();
            var monthInt = MonthConverter.ConvertMonthToInt(month);
            int days = DateTime.DaysInMonth(year, monthInt);
            for (var i = 1; i < days; i++)
            {

                HourRow hourRow = new HourRow
                {
                    Date = Convert.ToString(i) + "/" + Convert.ToString(month) + "/" + Convert.ToString(year),
                    Worked = 0,
                    Overtime = 0,
                    Sickness = 0,
                    Vacation = 0,
                    Holiday = 0,
                    Training = 0,
                    Other = 0,
                    OtherExplanation = "",
                    DeclarationFormId = declarationFormId
                };

                hourRowList.Add(hourRow);
                context.HourRows.Add(hourRow);
                context.SaveChanges();
            }
            return hourRowList;
        }
    }
}
