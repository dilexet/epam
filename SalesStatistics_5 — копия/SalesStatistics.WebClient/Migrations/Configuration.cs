using System.Data.Entity.Migrations;
using SalesStatistics.WebClient.Identity;

namespace SalesStatistics.WebClient.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);
            context.Roles.AddOrUpdate(new AppRole("Admin"));
            context.Roles.AddOrUpdate(new AppRole("User"));
            context.SaveChanges();
        }
    } 
}