using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SalesStatistics.WebClient.Startup))]
namespace SalesStatistics.WebClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
