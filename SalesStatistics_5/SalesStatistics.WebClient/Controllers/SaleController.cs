﻿using System.Collections.Generic;
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

        public ActionResult Index(int? page)
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
                });
            IEnumerable<Sale> sales = items.ToList();

            int pageSize = 3;
            int pageNumber = page ?? 1;
            return View(sales.ToPagedList(pageNumber, pageSize));
        }
        
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
                    ModelState.AddModelError("", @"Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(saleToUpdate);
        }

        // GET: Sale/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: Sale/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                ModelState.AddModelError("", @"Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(sale);
        }
        
        
        // GET: Sale/Delete
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
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
                Log.Error("{Message}",e.ToString());
                return RedirectToAction("Delete", new {id, saveChangesError = true });
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