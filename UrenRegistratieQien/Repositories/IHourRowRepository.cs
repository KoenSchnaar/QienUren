using System.Collections.Generic;
using UrenRegistratieQien.DatabaseClasses;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public interface IHourRowRepository
    {
        public void GenerateHourRows(int year, int month, int declarationFormId);
        public List<HourRowModel> GetHourRows(int userId, string month);
    }
}