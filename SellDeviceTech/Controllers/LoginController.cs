using ASPSnippets.GoogleAPI;
using SellDeviceTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace SellDeviceTech.Controllers
{
    public class LoginController : Controller
    {
        // GET: Account
        SellDeviceTechContext db = new SellDeviceTechContext(); 
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
        }
        public ActionResult ActionSignIn(Customers customer)
        {
            if (Session["UserName"] == null)
            {
                var u = db.Customers.Where(x => x.Email.Equals(customer.Email) && x.Password.Equals(customer.Password)).FirstOrDefault();
                if (u != null)
                {
                    var route = new RouteValueDictionary();
                    if (u.RoleId == 1)
                    {
                        Session["UserName"] = (u.FirstName+" "+u.LastName).ToString();
                        route = new RouteValueDictionary {
                            { "controller", "Admin/Admin" },
                            { "action", "Index" }, // Thêm đối tượng user vào route values
                        };
                    }
                    else
                    {
                        Session["UserName"] = (u.FirstName + " " + u.LastName).ToString();
                        Session["userid"] = u.CustomerId;
                        route = new RouteValueDictionary {
                        { "controller", "Home" },
                        { "action", "Index" },
                        { "user", u } // Thêm đối tượng user vào route values
                        };

                    };

                    return RedirectToRoute(route);
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void LoginWithGooglePlus()
        {
            GoogleConnect.ClientId = "222966081099-58vsc5pfefgd3qhoomjtsqgdcorhlau3.apps.googleusercontent.com";
            GoogleConnect.ClientSecret = "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";
            GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];
            GoogleConnect.Authorize("profile", "email");
        }
        [ActionName("LoginWithGooglePlus")]
        public ActionResult LoginWithGooglePlusConfirmed()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                string code = Request.QueryString["code"];
                string json = GoogleConnect.Fetch("me", code);
                Profile profile = new JavaScriptSerializer().Deserialize<Profile>(json);

                SellDeviceTechContext db = new SellDeviceTechContext();

                GoogleProfile khachHang = new GoogleProfile();
                var u = db.GoogleProfile.Where(x => x.Id.Equals(profile.Id)).FirstOrDefault();
                if (u != null)
                {
                    var route = new RouteValueDictionary();
                    if (u.RoleId == 2)
                    {
                        Session["userGG"] = u;
                        route = new RouteValueDictionary {
                            { "controller", "Home" },
                            { "action", "Index" },
                        };

                    }
                    else
                    {
                        Session["userGG"] = u;
                        Session["UserName"] = u.DisplayName.ToString();
                        route = new RouteValueDictionary {
                            { "controller", "Admin/Admin" },
                            { "action", "Index" }, // Thêm đối tượng user vào route values
                        };
                    }
                    return RedirectToRoute(route);

                }
                khachHang.Id = profile.Id;
                khachHang.DisplayName = profile.name;
                khachHang.Email = profile.email;
                khachHang.Image = profile.picture;
                khachHang.RoleId = 1;
                db.GoogleProfile.Add(khachHang);

                Session["userGG"] = khachHang;
                db.SaveChanges();
            }
            if (Request.QueryString["error"] == "access_denied")
            {
                Session["userGG"] = null;
                return RedirectToAction("Index", "SignUp");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}