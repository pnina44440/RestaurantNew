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
        //    public Dog { get; set; }
        //public string BreedName { get; set; }
      
     public string NameDose { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageUri { get; set; }
        public int Categorya { get; set; }
      
   }
public class MenusController : Controller
    {
        public string StatusRadios;
        public string WhoCheck;
        public IQueryable<MenuWithSale>  resusultSale;
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
        //public IQueryable<DogWithBreed> GetDogsWithBreedNames()
        //{
        //    var db = new DogDataContext(ConnectString);
        //    var result = from d in db.Dogs
        //                 join b in db.Breeds on d.BreedId equals b.BreedId
        //                 select new DogWithBreed()
        //                 {
        //                     Dog = d,
        //                     BreedName = b.BreedName
        //                 };
        //    return result;
        //}

        [HttpPost]
        public ActionResult CheckDiscountAfter1(string WhoCheck, string StatusRadios)
        {
            Debug.WriteLine(WhoCheck);
            this.WhoCheck = WhoCheck;
            this.StatusRadios = StatusRadios;
            Debug.WriteLine(WhoCheck);
            return View();
        }

        [HttpPost]
        public  IQueryable<MenuWithSale> CheckDiscountAfter(string WhoCheck , string StatusRadios)
        {
            Debug.WriteLine(WhoCheck);
            this.WhoCheck = WhoCheck;
            this.StatusRadios = StatusRadios;
            Debug.WriteLine(WhoCheck);
            //מוצא את השורה מהטבלה  ההנחה הטבלה לפי מה שהיוזר במחר ברדיו ברגיסטר
            var disc = from sale1 in db.Sales
                       where WhoCheck == sale1.Name
                       select sale1;

            //מחזיר את ההנחה מתוצאת השאילתא
            foreach (var x in disc)
            {
                DiscountAfter = x.Discount;
            }



            //---------מחזיר לclass שיצרנו -------------
             resusultSale = from sale1 in db.Sales
                          join menusale in db.MenuSales on sale1.Id equals menusale.SaleId
                          join menu1 in db.Menus on menusale.SaleId equals menu1.IdMenu
                          select new MenuWithSale()
                          {
                              NameDose = menu1.NameDose ,
                              Description = menu1.Description,
                              Price = menu1.Price * DiscountAfter,  //  sale1.Discount * menu1.Price  ,
                              ImageUri = menu1.ImageUri,
                              Categorya = menu1.Categorya
                          };

            //---------------------------
            Drinks();
            Disserts();
            // DiscountAfter = disc.Where(d => d.Discount == Session["User"]);
            
            return resusultSale;
        }
        public ActionResult Disserts()   // מחזירה את הקינוחים לאחר שעידכנה אחוז הנחה מתאים ליוזר 
        {

            //var dissert = from sale1 in db.Sales
            //                   join menusale in db.MenuSales on sale1.Id equals menusale.SaleId
            //                   join menu1 in db.Menus on menusale.SaleId equals menu1.IdMenu
            //                   select new
            //                   {
            //                       a = menu1.NameDose,
            //                       b = menu1.Description,
            //                       c = menu1.Price * DiscountAfter,  //  sale1.Discount * menu1.Price  ,
            //                       d = menu1.ImageUri,
            //                       e = menu1.Categorya
            //                   };


            resusultSale = resusultSale.Where(d => d.Categorya == 1);

            return View("Index", resusultSale);
        }

     

     

        public ActionResult Drinks()
        {
            //var drink = from m in db.Menus
            //            select m;

            //var drink = from sale1 in db.Sales
            //              join menusale in db.MenuSales on sale1.Id equals menusale.SaleId
            //              join menu1 in db.Menus on menusale.SaleId equals menu1.IdMenu
            //              select new
            //              {
            //                  a = menu1.NameDose,
            //                  b = menu1.Description,
            //                  c = menu1.Price * DiscountAfter,  //  sale1.Discount * menu1.Price  ,
            //                  d = menu1.ImageUri,
            //                  e = menu1.Categorya
            //              };

            resusultSale = resusultSale.Where(d => d.Categorya == 2);

            return View("Index", resusultSale);
        }
        public ActionResult Maindishes()
        {
            //var maindish = from m in db.Menus
            //               select m;
            CheckDiscountAfter1(WhoCheck , StatusRadios);
            resusultSale = resusultSale.Where(m => m.Categorya == 3);

            return View("Index", resusultSale);
        }

        public ActionResult Starters()
        {
            //var start = from m in db.Menus
            //            select m;

            resusultSale = resusultSale.Where(s => s.Categorya == 4);

            return View("Index", resusultSale);
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
