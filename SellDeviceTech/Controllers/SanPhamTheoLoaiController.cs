using SellDeviceTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellDeviceTech.Controllers
{
    public class SanPhamTheoLoaiController : Controller
    {
        // GET: SanPhamTheoLoai]
        SellDeviceTechContext db = new SellDeviceTechContext(); 
        public ActionResult Index(int maloai)
        {
            List<Products> listsanpham = db.Products.Where(x => x.IdCate == maloai).OrderBy(x => x.TitleProduct).ToList();
            return View(listsanpham);
        }
    }
}