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
using Parents.Domain.DomesticManagement;
using Parents.Backend.Models;

namespace Parents.Backend.Controllers.DomesticManagement
{
    [Authorize]
    public class ItemsCategoriesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ItemsCategories
        public async Task<ActionResult> Index()
        {
            return View(await db.ItemsCategories.ToListAsync());
        }

        // GET: ItemsCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemsCategory itemsCategory = await db.ItemsCategories.FindAsync(id);
            if (itemsCategory == null)
            {
                return HttpNotFound();
            }
            return View(itemsCategory);
        }

        // GET: ItemsCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemsCategories/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ItemCategoryId,ItemCategoryDescription")] ItemsCategory itemsCategory)
        {
            if (ModelState.IsValid)
            {
                db.ItemsCategories.Add(itemsCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(itemsCategory);
        }

        // GET: ItemsCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemsCategory itemsCategory = await db.ItemsCategories.FindAsync(id);
            if (itemsCategory == null)
            {
                return HttpNotFound();
            }
            return View(itemsCategory);
        }

        // POST: ItemsCategories/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ItemCategoryId,ItemCategoryDescription")] ItemsCategory itemsCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemsCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(itemsCategory);
        }

        // GET: ItemsCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemsCategory itemsCategory = await db.ItemsCategories.FindAsync(id);
            if (itemsCategory == null)
            {
                return HttpNotFound();
            }
            return View(itemsCategory);
        }

        // POST: ItemsCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ItemsCategory itemsCategory = await db.ItemsCategories.FindAsync(id);
            db.ItemsCategories.Remove(itemsCategory);
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
