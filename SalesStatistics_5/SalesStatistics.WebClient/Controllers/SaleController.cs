using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SalesStatistics.DataAccessLayer;
using SalesStatistics.DataAccessLayer.Models;
using SalesStatistics.WebClient.Models.Filter;
using SalesStatistics.WebClient.Models.ViewModels;
using SalesStatistics.WebClient.Services;
using Serilog;

namespace SalesStatistics.WebClient.Controllers
{
    public class SaleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperConfig _mapperConfig;

        public SaleController(IUnitOfWork unitOfWork, IMapperConfig mapperConfig)
        {
            _unitOfWork = unitOfWork;
            _mapperConfig = mapperConfig;
        }

        [Authorize]
        public ActionResult Index()
        {
            try
            {
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
                    }).ToList();
                var salesView = _mapperConfig.MapConfig(DataFilter.Filter(new SalesFilterModel(), items));
                return View(salesView.ToList());
            }
            catch (Exception e)
            {
               Log.Error("{Message}", e.ToString());

               return View("Error");
            }
        }

        public ActionResult UpdateTable(SalesFilterModel salesFilterModel)
        {
            try
            {
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
                    }).ToList();

                var salesView = _mapperConfig.MapConfig(DataFilter.Filter(salesFilterModel, items));
                return PartialView("_Table", salesView.ToList());
            }
            catch (Exception e)
            {
                Log.Error("{Message}", e.ToString());
                return View("Error");
            }
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var item = _unitOfWork.Repository.SingleOrDefault<Sale>(x => x.Id == id);


                if (item == null)
                {
                    return HttpNotFound();
                }


                var saleView = _mapperConfig.MapConfig(new Sale
                {
                    Id = item.Id,
                    Client = item.Client,
                    Manager = item.Manager,
                    Product = item.Product,
                    Date = item.Date
                });
                return View(saleView);
            }
            catch (Exception e)
            {
                Log.Error("{Message}", e.ToString());
                return View("Error");
            }
        }

        // GET: Sale/Edit
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            try
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

                var saleView = _mapperConfig.MapConfig(new Sale
                {
                    Id = item.Id,
                    Client = item.Client,
                    Manager = item.Manager,
                    Product = item.Product,
                    Date = item.Date
                });
                return View(saleView);
            }
            catch (Exception e)
            {
                Log.Error("{Message}", e.ToString());
                return View("Error");
            }
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

            var saleToUpdate = _unitOfWork.Repository.Find<Sale>(sale.Id);
            try
            {
                saleToUpdate.Client.Surname = sale.ClientSurname;
                saleToUpdate.Client.FirstName = sale.ClientFirstName;
                saleToUpdate.Product.Name = sale.ProductName;
                saleToUpdate.Product.Cost = sale.ProductCost;
                saleToUpdate.Date = sale.Date;

                _unitOfWork.Repository.Context.Entry(saleToUpdate).State = EntityState.Modified;
                _unitOfWork.SaveChanges();
                saleView = _mapperConfig.MapConfig(saleToUpdate);
                return RedirectToAction("Index");
            }
            catch (RetryLimitExceededException e)
            {
                Log.Error("{Message}", e.ToString());
                ModelState.AddModelError("",
                    @"Unable to save changes. Try again, and if the problem persists, see your system administrator.");
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
                var sale = _mapperConfig.MapConfig(saleView);
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

            return View(saleView);
        }


        // GET: Sale/Delete
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            try
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

                var item = _unitOfWork.Repository.Find<Sale>(id);
                if (item == null)
                {
                    return HttpNotFound();
                }

                var saleView = _mapperConfig.MapConfig(new Sale
                {
                    Id = item.Id,
                    Client = item.Client,
                    Manager = item.Manager,
                    Product = item.Product,
                    Date = item.Date
                });
                return View(saleView);
            }
            catch (Exception e)
            {
                Log.Error("{Message}", e.ToString());
                return View("Error");
            }
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