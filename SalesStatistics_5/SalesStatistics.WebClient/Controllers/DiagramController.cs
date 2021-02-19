using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SalesStatistics.DataAccessLayer.EFUnitOfWork;
using SalesStatistics.DataAccessLayer.EntityFrameworkContext;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.WebClient.Controllers
{
    public class DiagramController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new SampleContextFactory());
        
        // GET
        public ActionResult Index()
        {
            IDictionary<string, uint> statistics = new Dictionary<string, uint>();
            var sales = _unitOfWork.Repository.Get<Sale>().ToList();
            foreach (var item in sales)
            {
                if (item.Date != null)
                {
                    var count = Convert.ToUInt32(sales.Count(x => x.Date == item.Date));
                    try
                    {
                        statistics.Add(item.Date.Value.ToShortDateString(), count);
                    }
                    catch (ArgumentException e)
                    {
                        Serilog.Log.Error("{Message}", e.ToString());
                    }
                }
            }
            return View(statistics);
        }
        
        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}