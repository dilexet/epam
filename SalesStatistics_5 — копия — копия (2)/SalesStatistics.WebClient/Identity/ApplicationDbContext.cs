using Microsoft.AspNet.Identity.EntityFramework;

namespace SalesStatistics.WebClient.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("Default_10", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            var context = new ApplicationDbContext();
            
            return context;
        }
    }
}