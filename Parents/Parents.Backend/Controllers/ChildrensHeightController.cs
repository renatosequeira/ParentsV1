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
using Parents.Domain.HealthManagement;
using Parents.Backend.Models;

namespace Parents.Backend.Controllers
{
    public class ChildrensHeightController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ChildrensHeight
        public async Task<ActionResult> Index()
        {
            return View(await db.ChildrenHeights.ToListAsync());
        }

        // GET: ChildrensHeight/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildrenHeight childrenHeight = await db.ChildrenHeights.FindAsync(id);
            if (childrenHeight == null)
            {
                return HttpNotFound();
            }
            return View(childrenHeight);
        }

        // GET: ChildrensHeight/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChildrensHeight/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ChildrenHeightId,ChildrenId,HeightVaue,HeightUnit")] ChildrenHeight childrenHeight)
        {
            if (ModelState.IsValid)
            {
                db.ChildrenHeights.Add(childrenHeight);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(childrenHeight);
        }

        // GET: ChildrensHeight/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildrenHeight childrenHeight = await db.ChildrenHeights.FindAsync(id);
            if (childrenHeight == null)
            {
                return HttpNotFound();
            }
            return View(childrenHeight);
        }

        // POST: ChildrensHeight/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ChildrenHeightId,ChildrenId,HeightVaue,HeightUnit")] ChildrenHeight childrenHeight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(childrenHeight).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(childrenHeight);
        }

        // GET: ChildrensHeight/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildrenHeight childrenHeight = await db.ChildrenHeights.FindAsync(id);
            if (childrenHeight == null)
            {
                return HttpNotFound();
            }
            return View(childrenHeight);
        }

        // POST: ChildrensHeight/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ChildrenHeight childrenHeight = await db.ChildrenHeights.FindAsync(id);
            db.ChildrenHeights.Remove(childrenHeight);
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
