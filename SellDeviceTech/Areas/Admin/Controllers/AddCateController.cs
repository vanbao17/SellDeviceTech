using SellDeviceTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellDeviceTech.Areas.Admin.Controllers
{
    public class AddCateController : Controller
    {
        // GET: Admin/AddCate
        SellDeviceTechContext db = new SellDeviceTechContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add(Cates cate)
        {
            if (ModelState.IsValid)
            {
                db.Cates.Add(cate);
                db.SaveChanges();
                return RedirectToRoute(new { controller = "Cates", action = "Index" });
            }
            return RedirectToRoute(new { controller = "AddCate", action = "Index" });
        }
    }
}