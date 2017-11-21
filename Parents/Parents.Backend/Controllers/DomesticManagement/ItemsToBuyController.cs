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
using Parents.Backend.Helpers;
using Parents.Backend.Models.DomesticManagement;

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
        public async Task<ActionResult> Create(ItemsToBuyView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var itemToBuy = ToItemToBuy(view);
                itemToBuy.Image = pic;

                db.ItemToBuys.Add(itemToBuy);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ItemCategoryId = new SelectList(db.ItemsCategories, "ItemCategoryId", "ItemCategoryDescription", view.ItemCategoryId);
            return View(view);
        }

        private ItemToBuy ToItemToBuy(ItemsToBuyView view)
        {
            return new ItemToBuy
            {
                Image = view.Image,
                ItemCategoryId = view.ItemCategoryId,
                ItemsCategory = view.ItemsCategory,
                ItemToBuyAssignment = view.ItemToBuyAssignment,
                ItemToBuyDateAdded = view.ItemToBuyDateAdded,
                ItemToBuyDescription = view.ItemToBuyDescription,
                ItemToBuyId = view.ItemToBuyId,
                ItemToBuyOwner = view.ItemToBuyOwner,
                ItemToBuyStatus = view.ItemToBuyStatus
            };
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

            var view = ToView(itemToBuy);

            return View(view);
        }

        private ItemsToBuyView ToView(ItemToBuy itemToBuy)
        {
            return new ItemsToBuyView
            {
                Image = itemToBuy.Image,
                ItemCategoryId = itemToBuy.ItemCategoryId,
                ItemsCategory = itemToBuy.ItemsCategory,
                ItemToBuyAssignment = itemToBuy.ItemToBuyAssignment,
                ItemToBuyDateAdded = itemToBuy.ItemToBuyDateAdded,
                ItemToBuyDescription = itemToBuy.ItemToBuyDescription,
                ItemToBuyId = itemToBuy.ItemToBuyId,
                ItemToBuyOwner = itemToBuy.ItemToBuyOwner,
                ItemToBuyStatus = itemToBuy.ItemToBuyStatus
            };
        }

        // POST: ItemsToBuy/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ItemsToBuyView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.Image;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var itemToBuy = ToItemToBuy(view);
                itemToBuy.Image = pic;


                db.Entry(itemToBuy).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ItemCategoryId = new SelectList(db.ItemsCategories, "ItemCategoryId", "ItemCategoryDescription", view.ItemCategoryId);
            return View(view);
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
