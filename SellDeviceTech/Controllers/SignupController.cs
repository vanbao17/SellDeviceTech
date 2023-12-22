using SellDeviceTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellDeviceTech.Controllers
{
    public class SignupController : Controller
    {
        // GET: Signin
        SellDeviceTechContext db = new SellDeviceTechContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddAccount(Customers customers)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customers);
                db.SaveChanges();
                Session["UserName"] = (customers.FirstName+" " + customers.LastName).ToString();
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }
    }
}