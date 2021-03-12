using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using SalesStatistics.WebClient.Identity;
using Serilog;

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
            var roleAdmin = context.Roles.FirstOrDefault(role => role.Name == "Admin");
            var roleUser = context.Roles.FirstOrDefault(role => role.Name == "User");

            try
            {
                if (roleAdmin == null)
                {
                    context.Roles.AddOrUpdate(new AppRole("Admin"));
                }

                if (roleUser == null)
                {
                    context.Roles.AddOrUpdate(new AppRole("User"));
                }
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    Log.Error("Object: {Message} ",validationError.Entry.Entity.ToString());
                    
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        Log.Error("{Message} ", err.ErrorMessage);
                    }
                }
            }

        }
    }
}