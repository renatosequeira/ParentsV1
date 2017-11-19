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
    [Authorize]
    public class MedicineDosagesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: MedicineDosages
        public async Task<ActionResult> Index()
        {
            return View(await db.MedicineDosages.ToListAsync());
        }

        // GET: MedicineDosages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineDosage medicineDosage = await db.MedicineDosages.FindAsync(id);
            if (medicineDosage == null)
            {
                return HttpNotFound();
            }
            return View(medicineDosage);
        }

        // GET: MedicineDosages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicineDosages/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MedicineDosageId,MedicineDosageDescription")] MedicineDosage medicineDosage)
        {
            if (ModelState.IsValid)
            {
                db.MedicineDosages.Add(medicineDosage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(medicineDosage);
        }

        // GET: MedicineDosages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineDosage medicineDosage = await db.MedicineDosages.FindAsync(id);
            if (medicineDosage == null)
            {
                return HttpNotFound();
            }
            return View(medicineDosage);
        }

        // POST: MedicineDosages/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MedicineDosageId,MedicineDosageDescription")] MedicineDosage medicineDosage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicineDosage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(medicineDosage);
        }

        // GET: MedicineDosages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineDosage medicineDosage = await db.MedicineDosages.FindAsync(id);
            if (medicineDosage == null)
            {
                return HttpNotFound();
            }
            return View(medicineDosage);
        }

        // POST: MedicineDosages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MedicineDosage medicineDosage = await db.MedicineDosages.FindAsync(id);
            db.MedicineDosages.Remove(medicineDosage);
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
