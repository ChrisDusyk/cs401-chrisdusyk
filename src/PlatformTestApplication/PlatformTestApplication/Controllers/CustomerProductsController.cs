using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlatformTestApplication.Models;

namespace PlatformTestApplication.Controllers
{
    public class CustomerProductsController : Controller
    {
        private CS401_DBEntities db = new CS401_DBEntities();

        // GET: CustomerProducts
        public ActionResult Index()
        {
            var customerProducts = db.CustomerProducts.Include(c => c.Product).Include(c => c.Employee).Include(c => c.Customer);
            return View(customerProducts.ToList());
        }

        // GET: CustomerProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProduct customerProduct = db.CustomerProducts.Find(id);
            if (customerProduct == null)
            {
                return HttpNotFound();
            }
            return View(customerProduct);
        }

        // GET: CustomerProducts/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductCode");
            ViewBag.SoldByID = new SelectList(db.Employees, "EmployeeID", "FirstName");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName");
            return View();
        }

        // POST: CustomerProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerProductID,ProductID,SoldByID,CustomerID,SoldDate")] CustomerProduct customerProduct)
        {
            if (ModelState.IsValid)
            {
                db.CustomerProducts.Add(customerProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductCode", customerProduct.ProductID);
            ViewBag.SoldByID = new SelectList(db.Employees, "EmployeeID", "FirstName", customerProduct.SoldByID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", customerProduct.CustomerID);
            return View(customerProduct);
        }

        // GET: CustomerProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProduct customerProduct = db.CustomerProducts.Find(id);
            if (customerProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductCode", customerProduct.ProductID);
            ViewBag.SoldByID = new SelectList(db.Employees, "EmployeeID", "FirstName", customerProduct.SoldByID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", customerProduct.CustomerID);
            return View(customerProduct);
        }

        // POST: CustomerProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerProductID,ProductID,SoldByID,CustomerID,SoldDate")] CustomerProduct customerProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductCode", customerProduct.ProductID);
            ViewBag.SoldByID = new SelectList(db.Employees, "EmployeeID", "FirstName", customerProduct.SoldByID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", customerProduct.CustomerID);
            return View(customerProduct);
        }

        // GET: CustomerProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProduct customerProduct = db.CustomerProducts.Find(id);
            if (customerProduct == null)
            {
                return HttpNotFound();
            }
            return View(customerProduct);
        }

        // POST: CustomerProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerProduct customerProduct = db.CustomerProducts.Find(id);
            db.CustomerProducts.Remove(customerProduct);
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
    }
}
