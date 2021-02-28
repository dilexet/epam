using System.Data.Entity.Migrations;

namespace SalesStatistics.DataAccessLayer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EntityFrameworkContext.SalesInformationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}