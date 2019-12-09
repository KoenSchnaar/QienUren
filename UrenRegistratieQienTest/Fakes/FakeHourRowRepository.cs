using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrenRegistratieQien.DatabaseClasses;
using UrenRegistratieQien.Models;
using UrenRegistratieQien.Repositories;

namespace UrenRegistratieQienTest.Fakes
{
    class FakeHourRowRepository : IHourRowRepository

    {
        public List<HourRow> AddHourRows(int year, string month, int declarationFormId)
        {
            throw new NotImplementedException();
        }

        public List<HourRowModel> GetHourRows(string userId, string month)
        {
            throw new NotImplementedException();
        }

        public List<HourRowModel> GetHourRows(string userId, int declarationFormId)
        {
            throw new NotImplementedException();
        }

        Task IHourRowRepository.AddHourRows(int year, string month, int declarationFormId)
        {
            throw new NotImplementedException();
        }
    }
}
