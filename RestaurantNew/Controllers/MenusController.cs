using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantNew.Models;


namespace RestaurantNew.Controllers
{
    public class MenuWithSale
    {
        public int IdMenu { get; set; }
        public string NameDose { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageUri { get; set; }
        public int Categorya { get; set; }
      
   }
public class MenusController : Controller
    {
        public string Status;
        public static IEnumerable<MenuWithSale> resusultSale;
        public int DiscountAfter =5; 
        public string StatusSale = "None";
        public int IdSaleTime = 0; // 1= morning ' 2 = afternoon .  3=evening
        private ApplicationDbContext db = new ApplicationDbContext();
     
        private void InitialResultSale()
        {
            string UserStatus = (Session["User"] as ApplicationUser).UserStatus;

            DiscountAfter = db.Sales.FirstOrDefault(s => s.Name == UserStatus).Discount;

            resusultSale = from menu1 in db.Menus
                           select new MenuWithSale()
                           {
                               IdMenu = menu1.IdMenu,
                               NameDose = menu1.NameDose,
                               Description = menu1.Description,
                               Price = menu1.Price * (100 - DiscountAfter)/100,  //  sale1.Discount * menu1.Price  ,
                               ImageUri = menu1.ImageUri,
                               Categorya = menu1.Categorya
                           };
        }

        // GET: Menus
        public ActionResult Index()
        {
            return View(db.Menus.ToList());
        }
        
        public ActionResult List()
        {
            InitialResultSale();
            return View();
        }
       
        public ActionResult Disserts()   // מחזירה את הקינוחים לאחר שעידכנה אחוז הנחה מתאים ליוזר 
        {            
            return View("Index", resusultSale.Where(m => m.Categorya == 1).ToList());
        }
        public ActionResult Drinks()
        {
            return View("Index", resusultSale.Where(m => m.Categorya == 2).ToList());
        }
        public ActionResult Maindishes()
        {
            return View("Index", resusultSale.Where(m => m.Categorya == 3).ToList());
        }

        public ActionResult Starters()
        {  
            return View("Index", resusultSale.Where(m => m.Categorya == 4));
        }
        [HttpPost]
        public ActionResult Search( string NameDuse = "uu" ,int FromPrice = 0 ,int UntilPrice = 0)
        {
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
