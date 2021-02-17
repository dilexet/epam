using System.Data.Entity.Migrations;

namespace SalesStatistics.WebClient.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}