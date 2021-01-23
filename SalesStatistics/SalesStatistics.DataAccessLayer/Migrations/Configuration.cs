using System.Data.Entity.Migrations;
using SalesStatistics.DataAccessLayer.EntityFraimworkContext;

namespace SalesStatistics.DataAccessLayer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SalesInformationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}