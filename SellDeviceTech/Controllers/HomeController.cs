using SellDeviceTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellDeviceTech.Controllers
{
    public class HomeController : Controller
    {
        decimal tonggia = 0;
        SellDeviceTechContext db = new SellDeviceTechContext();
        public ActionResult Index()
        {
            if(Session["userid"]!=null)
            {
                if (Session["Cart"] == null)
                {
                    List<CartItem> listcartitem = new List<CartItem>();
                    var listcart = db.Cart.ToList();
                    foreach (var item in listcart)
                    {
                        if (item.CustomerId == (int)Session["userid"])
                        {
                            var sanpham = db.Products.SingleOrDefault(x => x.IdProduct == item.IdProduct);
                            tonggia = (decimal)(tonggia + (sanpham.Price * item.Quality));
                            CartItem cartItem = new CartItem
                            {
                                product = sanpham,
                                Quantity = (int)item.Quality,
                                IdCate = item.IdCart
                            };
                            listcartitem.Add(cartItem);
                        }
                    }
                    Session["Cart"] = listcartitem;
                    Session["SumPrice"] = tonggia;
                }
            }
            return View();
        }
    }
}