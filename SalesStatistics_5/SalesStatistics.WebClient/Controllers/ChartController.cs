using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SalesStatistics.DataAccessLayer;
using SalesStatistics.DataAccessLayer.Models;
using SalesStatistics.WebClient.Services;
using Serilog;

namespace SalesStatistics.WebClient.Controllers
{
    public class ChartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperConfig _mapperConfig;
        public ChartController(IUnitOfWork unitOfWork, IMapperConfig mapperConfig)
        {
            _unitOfWork = unitOfWork;
            _mapperConfig = mapperConfig;
        }

        // GET
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                IDictionary<string, int> data = new Dictionary<string, int>();

                IEnumerable<Sale> sales = _unitOfWork.Repository.Get<Sale>().ToList();

                var salesView = _mapperConfig.MapConfig(sales);

                var dates = salesView.Select(x => x.Date).Distinct().ToList();

                foreach (var item in dates)
                {
                    if (item != null)
                    {
                        data.Add(item.Value.ToShortDateString(), sales.Count(x => x.Date == item));
                    }
                }

                return View(data);
            }
            catch (Exception e)
            {
                Log.Error("{Message}", e.ToString());
                return View("Error");
            }
        }
        
        #region Disposable
    
        private bool _disposed;

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
            }
            _disposed = true;
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}