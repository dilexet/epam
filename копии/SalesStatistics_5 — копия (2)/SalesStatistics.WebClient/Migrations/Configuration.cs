
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SalesStatistics.WebClient.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SalesStatistics.WebClient.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}