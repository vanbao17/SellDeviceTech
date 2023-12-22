using SellDeviceTech.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellDeviceTech.Areas.Admin.Controllers
{
    public class AddProductController : Controller
    {
        // GET: Admin/AddProduct
        SellDeviceTechContext db  = new SellDeviceTechContext();
        public ActionResult Index()
        {
            var listcate = db.Cates.ToList();
            ViewBag.ListCate = listcate;
            return View();
        }
        public ActionResult ThemSpMoi(Products products, HttpPostedFileBase uploadhinh, string selectedValue)
        {

            if (ModelState.IsValid)
            {

                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    string filename = "";
                    filename = "prod" + uploadhinh.FileName;
                    string path = Path.Combine(Server.MapPath("~/Assets/images/product/"), filename);
                    uploadhinh.SaveAs(path);
                    products.Image = filename;
                }
                products.IdCate = int.Parse(selectedValue);
                db.Products.Add(products);
                db.SaveChanges();
                return Redirect("https://localhost:44366/Admin/Products"); ;
            }
            return RedirectToRoute(new { controller = "AddProduct", action = "Index" });
        }
    }
}