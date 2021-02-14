
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SalesStatistics.DataAccessLayer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SalesStatistics.DataAccessLayer.EntityFrameworkContext.SalesInformationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}