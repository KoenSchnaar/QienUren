﻿using System.Collections.Generic;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public interface IHourRowRepository
    {
        public List<HourRowModel> GetHourRows(int userId, string month);

        public void AddHourRows(int year, int month, int declarationFormId);
    }
}