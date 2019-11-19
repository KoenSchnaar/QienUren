
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrenRegistratieQien.Data;
using UrenRegistratieQien.DatabaseClasses;

namespace UrenRegistratieQien.Repositories
{
    public class HourRowRepository : IHourRowRepository
    {
        private readonly ApplicationDbContext context;

        public HourRowRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void GetHourRows()
        {
            
        }

        public void GenerateHourRows(int year, int month, int declarationFormId)
        {
            int days = DateTime.DaysInMonth(year, month);
            for (var i=1; i<days; i++)
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
            }
        }
    }
}
