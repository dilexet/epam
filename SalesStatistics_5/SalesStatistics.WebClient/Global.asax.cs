using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SalesStatistics.WebClient.Infrastructure;
using Serilog;

namespace SalesStatistics.WebClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("~/Logs/log.txt", fileSizeLimitBytes: 10000000)
                .Enrich.WithMvcRouteTemplate()
                .Enrich.WithMvcActionName()
                .CreateLogger();
            Database.SetInitializer(new AppDbInitializer());
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
