using SellDeviceTech.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellDeviceTech.Areas.Admin.Controllers
{
    public class UpdateController : Controller
    {
        // GET: Admin/Update
        SellDeviceTechContext db = new SellDeviceTechContext();
        public ActionResult Index(int masanpham)
        {
            var listcate = db.Cates.ToList();
            ViewBag.ListCate = listcate;
            var sanpham = db.Products.Find(masanpham);
            return View(sanpham);
        }
        public ActionResult SuaSach(Products products, HttpPostedFileBase uploadhinh, string selectedValue, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    string filename = "";
                    filename = "book" + uploadhinh.FileName;
                    string path = Path.Combine(Server.MapPath("~/Assets/images/product/"), filename);
                    uploadhinh.SaveAs(path);
                    products.Image = filename;
                }

                products.IdCate = int.Parse(selectedValue);
                db.Update(products);
                db.SaveChanges();
                return Redirect("https://localhost:44366/Admin/Products"); ;
            }
            return View();
        }
    }
}