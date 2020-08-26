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
using FadlonRealEstate.Controllers;

namespace FadlonRealEstate.Controllers
{
    public class PropertiesController : Controller
    {
        private OfficeDB db = new OfficeDB();

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.name = "";
            return View(db.Properties.ToList());
        }

        [HttpPost]
        public ActionResult Index(string name)
        {
            ViewBag.name = name;
            var Property = db.Properties.ToList().Where(p => p.PropertyName.StartsWith(name));
            return View(Property.ToList());
        }

        // GET: Properties/Details/5
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
        public ActionResult Create([Bind(Include = "PropertyID,PropertyName,PropertyType,Stock,NumofRooms,city,Feautres,price")] Property property)
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
        public ActionResult Edit([Bind(Include = "PropertyID,PropertyName,PropertyType,Stock,NumofRooms,city,Feautres,price")] Property property)
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

        [HttpGet]
        public ActionResult Home()
        {
            return View(db.Properties.ToList());
        }

        [HttpGet]
        public ActionResult Gallery()
        {
            return View(db.Properties.ToList());
        }

        [HttpPost]
        public ActionResult Gallery(string name, string type, string city, string feat,int? price)
        {
            var properties = db.Properties.ToList().Where(p => (p.city.StartsWith(city) && p.PropertyType.StartsWith(type) && p.Feautres.StartsWith(feat)));
            //var properties = db.Properties.ToList().Where(p => (p.PropertyName.StartsWith(name)));
            if (price != null)
            {
                var b = properties.ToList().Where(p => p.price.Equals(price));
                return View(b.ToList());

            }

            return View(properties.ToList());
        }

    }
}
