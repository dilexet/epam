using System.Data.Entity.Migrations;

namespace SalesStatistics.DataAccessLayer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Contexts.SalesInformationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}