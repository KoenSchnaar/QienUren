using System.Collections.Generic;
using System.Threading.Tasks;
using UrenRegistratieQien.DatabaseClasses;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public interface IHourRowRepository
    {
        //public List<HourRowModel> GetHourRows(string userId, int declarationFormId);

        Task AddHourRows(int year, string month, int declarationFormId);
    }
}