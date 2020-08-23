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
        public static string CustomerName = "";
        private OfficeDB db = new OfficeDB();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string name, string pass)
        {
            ViewBag.name = name;
            ViewBag.pass = pass;

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
                if (BrokersMap[name].Equals(pass))
                {
                    return RedirectToAction("BrokerHome");
                }
                else return RedirectToAction("Index");
            }
            else if (CustomersMap.ContainsKey(name))
            {
                if (CustomersMap[name].Equals(pass))
                {
                    CustomerName = name;

                    return RedirectToAction("CustomerHome");
                }
                else return RedirectToAction("Index");
            }
            else return RedirectToAction("Index");
        }

        public ActionResult Gallery()
        {
            return View();
        }

        public ActionResult CustomerHome()
        {
            var deals =(from po in db.Customers
                       join lo in db.Deals
                       on po.CustomerID equals lo.CustomerID
                       where po.CustomerFirstName.StartsWith(CustomerName)
                       select lo);

            var property= (from bo in db.Properties
                           join lo in deals
                           on bo.PropertyID equals lo.PropertyID
                           where bo.PropertyID == lo.PropertyID
                           select new { PropertyName = bo.PropertyName, Type = bo.PropertyType, Price = bo.price });


            ICollection<Stat> gList = new Collection<Stat>();
            
            var property2 = (from bo in db.Properties
                             join lo in deals
                             on bo.PropertyID equals lo.PropertyID
                             where bo.PropertyID == lo.PropertyID
                             group bo by bo.NumofRooms into j
                             select j);

            foreach (var v in property2)
            {
                gList.Add(new Stat(v.Key, v.Count())); 
            }

            int max = 0;
            foreach (var c in gList)
            {
                if(c.Values > max)
                {
                    max = c.Values;
                    ViewBag.type = c.Key;
                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            CustomerName = "";

            ViewBag.Admin = "";

            return View();
        }
    }
}