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
        public static int role = 2;


        public ActionResult Index()
        {
            if (role == 0)
                ViewBag.Role = "Admin";
            else if (role == 1)
                ViewBag.Role = "Customer";
            else
                ViewBag.Role = "Guest";
            return View();
        }

        public ActionResult About()
        {
            if (role == 0)
                ViewBag.Role = "Admin";
            else if (role == 1)
                ViewBag.Role = "Customer";
            else
                ViewBag.Role = "Guest";
            return View();
        }

        public ActionResult Contact()
        {
            if (role == 0)
                ViewBag.Role = "Admin";
            else if (role == 1)
                ViewBag.Role = "Customer";
            else
                ViewBag.Role = "Guest";
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (role == 0)
                ViewBag.Role = "Admin";
            else if (role == 1)
                ViewBag.Role = "Customer";
            else
                ViewBag.Role = "Guest";
            return View();
        }

            [HttpPost]
        public ActionResult Login(string name, string password)
        {
            ViewBag.name = name;
            ViewBag.pass = password;

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
                    role = 0;
                }
            }
            else if (CustomersMap.ContainsKey(name))
            {
                if (CustomersMap[name].Equals(password))
                {
                    CustomerName = name;
                    role = 1;
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            CustomerName = "";
            role = 2;
            return RedirectToAction("Index");
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
                //gList.Add(new Stat(v.Key, v.Count())); 
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
    }
}