using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SalesStatistics.DataAccessLayer.EFUnitOfWork;
using SalesStatistics.DataAccessLayer.EntityFrameworkContext;
using SalesStatistics.DataAccessLayer.Models;
using SalesStatistics.WebClient.Models.Filter;
using SalesStatistics.WebClient.Models.ViewModels;
using SalesStatistics.WebClient.Services;
using Serilog;

namespace SalesStatistics.WebClient.Controllers
{
    public class SaleController : Controller
    {
        [Authorize]
        public ActionResult Index(SalesFilterModel salesFilterModel)
        {
            IEnumerable<SaleViewModel> salesView;
            using (UnitOfWork unitOfWork = new UnitOfWork(new SampleContextFactory()))
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
                    }).ToList();

                salesView = DataFilter.Filter(salesFilterModel, items);
            }

            if (!Request.IsAjaxRequest())
            {
                return View(salesView.ToList());
            }

            return PartialView("IndexPage", salesView.ToList());
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sale sale;
            using (UnitOfWork unitOfWork = new UnitOfWork(new SampleContextFactory()))
            {
                var item = unitOfWork.Repository.SingleOrDefault<Sale>(x => x.Id == id);


                if (item == null)
                {
                    return HttpNotFound();
                }

                sale = new Sale
                {
                    Id = item.Id,
                    Client = item.Client,
                    Manager = item.Manager,
                    Product = item.Product,
                    Date = item.Date
                };
            }

            MapperConfig mapperConfig = new MapperConfig();
            var saleView = mapperConfig.MapConfig(sale);
            return View(saleView);
        }

        // GET: Sale/Edit
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sale sale;
            using (UnitOfWork unitOfWork = new UnitOfWork(new SampleContextFactory()))
            {
                var item = unitOfWork.Repository.Find<Sale>(id);

                if (item == null)
                {
                    return HttpNotFound();
                }

                sale = new Sale
                {
                    Id = item.Id,
                    Client = item.Client,
                    Manager = item.Manager,
                    Product = item.Product,
                    Date = item.Date
                };
            }

            MapperConfig mapperConfig = new MapperConfig();
            var saleView = mapperConfig.MapConfig(sale);
            return View(saleView);
        }

        // POST: Sale/Edit
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult EditPost(SaleViewModel sale)
        {
            if (sale == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SaleViewModel saleView = null;
            using (UnitOfWork unitOfWork = new UnitOfWork(new SampleContextFactory()))
            {
                var saleToUpdate = unitOfWork.Repository.Find<Sale>(sale.Id);
                try
                {
                    saleToUpdate.Client.Surname = sale.ClientSurname;
                    saleToUpdate.Client.FirstName = sale.ClientFirstName;
                    saleToUpdate.Product.Name = sale.ProductName;
                    saleToUpdate.Product.Cost = sale.ProductCost;
                    saleToUpdate.Date = sale.Date;

                    unitOfWork.Repository.Context.Entry(saleToUpdate).State = EntityState.Modified;
                    unitOfWork.SaveChanges();
                    MapperConfig mapperConfig = new MapperConfig();
                    saleView = mapperConfig.MapConfig(saleToUpdate);
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException e)
                {
                    Log.Error("{Message}", e.ToString());
                    ModelState.AddModelError("",
                        @"Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(saleView);
        }

        // GET: Sale/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sale/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(SaleViewModel saleView)
        {
            try
            {
                MapperConfig mapperConfig = new MapperConfig();
                var sale = mapperConfig.MapConfig(saleView);
                if (ModelState.IsValid)
                {
                    using (UnitOfWork unitOfWork = new UnitOfWork(new SampleContextFactory()))
                    {
                        unitOfWork.Repository.Add(sale);
                        unitOfWork.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (DataException e)
            {
                Log.Error("{Message}", e.ToString());
                ModelState.AddModelError("",
                    @"Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(saleView);
        }


        // GET: Sale/Delete
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {
                Log.Error("{Message}",
                    "Delete failed. Try again, and if the problem persists see your system administrator");
            }

            Sale sale;
            using (UnitOfWork unitOfWork = new UnitOfWork(new SampleContextFactory()))
            {
                var item = unitOfWork.Repository.Find<Sale>(id);
                if (item == null)
                {
                    return HttpNotFound();
                }

                sale = new Sale
                {
                    Id = item.Id,
                    Client = item.Client,
                    Manager = item.Manager,
                    Product = item.Product,
                    Date = item.Date
                };
            }

            MapperConfig mapperConfig = new MapperConfig();
            var saleView = mapperConfig.MapConfig(sale);
            return View(saleView);
        }

        // POST: Sale/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new SampleContextFactory()))
                {
                    Sale student = unitOfWork.Repository.Find<Sale>(id);
                    unitOfWork.Repository.Remove(student);
                    unitOfWork.SaveChanges();
                }
            }
            catch (RetryLimitExceededException e)
            {
                Log.Error("{Message}", e.ToString());
                return RedirectToAction("Delete", new {id, saveChangesError = true});
            }

            return RedirectToAction("Index");
        }
    }
}