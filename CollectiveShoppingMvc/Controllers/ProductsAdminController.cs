using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CollectiveShoppingMvc.Models;

namespace CollectiveShoppingMvc.Controllers
{
    [Authorize(Roles="Admin")]
    public class ProductsAdminController : Controller
    {
        private ShoppingModelEntities db = new ShoppingModelEntities();

        // GET: ProductsAdmin
        public ActionResult Index()
        {
            var productSet = db.ProductSet.Include(p => p.Shop).Include(p => p.Category);
            return View(productSet.ToList());
        }

        // GET: ProductsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.ProductSet.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: ProductsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ShopId = new SelectList(db.ShopSet, "ShopId", "Name");
            ViewBag.CategoryId = new SelectList(db.CategorySet, "CategoryId", "Name");
            return View();
        }

        // POST: ProductsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,Unit,Date,Price,UnitQuantity,IsEnabled,ShopId,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.ProductSet.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShopId = new SelectList(db.ShopSet, "ShopId", "Name", product.ShopId);
            ViewBag.CategoryId = new SelectList(db.CategorySet, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // GET: ProductsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.ProductSet.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShopId = new SelectList(db.ShopSet, "ShopId", "Name", product.ShopId);
            ViewBag.CategoryId = new SelectList(db.CategorySet, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // POST: ProductsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Unit,Date,Price,UnitQuantity,IsEnabled,ShopId,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShopId = new SelectList(db.ShopSet, "ShopId", "Name", product.ShopId);
            ViewBag.CategoryId = new SelectList(db.CategorySet, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // GET: ProductsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.ProductSet.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: ProductsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.ProductSet.Find(id);
            db.ProductSet.Remove(product);
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
