using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ASP.NET_MVC.DataAccessLayer.EFUnitOfWork;
using PagedList;
using SalesStatistics.DataAccessLayer.EntityFrameworkContext;
using SalesStatistics.ModelLayer.Models;

namespace SalesStatistics.WebClient.Controllers
{
    public class SaleController : Controller
    {
        // GET
        public ActionResult Index(int? page)
        {
            IEnumerable<Sale> sales;
            using (var unitOfWork = new UnitOfWork(new SampleContextFactory()))
            {
                var items = unitOfWork.Repository.Get<Sale>()
                    .ToList()
                    .Select(x => new Sale()
                    {
                        Id = x.Id,
                        ManagerId = x.Id,
                        ClientId = x.ClientId,
                        ProductId = x.ProductId,
                        Date = x.Date,
                        Client = x.Client,
                        Manager = x.Manager,
                        Product = x.Product
                    });
                sales = items.ToList();
            }
            int pageSize = 10;
            int pageNumber = page ?? 1;
            return View(sales.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Edit()
        {
            return null;
        }
        public ActionResult Details()
        {
            return null;
        }
        public ActionResult Delete()
        {
            return null;
        }
    }
}