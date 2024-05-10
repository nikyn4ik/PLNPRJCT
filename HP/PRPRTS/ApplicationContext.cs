using Microsoft.EntityFrameworkCore;
using System;

namespace HP
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Authorization> Authorization { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PLNPRJCT;Trusted_Connection=True;");
        }
    }
}