using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantNew.Models;

namespace RestaurantNew.Controllers
{
    public class MenuSalesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MenuSales
        public ActionResult Index()
        {
            var menuSales = db.MenuSales.Include(m => m.Sale);
            return View(menuSales.ToList());
        }

        // GET: MenuSales/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuSale menuSale = db.MenuSales.Find(id);
            if (menuSale == null)
            {
                return HttpNotFound();
            }
            return View(menuSale);
        }

        // GET: MenuSales/Create
        public ActionResult Create()
        {
            ViewBag.SaleId = new SelectList(db.Sales, "Id", "Name");
            return View();
        }

        // POST: MenuSales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenuId,SaleId")] MenuSale menuSale)
        {
            if (ModelState.IsValid)
            {
                db.MenuSales.Add(menuSale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SaleId = new SelectList(db.Sales, "Id", "Name", menuSale.SaleId);
            return View(menuSale);
        }

        // GET: MenuSales/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuSale menuSale = db.MenuSales.Find(id);
            if (menuSale == null)
            {
                return HttpNotFound();
            }
            ViewBag.SaleId = new SelectList(db.Sales, "Id", "Name", menuSale.SaleId);
            return View(menuSale);
        }

        // POST: MenuSales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenuId,SaleId")] MenuSale menuSale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuSale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SaleId = new SelectList(db.Sales, "Id", "Name", menuSale.SaleId);
            return View(menuSale);
        }

        // GET: MenuSales/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuSale menuSale = db.MenuSales.Find(id);
            if (menuSale == null)
            {
                return HttpNotFound();
            }
            return View(menuSale);
        }

        // POST: MenuSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MenuSale menuSale = db.MenuSales.Find(id);
            db.MenuSales.Remove(menuSale);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
