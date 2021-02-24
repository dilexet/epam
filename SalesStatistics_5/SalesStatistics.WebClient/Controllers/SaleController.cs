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
using SalesStatistics.WebClient.Filter;
using SalesStatistics.WebClient.Services;
using Serilog;

namespace SalesStatistics.WebClient.Controllers
{
    public class SaleController : Controller
    {
       [Authorize]
        public ActionResult Index(string sortOrder, SalesFilterModel salesFilterModel)
        {
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "clientSurname" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            IEnumerable<Sale> sales;
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
                    });

                if (!string.IsNullOrEmpty(salesFilterModel.ClientName))
                {
                    items = items.Where(s =>
                        s.Client.Surname.ToUpper().Contains(salesFilterModel.ClientName.ToUpper()) ||
                        s.Client.FirstName.ToUpper().Contains(salesFilterModel.ClientName.ToUpper()));
                }

                if (!string.IsNullOrEmpty(salesFilterModel.ProductName))
                {
                    items = items.Where(s => s.Product.Name.ToUpper().Contains(salesFilterModel.ProductName.ToUpper()));
                }

                if (salesFilterModel.DateStart != null && salesFilterModel.DateEnd != null)
                {
                    items = items.Where(s =>
                        s.Date >= salesFilterModel.DateStart && s.Date <= salesFilterModel.DateEnd);
                }

                sales = items.ToList();


                switch (sortOrder)
                {
                    case "clientSurname":
                        sales = sales.OrderBy(s => s.Client.Surname);
                        break;
                    case "Date":
                        sales = sales.OrderBy(s => s.Date);
                        break;
                    case "date_desc":
                        sales = sales.OrderByDescending(s => s.Date);
                        break;
                    default:
                        sales = sales.OrderBy(s => s.Manager.Surname);
                        break;
                }
            }

            MapperConfig mapperConfig = new MapperConfig();
            var salesView = mapperConfig.MapConfig(sales);
            if (!Request.IsAjaxRequest())
            {
                return View(salesView.ToList());
            }
            return PartialView("IndexPage",salesView.ToList());
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
        public ActionResult EditPost(Sale sale)
        {
            if (sale == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sale saleToUpdate;
            using (UnitOfWork unitOfWork = new UnitOfWork(new SampleContextFactory()))
            {
                saleToUpdate = unitOfWork.Repository.Find<Sale>(sale.Id);
                if (TryUpdateModel(saleToUpdate))
                {
                    try
                    {
                        saleToUpdate.Client = sale.Client;
                        saleToUpdate.Product = sale.Product;
                        saleToUpdate.Date = sale.Date;

                        unitOfWork.Repository.Context.Entry(saleToUpdate).State = EntityState.Modified;
                        unitOfWork.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    catch (RetryLimitExceededException e)
                    {
                        Log.Error("{Message}", e.ToString());
                        ModelState.AddModelError("",
                            @"Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }
            }
            MapperConfig mapperConfig = new MapperConfig();
            var saleView = mapperConfig.MapConfig(saleToUpdate);
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
        public ActionResult Create(Sale sale)
        {
            try
            {
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

            MapperConfig mapperConfig = new MapperConfig();
            var saleView = mapperConfig.MapConfig(sale);
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
                ViewBag.ErrorMessage =
                    "Delete failed. Try again, and if the problem persists see your system administrator.";
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