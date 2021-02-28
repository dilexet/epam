using System.Web.Mvc;

namespace SalesStatistics.WebClient.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}