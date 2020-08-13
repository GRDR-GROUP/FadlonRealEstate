using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FadlonRealEstate.Models;
using FadlonRealEstate.Controllers;

namespace FadlonRealEstate.Controllers
{
    public class HomeController : Controller
    {
        IDictionary<string, string> BrokersMap = new Dictionary<string, string>();
        IDictionary<string, string> CustomersMap = new Dictionary<string, string>();
        public static string CustomersName = "";
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

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Gallery()
        {
            return View();
        }
    }
}