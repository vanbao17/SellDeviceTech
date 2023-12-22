using SellDeviceTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellDeviceTech.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        SellDeviceTechContext db = new SellDeviceTechContext();
        public ActionResult Index()
        {
            var products = db.Products.ToList();
            var listcate = db.Cates.ToList();
            Session["Cates"] = listcate;
            return View(products);
        }
    }
}