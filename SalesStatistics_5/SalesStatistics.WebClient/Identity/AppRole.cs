using Microsoft.AspNet.Identity.EntityFramework;

namespace SalesStatistics.WebClient.Identity
{
    public class AppRole : IdentityRole
    {
        public AppRole()
        { }

        public AppRole(string name)
            : base(name)
        { }
    }
}