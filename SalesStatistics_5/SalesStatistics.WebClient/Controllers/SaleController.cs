using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using SalesStatistics.DataAccessLayer.EFUnitOfWork;
using SalesStatistics.DataAccessLayer.EntityFrameworkContext;
using SalesStatistics.ModelLayer.Models;
using Serilog;

namespace SalesStatistics.WebClient.Controllers
{
    public class SaleController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new SampleContextFactory());

        [Authorize]
        public ActionResult Index(string sortOrder, string searchStringClient,string searchStringProduct, int? page)
        {
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "clientSurname" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var items = _unitOfWork.Repository.Get<Sale>()
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
            if (!string.IsNullOrEmpty(searchStringClient))
            {
                items = items.Where(s => s.Client.Surname.ToUpper().Contains(searchStringClient.ToUpper()) ||
                                         s.Client.FirstName.ToUpper().Contains(searchStringClient.ToUpper()));
            }

            if (!string.IsNullOrEmpty(searchStringProduct))
            {
                items = items.Where(s => s.Product.Name.ToUpper().Contains(searchStringProduct.ToUpper()));
            }
            IEnumerable<Sale> sales = items.ToList();
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

            int pageSize = 3;
            int pageNumber = page ?? 1;
            return View(sales.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var items = _unitOfWork.Repository.SingleOrDefault<Sale>(x => x.Id == id);

            if (items == null)
            {
                return HttpNotFound();
            }

            var sale = new Sale()
            {
                Id = items.Id,
                Client = items.Client,
                Manager = items.Manager,
                Product = items.Product,
                Date = items.Date
            };


            return View(sale);
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

            var item = _unitOfWork.Repository.Find<Sale>(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
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

            var saleToUpdate = _unitOfWork.Repository.Find<Sale>(sale.Id);
            if (TryUpdateModel(saleToUpdate))
            {
                try
                {
                    saleToUpdate.Client = sale.Client;
                    saleToUpdate.Product = sale.Product;
                    saleToUpdate.Date = sale.Date;

                    _unitOfWork.Repository.Context.Entry(saleToUpdate).State = EntityState.Modified;
                    _unitOfWork.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException e)
                {
                    Log.Error("{Message}", e.ToString());
                    ModelState.AddModelError("",
                        @"Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(saleToUpdate);
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
                    _unitOfWork.Repository.Add(sale);
                    _unitOfWork.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException e)
            {
                Log.Error("{Message}", e.ToString());
                ModelState.AddModelError("",
                    @"Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(sale);
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

            Sale sale = _unitOfWork.Repository.Find<Sale>(id);
            if (sale == null)
            {
                return HttpNotFound();
            }

            return View(sale);
        }

        // POST: Sale/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                Sale student = _unitOfWork.Repository.Find<Sale>(id);
                _unitOfWork.Repository.Remove(student);
                _unitOfWork.SaveChanges();
            }
            catch (RetryLimitExceededException e)
            {
                Log.Error("{Message}", e.ToString());
                return RedirectToAction("Delete", new {id, saveChangesError = true});
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}