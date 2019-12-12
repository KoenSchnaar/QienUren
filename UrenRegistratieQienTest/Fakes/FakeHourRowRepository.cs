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
        public Task AddHourRows(int year, string month, int declarationFormId)
        {
            throw new NotImplementedException();
        }
    }
}
