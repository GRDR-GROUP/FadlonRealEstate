using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FadlonRealEstate.Models;

namespace FadlonRealEstate.Controllers
{
    public class CustomersController : Controller
    {
        private OfficeDB db = new OfficeDB();

        // GET: Customers
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        [HttpPost]
        public ActionResult Index(string fname, string lname, string mail, int? phone)
        {
           var Customers = db.Customers.ToList().Where(p => (p.CustomerFirstName.StartsWith(fname) && p.CustomerLastName.StartsWith(lname) && p.Email.StartsWith(mail)));
            if (phone != null)
            {
                var b = Customers.ToList().Where(p => p.PhoneNumber.Equals(phone));
                return View(b.ToList());
            }
           return View(Customers.ToList());
        }
  
        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,CustomerFirstName,CustomerLastName,Email,PhoneNumber,Password,Age")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,CustomerFirstName,CustomerLastName,Email,PhoneNumber,Password,Age")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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

        public ActionResult Account()
        {
            if (TempData["name"] != null)
            {
                String CustomerName = TempData["name"].ToString();
                TempData.Keep();

                var deals = (from bo in db.Customers
                             join lo in db.Deals
                             on bo.CustomerID equals lo.CustomerID
                             where bo.CustomerFirstName.StartsWith(CustomerName)
                             select lo);


                var Asset = (from bo in db.Properties
                             join lo in deals
                             on bo.PropertyID equals lo.PropertyID
                             where bo.PropertyID == lo.PropertyID
                             select new { assetName = bo.PropertyName, type = bo.PropertyType });


                ICollection<Asset> list = new Collection<Asset>();

                foreach (var v in Asset)
                {
                    list.Add(new Asset(Asset.Count(), v.assetName, v.type));

                }

                ViewBag.data = list;
                ICollection<Stat> pList = new Collection<Stat>();

                var Asset2 = (from bo in db.Properties
                              join lo in deals
                              on bo.PropertyID equals lo.PropertyID
                              where bo.PropertyID == lo.PropertyID
                              group bo by bo.PropertyType into j
                              select j);

                foreach (var v in Asset2)
                {
                    pList.Add(new Stat(v.Key, v.Count()));
                }

                int max = 0;
                foreach (var c in pList)
                {
                    if (c.Values > max)
                    {
                        max = c.Values;
                        ViewBag.type = c.Key;
                    }
                }
                TempData.Keep();
                return View(db.Deals.ToList());
            }

            return RedirectToAction("Index");
        }
    }

     public class Asset
    {
        int num;
        public string assetName;
        public string assetType;

        public Asset() { }
        public Asset(int num, string assetName, string assetType)
        {
            this.num = num;
            this.assetName = assetName;
            this.assetType = assetType;
        }
    }
}

