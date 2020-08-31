using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FadlonRealEstate.Models;
using FadlonRealEstate.Controllers;
using static FadlonRealEstate.Controllers.PropertiesController;


namespace FadlonRealEstate.Controllers
{
    public class HomeController : Controller
    {
        IDictionary<string, string> BrokersMap = new Dictionary<string, string>();
        IDictionary<string, string> CustomersMap = new Dictionary<string, string>();
        private OfficeDB db = new OfficeDB();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string name, string password)
        {
            TempData["name"] = name.ToString();

            foreach (Broker b in db.Brokers)
            {
                BrokersMap.Add(b.BrokerName, b.BrokerPassword);
            }
            foreach (Customer c in db.Customers)
            {
                CustomersMap.Add(c.CustomerFirstName, c.Email);
            }
            if (BrokersMap.ContainsKey(name))
            {
                if (BrokersMap[name].Equals(password))
                {
                    TempData["Role"] = "Admin";
                }
            }
            else if (CustomersMap.ContainsKey(name))
            {
                if (CustomersMap[name].Equals(password))
                {
                    TempData["Role"] = "Customer";
                }
            }
            else
                TempData["Role"] = "Guest";
            TempData.Keep();

            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            TempData["name"] = null;
            TempData["Role"] = null;
            return RedirectToAction("Index");
        }
    }
}