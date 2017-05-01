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

namespace UWGBRestaurantAutomation.Controllers
{
    public class PaymentsController : Controller
    {
        private RestaurantContext db = new RestaurantContext();

        // GET: Payments
        public ActionResult Index()
        {
            // Check if Customer/Server/Cook and Display Accordingly
            if (User.Identity.Name.Contains("customer"))
            {
                ViewBag.Role = "Customer";
                return View(db.Payments.ToList());
            }
            if (User.Identity.Name.Contains("server"))
            {
                ViewBag.Role = "Server";
                return View(db.Payments.ToList());
            }
            if (User.Identity.Name.Contains("cook"))
            {
                ViewBag.Role = "Cook";
                return View(db.Payments.ToList());
            }
            return View();
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            // Get the Order Number and Retrieve Payment Model
            var OrderNumber = Int32.Parse(Session["OrderNumber"].ToString());
            var paymentId = db.Payments.Where(x => x.OrderNumber == OrderNumber).Select(x => x.PaymentId).First();
            Payment payment = db.Payments.Find(paymentId);
            return View(payment);
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentId,OrderNumber,Total,CCName,CCNumber,CCMonth,CCYear,CCSecurity,PaymentDate,Paid")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                //db.Payments.Add(payment);
                var OrderNumber = Int32.Parse(Session["OrderNumber"].ToString());
                var paymentId = db.Payments.Where(x => x.OrderNumber == OrderNumber).Select(x => x.PaymentId).First();
                payment.OrderNumber = OrderNumber;
                payment.PaymentId = paymentId;
                payment.Paid = true;
                payment.PaymentDate = DateTime.Now;
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Success");
            }

            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentId,OrderNumber,Total,CCName,CCNumber,CCMonth,CCYear,CCSecurity,PaymentDate")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Success()
        {
            return View("~/Views/Payments/Success.cshtml");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
