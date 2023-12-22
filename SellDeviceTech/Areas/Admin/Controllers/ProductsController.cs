using SellDeviceTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellDeviceTech.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Admin/Products
        SellDeviceTechContext db = new SellDeviceTechContext();
        public ActionResult Index()
        {
            var listsanpham = db.Products.ToList();
            var listcate = db.Cates.ToList();
            ViewBag.Cate = listcate;
            return View(listsanpham);
        }
        public ActionResult XoaSach(int masp)
        {
            Products products = db.Products.Find(masp);
            if (products != null)
            {
                db.Products.Remove(products);
                db.SaveChanges();
                return Redirect("https://localhost:44366/Admin/Products");
            }
            else
            {
                return RedirectToRoute(new { controller = "HomeAdmin", action = "Index" });
            }

        }
    }
}