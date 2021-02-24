using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SalesStatistics.WebClient.Models;

namespace SalesStatistics.WebClient.Infrastructure
{
    public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(context));
 
            // создаем две роли
            var role1 = new AppRole { Name = "Admin" };
            var role2 = new AppRole { Name = "User" };
 
            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);

            base.Seed(context);
        }
    }
}