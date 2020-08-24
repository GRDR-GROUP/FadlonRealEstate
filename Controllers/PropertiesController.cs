using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FadlonRealEstate.Models;
using FadlonRealEstate.Controllers;
using static FadlonRealEstate.Controllers.PropertiesController;
using System.Collections.ObjectModel;

namespace FadlonRealEstate.Controllers
{
    public class PropertiesController : Controller
    {
        private OfficeDB db = new OfficeDB();
        public HomeController homec;


        public ActionResult Index()
        {
            ViewBag.name = "";
            return View(db.Properties.ToList());
        }


        [HttpPost]
        public ActionResult Index(string name)
        {
            ViewBag.name = name;

            var manager = db.Properties.ToList().Where(p => p.PropertyName.StartsWith(name));
            return View(manager.ToList());
        }


        // GET: Properties/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // GET: Properties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Properties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PropertyID,PropertyName,PropertyType,Stock")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Properties.Add(property);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(property);
        }

        // GET: Properties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PropertyID,PropertyName,PropertyType,Stock")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(property);
        }

        // GET: Properties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Property property = db.Properties.Find(id);
            db.Properties.Remove(property);
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

        public ActionResult Gallery()
        {
            return View();
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
        public ActionResult BasicStatistics()
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
