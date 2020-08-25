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
    public class DealsController : Controller
    {
        private OfficeDB db = new OfficeDB();

        // GET: Deals
        public ActionResult Index()
        {
            var deals = db.Deals.Include(d => d.Customer).Include(d => d.Property);
            return View(deals.ToList());
        }
        [HttpPost]
        public ActionResult Index(string item)
        {
            ViewBag.item = item;

            var Itemsid = (from po in db.Properties
                           join lo in db.Deals
                           on po.PropertyID equals lo.PropertyID
                           where po.city.StartsWith(item)
                           select lo);

            return View(Itemsid.ToList());

        }

        // GET: Deals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            return View(deal);
        }

        // GET: Deals/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerFirstName");
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "PropertyName");
            return View();
        }

        // POST: Deals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DealID,CustomerID,PropertyID,Active")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                db.Deals.Add(deal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerFirstName", deal.CustomerID);
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "PropertyName", deal.PropertyID);
            return View(deal);
        }

        // GET: Deals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerFirstName", deal.CustomerID);
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "PropertyName", deal.PropertyID);
            return View(deal);
        }

        // POST: Deals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DealID,CustomerID,PropertyID,Active")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerFirstName", deal.CustomerID);
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "PropertyName", deal.PropertyID);
            return View(deal);
        }

        // GET: Deals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            return View(deal);
        }

        // POST: Deals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deal deal = db.Deals.Find(id);
            db.Deals.Remove(deal);
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

        [HttpGet]
        public ActionResult Statistics()
        {
            ICollection<Stat> mylist = new Collection<Stat>();
            var r = (from bo in db.Properties
                     group bo by bo.PropertyType into j
                     select j);

            foreach (var v in r)
            {
                mylist.Add(new Stat(v.Key, v.Count()));
            }

            ViewBag.data = mylist;

            ICollection<Stat> mylist2 = new Collection<Stat>();

            var q = (from lo in db.Deals
                     join bo in db.Properties
                     on lo.PropertyID equals bo.PropertyID
                     where lo.PropertyID == bo.PropertyID
                     group bo by bo.PropertyName into j
                     select j);

            foreach (var v in q)
            {
                mylist2.Add(new Stat(v.Key, v.Count()));

            }

            ViewBag.data2 = mylist2;

            return View();
        }


        [HttpGet]
        public ActionResult Group()
        {
            var group = (from bo in db.Properties
                         group bo by bo.PropertyType into j
                         select new Group<string, Property> { Key = j.Key, Values = j });

            return View(group.ToList());
        }


    }

    public class Group<K, T>
    {
        public K Key { get; set; }
        public IEnumerable<T> Values { get; set; }
    }
    public class Stat
    {
        public string Key;
        public int Values;

        public Stat(string key, int values)
        {
            Key = key;
            Values = values;
        }
    }
}
