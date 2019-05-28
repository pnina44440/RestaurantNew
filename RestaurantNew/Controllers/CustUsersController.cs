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
    public class CustUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CustUsers
        public ActionResult Index()
        {
            return View(db.CustUsers.ToList());
        }

        // GET: CustUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustUser custUser = db.CustUsers.Find(id);
            if (custUser == null)
            {
                return HttpNotFound();
            }
            return View(custUser);
        }

        // GET: CustUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustName,Password")] CustUser custUser)
        {
            if (ModelState.IsValid)
            {
                db.CustUsers.Add(custUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(custUser);
        }

        // GET: CustUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustUser custUser = db.CustUsers.Find(id);
            if (custUser == null)
            {
                return HttpNotFound();
            }
            return View(custUser);
        }

        // POST: CustUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustName,Password")] CustUser custUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(custUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(custUser);
        }

        // GET: CustUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustUser custUser = db.CustUsers.Find(id);
            if (custUser == null)
            {
                return HttpNotFound();
            }
            return View(custUser);
        }

        // POST: CustUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustUser custUser = db.CustUsers.Find(id);
            db.CustUsers.Remove(custUser);
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
