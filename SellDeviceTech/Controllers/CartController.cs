﻿using PayPal.Api;
using SellDeviceTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Transaction = PayPal.Api.Transaction;

namespace SellDeviceTech.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        SellDeviceTechContext db = new SellDeviceTechContext();
        decimal tonggia = 0;
        public ActionResult Index()
        {
            if (Session["UserName"] != null || Session["userGG"] != null)
            {
                decimal total = 0;
                if(Session["Cart"]==null )
                {
                    List<CartItem> listcartitem = new List<CartItem>();
                    var listcart = db.Cart.ToList();
                    foreach ( var item in listcart)
                    {
                        if (item.CustomerId ==(int) Session["userid"])
                        {
                            var sanpham = db.Products.SingleOrDefault(x => x.IdProduct == item.IdProduct);
                            
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
                }
                List<CartItem> productList = Session["Cart"] as List<CartItem>;


                foreach (CartItem product in productList)
                {
                    total = Math.Round((decimal)(total + product.product.Price));
                }
                return View(productList);
            }
            return RedirectToRoute(new { controller = "SignIn", action = "Index" });
        }
        public ActionResult AddToCart(int productId,int quantity)
        {
            if (Session["UserName"] != null || Session["userGG"] != null)
            {
                List<CartItem> listcartItem = new List<CartItem>();
                int id = (int)Session["userid"];
                Cart newItem = new Cart
                {
                    IdProduct = productId,
                    Quality = quantity,
                    CustomerId = id,
                };
                db.Cart.Add(newItem);
                db.SaveChanges();
                var carts = db.Cart.ToList();
                foreach (Cart cart in carts)
                {
                    var sanpham = db.Products.SingleOrDefault(x => x.IdProduct == cart.IdProduct);
                    if(sanpham!=null)
                    {
                        CartItem cartItem = new CartItem
                        {
                            product = sanpham,
                            Quantity = (int)cart.Quality,
                            IdCate = cart.IdCart
                        };
                        listcartItem.Add(cartItem);
                    }
                }
                Session["Cart"] = listcartItem;
                return RedirectToRoute(new { controller = "Cart", action = "Index" });
            }
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        public ActionResult RemoveItemCart(int productId) { 
            if (Session["Cart"] != null)
            {
                var carforremove = db.Cart.Find(productId);
                if (carforremove != null)
                {
                    db.Cart.Remove(carforremove);
                    db.SaveChanges();
                    return RedirectToRoute(new { controller = "Cart", action = "Index" });
                }
            }
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/cart/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            //on successful payment, show success page to user.  
            return RedirectToRoute(new { controller = "ThankPage", action = "Index" });
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new Item()
            {
                name = "Item Name comes here",
                currency = "USD",
                price = "1",
                quantity = "1",
                sku = "sku"
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "1"
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = "3", // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            var paypalOrderId = DateTime.Now.Ticks;
            transactionList.Add(new Transaction()
            {
                description = $"Invoice #{paypalOrderId}",
                invoice_number = paypalOrderId.ToString(), //Generate an Invoice No    
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }
    }
}