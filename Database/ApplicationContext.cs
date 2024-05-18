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
        public DbSet<Company> Company { get; set; }
        public DbSet<ContainerPackage> ContainerPackage { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PLNPRJCT;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Storage>()
                .HasOne(s => s.Company)
                .WithMany(c => c.Storage)
                .HasForeignKey(s => s.IdCompany);

            modelBuilder.Entity<Consignee>()
                .HasOne(c => c.Company)
                .WithMany(co => co.Consignee)
                .HasForeignKey(c => c.IdCompany);

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Company)
                .WithMany()
                .HasForeignKey(o => o.IdCompany);

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Storage)
                .WithMany()
                .HasForeignKey(o => o.IdStorage);

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Payer)
                .WithMany()
                .HasForeignKey(o => o.IdPayer);

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Consignee)
                .WithMany()
                .HasForeignKey(o => o.IdConsignee);
        }
    }
}