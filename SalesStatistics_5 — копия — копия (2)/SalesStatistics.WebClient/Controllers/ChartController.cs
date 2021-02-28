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
        [Authorize]
        public ActionResult Index()
        {
            IDictionary<string, int> data = new Dictionary<string, int>();
            using (UnitOfWork unitOfWork = new UnitOfWork(new SampleContextFactory()))
            {
                IEnumerable<Sale> sales = unitOfWork.Repository.Get<Sale>().ToList();
                MapperConfig mapperConfig = new MapperConfig();
                
                var salesView = mapperConfig.MapConfig(sales);
                
                var dates = salesView.Select(x => x.Date).Distinct().ToList();

                foreach (var item in dates)
                {
                    if (item != null)
                    {
                        data.Add(item.Value.ToShortDateString(), sales.Count(x => x.Date == item));
                    }
                }
            }

            return View(data);
        }
    }
}