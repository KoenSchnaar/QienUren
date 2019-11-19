using System.Collections.Generic;
using UrenRegistratieQien.DatabaseClasses;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public interface IHourRowRepository
    {
        public List<HourRowModel> GetHourRows(int userId, string month);
        public List<HourRow> AddHourRows(int year, int month, int declarationFormId);
    }
}