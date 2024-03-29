﻿
using Microsoft.EntityFrameworkCore;
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

        public async Task AddHourRows(int year, string month, int declarationFormId)
        {
            var entity = await context.HourRows.FirstOrDefaultAsync(h => h.DeclarationFormId == declarationFormId); // als er uberhaupt iets in de hourrows van het declaratieform staat maakt het niks meer aan. Kan dus voor bugs zorgen.
            if (entity == null)
            {
                var monthInt = MonthConverter.ConvertMonthToInt(month);
                month = MonthConverter.ConvertIntToMonth(monthInt);
                int days = DateTime.DaysInMonth(year, monthInt);
                for (var i = 1; i <= days; i++)
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

                    context.HourRows.Add(hourRow);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
