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
    public class ItemsToBuyController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ItemsToBuy
        public async Task<ActionResult> Index()
        {
            var itemToBuys = db.ItemToBuys.Include(i => i.ItemsCategory);
            return View(await itemToBuys.ToListAsync());
        }

        // GET: ItemsToBuy/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemToBuy itemToBuy = await db.ItemToBuys.FindAsync(id);
            if (itemToBuy == null)
            {
                return HttpNotFound();
            }
            return View(itemToBuy);
        }

        // GET: ItemsToBuy/Create
        public ActionResult Create()
        {
            ViewBag.ItemCategoryId = new SelectList(db.ItemsCategories, "ItemCategoryId", "ItemCategoryDescription");
            return View();
        }

        // POST: ItemsToBuy/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ItemToBuy itemToBuy)
        {
            if (ModelState.IsValid)
            {
                db.ItemToBuys.Add(itemToBuy);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ItemCategoryId = new SelectList(db.ItemsCategories, "ItemCategoryId", "ItemCategoryDescription", itemToBuy.ItemCategoryId);
            return View(itemToBuy);
        }

        // GET: ItemsToBuy/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemToBuy itemToBuy = await db.ItemToBuys.FindAsync(id);
            if (itemToBuy == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemCategoryId = new SelectList(db.ItemsCategories, "ItemCategoryId", "ItemCategoryDescription", itemToBuy.ItemCategoryId);
            return View(itemToBuy);
        }

        // POST: ItemsToBuy/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ItemToBuy itemToBuy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemToBuy).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ItemCategoryId = new SelectList(db.ItemsCategories, "ItemCategoryId", "ItemCategoryDescription", itemToBuy.ItemCategoryId);
            return View(itemToBuy);
        }

        // GET: ItemsToBuy/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemToBuy itemToBuy = await db.ItemToBuys.FindAsync(id);
            if (itemToBuy == null)
            {
                return HttpNotFound();
            }
            return View(itemToBuy);
        }

        // POST: ItemsToBuy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ItemToBuy itemToBuy = await db.ItemToBuys.FindAsync(id);
            db.ItemToBuys.Remove(itemToBuy);
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
