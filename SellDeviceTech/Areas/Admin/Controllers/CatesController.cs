using SellDeviceTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellDeviceTech.Areas.Admin.Controllers
{
    public class CatesController : Controller
    {
        // GET: Admin/Cates
        SellDeviceTechContext db = new SellDeviceTechContext(); 
        public ActionResult Index()
        {
            var listcate = db.Cates.ToList();
            return View(listcate);
        }
        public ActionResult XoaCate(int cateid)
        {
            Cates cate = db.Cates.Find(cateid);
            if (cate != null)
            {
                db.Cates.Remove(cate);
                db.SaveChanges();
                return RedirectToRoute(new { controller = "Cates", action = "Index" });
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "Index" });
            }
        }
    }
}