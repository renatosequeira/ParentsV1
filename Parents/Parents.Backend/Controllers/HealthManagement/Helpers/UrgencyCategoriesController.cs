using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Parents.Domain;
using Parents.Domain.HealthManagement.Categories;
using Parents.Backend.Models;

namespace Parents.Backend.Controllers.HealthManagement.Helpers
{
    public class UrgencyCategoriesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: UrgencyCategories
        public async Task<ActionResult> Index()
        {
            return View(await db.UrgencyCategories.ToListAsync());
        }

        // GET: UrgencyCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrgencyCategory urgencyCategory = await db.UrgencyCategories.FindAsync(id);
            if (urgencyCategory == null)
            {
                return HttpNotFound();
            }
            return View(urgencyCategory);
        }

        // GET: UrgencyCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UrgencyCategories/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UrgencyCategoryId,UrgencyCategoryDescription")] UrgencyCategory urgencyCategory)
        {
            if (ModelState.IsValid)
            {
                db.UrgencyCategories.Add(urgencyCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(urgencyCategory);
        }

        // GET: UrgencyCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrgencyCategory urgencyCategory = await db.UrgencyCategories.FindAsync(id);
            if (urgencyCategory == null)
            {
                return HttpNotFound();
            }
            return View(urgencyCategory);
        }

        // POST: UrgencyCategories/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UrgencyCategoryId,UrgencyCategoryDescription")] UrgencyCategory urgencyCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urgencyCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(urgencyCategory);
        }

        // GET: UrgencyCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrgencyCategory urgencyCategory = await db.UrgencyCategories.FindAsync(id);
            if (urgencyCategory == null)
            {
                return HttpNotFound();
            }
            return View(urgencyCategory);
        }

        // POST: UrgencyCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UrgencyCategory urgencyCategory = await db.UrgencyCategories.FindAsync(id);
            db.UrgencyCategories.Remove(urgencyCategory);
            await db.SaveChangesAsync();
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
