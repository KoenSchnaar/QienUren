using System.Collections.Generic;
using UrenRegistratieQien.DatabaseClasses;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public interface IHourRowRepository
    {
        public List<HourRowModel> GetHourRows(string userId, int declarationFormId);

        public void AddHourRows(int year, string month, int declarationFormId);
    }
}