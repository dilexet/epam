using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SalesStatistics.DataAccessLayer.EFUnitOfWork;
using SalesStatistics.DataAccessLayer.EntityFrameworkContext;
using SalesStatistics.DataAccessLayer.Models;
using SalesStatistics.WebClient.Services;

namespace SalesStatistics.WebClient.Controllers
{
    public class ChartController : Controller
    {
        // GET
        public ActionResult Index()
        {
            List<string> dates = new List<string>();
            using (UnitOfWork unitOfWork = new UnitOfWork(new SampleContextFactory()))
            {
                IEnumerable<Sale> sales = unitOfWork.Repository.Get<Sale>().ToList();
                MapperConfig mapperConfig = new MapperConfig();
                var salesView = mapperConfig.MapConfig(sales);
                var counts = new List<int>();
                var items = salesView.Select(x => x.Date).Distinct().ToList();
                
                foreach (var item in items)
                {
                    counts.Add(sales.Count(x => x.Date == item));
                    if (item != null)
                    {
                        dates.Add(item.Value.ToShortDateString());
                    }
                }
                ViewBag.Counts = counts.ToList();
            }
            return View(dates);  
        }  
    }
}