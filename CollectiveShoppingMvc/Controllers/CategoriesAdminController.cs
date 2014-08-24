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
    public class CategoriesAdminController : Controller
    {
        private ShoppingModelEntities db = new ShoppingModelEntities();

        // GET: CategoriesAdmin
        public ActionResult Index()
        {
            var categorySet = db.CategorySet.Include(c => c.Shop);
            return View(categorySet.ToList());
        }

        // GET: CategoriesAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.CategorySet.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: CategoriesAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ShopId = new SelectList(db.ShopSet, "ShopId", "Name");
            return View();
        }

        // POST: CategoriesAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Name,IsEnabled,ShopId")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.CategorySet.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShopId = new SelectList(db.ShopSet, "ShopId", "Name", category.ShopId);
            return View(category);
        }

        // GET: CategoriesAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.CategorySet.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShopId = new SelectList(db.ShopSet, "ShopId", "Name", category.ShopId);
            return View(category);
        }

        // POST: CategoriesAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Name,IsEnabled,ShopId")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShopId = new SelectList(db.ShopSet, "ShopId", "Name", category.ShopId);
            return View(category);
        }

        // GET: CategoriesAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.CategorySet.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: CategoriesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.CategorySet.Find(id);
            db.CategorySet.Remove(category);
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
