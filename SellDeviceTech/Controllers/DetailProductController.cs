using SellDeviceTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellDeviceTech.Controllers
{
    public class DetailProductController : Controller
    {
        // GET: DetailProduct
        SellDeviceTechContext db = new SellDeviceTechContext();
        public ActionResult Index(int masanpham)
        {
            var sanpham = db.Products.SingleOrDefault(x => x.IdProduct == masanpham);
            return View(sanpham);
        }
        public ActionResult AddToCart(int masp)
        {
            if (Session["UserName"] != null || Session["userGG"] != null)
            {
                var product = db.Products.Find(masp);
                var cart = Session["Cart"] as List<Products> ?? new List<Products>();
                cart.Add(product);
                Session["Cart"] = cart;

                return RedirectToRoute(new { controller = "Cart", action = "Index" });
            }
            else
            {
                return RedirectToRoute(new { controller = "SignIn", action = "Index" });
            }
        }
    }
}