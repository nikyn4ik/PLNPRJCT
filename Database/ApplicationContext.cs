using Database.MDLS;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Authorization> Authorization { get; set; }
        public DbSet<Certificate> Certificate { get; set; }
        public DbSet<Consignee> Consignee { get; set; }
        public DbSet<Container> Container { get; set; }
        public DbSet<Defects> Defects { get; set; }
        public DbSet<Delivery> Delivery { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Payer> Payer { get; set; }
        public DbSet<Shipment> Shipment { get; set; }
        public DbSet<Storage> Storage { get; set; }
        public DbSet<Transport> Transport { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PLNPRJCT;Trusted_Connection=True;");
        }
    }
}