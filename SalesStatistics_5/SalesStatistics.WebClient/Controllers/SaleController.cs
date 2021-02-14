using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using SalesStatistics.DataAccessLayer;
using SalesStatistics.DataAccessLayer.EFUnitOfWork;
using SalesStatistics.DataAccessLayer.EntityFrameworkContext;
using SalesStatistics.ModelLayer.Models;
using Serilog;

namespace SalesStatistics.WebClient.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        public ActionResult Index(int? page)
        {
            IEnumerable<Sale> sales;
            using (IUnitOfWork unitOfWork = new UnitOfWork(new SampleContextFactory()))
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

        // GET: Sale/Details
        public ActionResult Details(int? id)
        {
            Sale sale;
            using (IUnitOfWork unitOfWork = new UnitOfWork(new SampleContextFactory()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var items = unitOfWork.Repository.SingleOrDefault<Sale>(x => x.Id == id);
                
                if (items == null)
                {
                    return HttpNotFound();
                }

                sale = new Sale()
                {
                    Id = items.Id,
                    Client = items.Client,
                    Manager = items.Manager,
                    Product = items.Product,
                    Date = items.Date
                };
            }

            return View(sale);
        }


        // GET: Sale/Edit
        public ActionResult Edit(int? id)
        {
            Sale sale;
            using (IUnitOfWork unitOfWork = new UnitOfWork(new SampleContextFactory()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var item = unitOfWork.Repository.SingleOrDefault<Sale>(x => x.Id == id);
                
                if (item == null)
                {
                    return HttpNotFound();
                }

                sale = new Sale
                {
                    Client = item.Client,
                    Manager = item.Manager,
                    Product = item.Product,
                    Date = item.Date,
                };
            }

            return View(sale);
        }

        // POST: Sale/Edit
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            Sale saleToUpdate;
            using (IUnitOfWork unitOfWork = new UnitOfWork(new SampleContextFactory()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var item = unitOfWork.Repository.SingleOrDefault<Sale>(x => x.Id == id);
                saleToUpdate = new Sale
                {
                    Client = item.Client,
                    Manager = item.Manager,
                    Product = item.Product,
                    Date = item.Date,
                };
                if (TryUpdateModel(saleToUpdate, "",
                    new[] {"Surname", "FirstName", "Name", "Cost", "Date"}))
                {
                    try
                    {
                        unitOfWork.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    catch (DataException e)
                    {
                        Log.Error("{Message}", e.ToString());
                        ModelState.AddModelError("",
                            @"Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }
            }

            return View(saleToUpdate);
        }

        public ActionResult Delete()
        {
            return null;
        }
    }
}