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
    public class UrgencySeveritiesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: UrgencySeverities
        public async Task<ActionResult> Index()
        {
            return View(await db.UrgencySeverities.ToListAsync());
        }

        // GET: UrgencySeverities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrgencySeverity urgencySeverity = await db.UrgencySeverities.FindAsync(id);
            if (urgencySeverity == null)
            {
                return HttpNotFound();
            }
            return View(urgencySeverity);
        }

        // GET: UrgencySeverities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UrgencySeverities/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UrgencySeverity urgencySeverity)
        {
            if (ModelState.IsValid)
            {
                db.UrgencySeverities.Add(urgencySeverity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(urgencySeverity);
        }

        // GET: UrgencySeverities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrgencySeverity urgencySeverity = await db.UrgencySeverities.FindAsync(id);
            if (urgencySeverity == null)
            {
                return HttpNotFound();
            }
            return View(urgencySeverity);
        }

        // POST: UrgencySeverities/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UrgencySeverity urgencySeverity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urgencySeverity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(urgencySeverity);
        }

        // GET: UrgencySeverities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrgencySeverity urgencySeverity = await db.UrgencySeverities.FindAsync(id);
            if (urgencySeverity == null)
            {
                return HttpNotFound();
            }
            return View(urgencySeverity);
        }

        // POST: UrgencySeverities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UrgencySeverity urgencySeverity = await db.UrgencySeverities.FindAsync(id);
            db.UrgencySeverities.Remove(urgencySeverity);
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
