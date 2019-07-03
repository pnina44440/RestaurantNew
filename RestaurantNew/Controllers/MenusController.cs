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
    public class MenusController : Controller
    {

        public int DiscountAfter =5; 
        public string StatusSale = "None";
        public int IdSaleTime = 0; // 1= morning ' 2 = afternoon .  3=evening
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Menus
        public ActionResult Index()
        {
            return View(db.Menus.ToList());
        }
        
        public ActionResult List()
        {
            return View();
        }
        public int CheckDiscountAfter()
        {
            var disc = from sale1 in db.Sales
                       select sale1;
           // DiscountAfter = disc.Where(d => d.Discount == Session["User"]);
           
            return DiscountAfter;
        }
        public ActionResult Disserts()   // מחזירה את הקינוחים לאחר שעידכנה אחוז הנחה מתאים ליוזר 
        {
            
            
            var dissert = from sale1 in db.Sales
                           join  menusale in db.MenuSales on sale1.Id equals menusale.SaleId
                           join menu1 in db.Menus on menusale.SaleId equals menu1.IdMenu
                          select new
                          {
                              menu1.NameDose ,
                              menu1.Description ,
                              menu1.Price,  //  sale1.Discount * menu1.Price  ,
                              menu1.ImageUri ,
                              menu1.Categorya
                          };

            dissert = dissert.Where(d => d.Categorya == 1);

            return View("Index", dissert);
        }
        public ActionResult Drinks()
        {
            var drink = from m in db.Menus
                        select m;

            drink = drink.Where(d => d.Categorya == 2);

            return View("Index", drink);
        }
        public ActionResult Maindishes()
        {
            var maindish = from m in db.Menus
                           select m;

            maindish = maindish.Where(m => m.Categorya == 3);

            return View("Index", maindish);
        }

        public ActionResult Starters()
        {
            var start = from m in db.Menus
                        select m;

            start = start.Where(s => s.Categorya == 4);

            return View("Index", start);
        }
        [HttpPost]
        public ActionResult Search( string NameDuse = "uu" ,int FromPrice = 0 ,int UntilPrice = 0)
        {
            //List<string> Cars ;
            //var menu = from m in db.Menus
            //           where m.Price < UntilPrice && m.Price > FromPrice
            //           group m.Price by m.IdMenu into g

            //           select new { IdMenu = g.Key, cars = g.ToList() };
            //---------------------
            var menu = from m in db.Menus
                       where (m.Price < UntilPrice) && (m.Price > FromPrice)
                       select m;


            if (!String.IsNullOrEmpty(NameDuse))
            {
                menu = menu.Where(s => s.NameDose.Contains(NameDuse));
            }

       

            return View("Index",menu);
        }


        // GET: Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Menus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMenu,Categorya,NameDose,Description,Price,ImageUri")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // GET: Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMenu,Categorya,NameDose,Description,Price,ImageUri")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // GET: Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
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
        public int CheckSuitibleSale()
        {

            System.DateTime moment = new System.DateTime();
            // Year gets 1999.
           
            int hour = moment.Hour;
            if (hour > 8 && hour < 12)
                IdSaleTime = 1;
            else if(hour > 16 && hour < 20)
                IdSaleTime = 2;
            else
                IdSaleTime = 3;
            return IdSaleTime;
        }
     
    }
}
