using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UWGBRestaurantAutomation.DAL;
using UWGBRestaurantAutomation.Models;
using UWGBRestaurantAutomation.Repository;

namespace UWGBRestaurantAutomation.Controllers
{
    public class OrdersController : Controller
    {
        private RestaurantContext db = new RestaurantContext();

        // GET: Orders
        public ActionResult Index()
        {
            // Check if Customer/Server/Cook and Display Accordingly
            if (Utility.GetRole(User.Identity.Name) == "customer")
            {
                var Customers = db.Customers.Where(x => x.CustomerEmail == User.Identity.Name).First();
                int CustomerID = Customers.CustomerId;
                if (Session["OrderNumber"] == null)
                {
                    var orders = db.Orders.Include(o => o.Customer).Where(x => x.CustomerId == CustomerID && x.OrderNumber == 0);
                    // Use ViewModel
                    var query = (from o in db.Orders
                                 join p in db.Products on o.ProductId equals p.ProductId
                                 where o.CustomerId == CustomerID && o.OrderNumber == 0
                                 select new CustomerViewModel
                                 {
                                     ProductImage = p.ProductImage,
                                     ProductName = p.ProductName,
                                     ProductPrice = p.ProductPrice,
                                     OrderId = o.OrderId,
                                     Ordering = false
                                 }).ToList().AsQueryable();

                    ViewBag.Ordering = false;
                    ViewBag.Role = "Customer";
                    return View(query);
                }
                else
                {
                    int OrderNumber = Convert.ToInt32(Session["OrderNumber"].ToString());
                    var orders = db.Orders.Include(o => o.Customer).Where(x => x.CustomerId == CustomerID && x.OrderNumber == OrderNumber);

                    // Use ViewModel
                    var query = (from o in db.Orders
                                 join p in db.Products on o.ProductId equals p.ProductId
                                 where o.CustomerId == CustomerID && o.OrderNumber == OrderNumber
                                 select new CustomerViewModel
                                 {
                                     ProductImage = p.ProductImage,
                                     ProductName = p.ProductName,
                                     ProductPrice = p.ProductPrice,
                                     OrderId = o.OrderId,
                                     Ordering = true
                                 }).ToList().AsQueryable();
                    ViewBag.Ordering = true;
                    ViewBag.Role = "Customer";
                    return View(query);
                }
            }
            if (Utility.GetRole(User.Identity.Name) == "server")
            {
                var orders = db.Orders.Include(o => o.Customer).Where(x => x.OrderDate >= DateTime.Today);

                // Use ViewModel
                var query = (from o in db.Orders
                             join p in db.Products on o.ProductId equals p.ProductId
                             select new ServerViewModel
                             {
                                 ProductImage = p.ProductImage,
                                 ProductName = p.ProductName,
                                 ProductPrice = p.ProductPrice,
                                 TableNumber = o.TableNumber,
                                 OrderDate = o.OrderDate,
                                 OrderNumber = o.OrderNumber
                             }).ToList().AsQueryable();

                ViewBag.Role = "Server";
                return View(query);
            }
            if (Utility.GetRole(User.Identity.Name) == "cook")
            {
                var orders = db.Orders.Include(o => o.Customer).Where(x => x.OrderDate >= DateTime.Today);
                ViewBag.Role = "Cook";
                return View(orders.ToList());
            }
            return View();
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create(int id)
        {
            // Populate an OrderNumber (if one doesn't exist) for the Customer session
            if(Session["OrderNumber"] == null)
            {
                Session["OrderNumber"] = GenerateOrderNumber();
            }
            // Populate TableNumber
            if (Session["TableNumber"] == null)
            {
                Session["TableNumber"] = GenerateTableNumber();
            }
            // Default Quantity
            var Quantity = 1;

            // Variables
            var OrderNumber = Int32.Parse(Session["OrderNumber"].ToString());
            ViewBag.OrderNumber = Session["OrderNumber"];
            ViewBag.TableNumber = Session["TableNumber"];
            ViewBag.OrderDate = DateTime.Now;

            // Preselected Product
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", id);

            // Get Customer Id to Preselct
            var customerId = db.Customers.Where(x => x.CustomerEmail == User.Identity.Name).Select(x => x.CustomerId).First();
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerEmail", customerId);

            // Create the Order
            var order = new Order();
            order.CustomerId = customerId;
            order.OrderDate = DateTime.Now;
            order.OrderNumber = Int32.Parse(Session["OrderNumber"].ToString());
            order.OrderQuantity = Quantity;
            order.ProductId = id;
            order.TableNumber = Int32.Parse(Session["TableNumber"].ToString());
            db.Orders.Add(order);
            db.SaveChanges();

            // Create the Payment (Invoice)
            // If payment already doesn't exist, create new, else update the same record with new total
            var paymentexist = db.Payments.Where(x => x.OrderNumber == OrderNumber).FirstOrDefault();
            if (paymentexist == null)
            {
                var payment = new Payment();
                payment.OrderNumber = OrderNumber;

                // Calculate the Total
                decimal total = 0;
                var query = db.Orders.Join(db.Products, ord => ord.ProductId, prod => prod.ProductId, (ord, prod) => new { Order = ord, Product = prod }).Where(x => x.Order.OrderNumber == OrderNumber).ToList();
                foreach (var val in query)
                {
                    // Get price from product db
                    total += val.Product.ProductPrice;
                }
                payment.Total = total;
                payment.PaymentDate = DateTime.Now;

                var db2 = new RestaurantContext();
                db2.Payments.Add(payment);
                db2.SaveChanges();
            }
            else
            {
                var payment = db.Payments.Where(x => x.OrderNumber == OrderNumber).First();

                // Calculate the Total
                decimal total = 0;
                var query = db.Orders.Join(db.Products, ord => ord.ProductId, prod => prod.ProductId, (ord, prod) => new { Order = ord, Product = prod }).Where(x => x.Order.OrderNumber == OrderNumber).ToList();
                foreach (var val in query)
                {
                    // Get price from product db
                    total += val.Product.ProductPrice;
                }
                payment.Total = total;
                payment.PaymentDate = DateTime.Now;

                var db2 = new RestaurantContext();
                db2.Entry(payment).State = EntityState.Modified;
                db2.SaveChanges();
            }

            return RedirectToAction("Index", "Orders");
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,CustomerId,ProductId,OrderNumber,OrderQuantity,TableNumber,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,CustomerId,ProductId,OrderNumber,OrderQuantity,TableNumber,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public int GenerateOrderNumber()
        {
            int min = 1000;
            int max = 9999;
            Random random = new Random();
            return random.Next(min, max);
        }
        public int GenerateTableNumber()
        {
            int min = 1;
            int max = 15;
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
