using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UrenRegistratieQien.DatabaseClasses;

namespace UrenRegistratieQien.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }

        DbSet<Employee> Employees { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<HourRow> HourRows { get; set; }
        DbSet<DeclarationForm> DeclarationForms { get; set; }
    }
}
