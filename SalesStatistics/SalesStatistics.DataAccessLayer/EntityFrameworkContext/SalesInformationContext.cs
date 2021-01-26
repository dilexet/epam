using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.DataAccessLayer.EntityFrameworkContext
{
    public class SalesInformationContext: DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        
        public SalesInformationContext(): 
            base("ManagerContext")
        {
            
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // modelBuilder.Entity<Manager>().HasKey(x => x.Id);
            // base.OnModelCreating(modelBuilder);
        }
    }
}