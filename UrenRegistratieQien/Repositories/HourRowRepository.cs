
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrenRegistratieQien.Data;

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
    }
}
