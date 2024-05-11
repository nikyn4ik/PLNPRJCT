using Microsoft.EntityFrameworkCore;
using PRGRM.MDLS;
using HP;

namespace PRGRM
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Authorization> Authorization { get; set; }
        public DbSet<Users> Users { get; set; }
        //public DbSet<Certificate> Certificate { get; set; }
        //public DbSet<Confirmation> Confirmation { get; set; }
        //public DbSet<Delivery> Delivery { get; set; }
        //public DbSet<Orders> Orders { get; set; }
        //public DbSet<Shipment> Shipment { get; set; }
        //public DbSet<Storage> Storage { get; set; }
        //public DbSet<TypeProduct> TypeProduct { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PLNPRJCT;Trusted_Connection=True;");
        }
    }
}