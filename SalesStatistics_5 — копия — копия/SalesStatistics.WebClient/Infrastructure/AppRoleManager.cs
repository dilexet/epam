using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SalesStatistics.WebClient.Identity;

namespace SalesStatistics.WebClient.Infrastructure
{
    public class AppRoleManager : RoleManager<AppRole>
    {
        public AppRoleManager(RoleStore<AppRole> store)
            : base(store)
        {
        }

        public static AppRoleManager Create(
            IdentityFactoryOptions<AppRoleManager> options,
            IOwinContext context)
        {
            var store = new
                RoleStore<AppRole>(context.Get<ApplicationDbContext>());
            
            return new AppRoleManager(store);
        }
    }
}