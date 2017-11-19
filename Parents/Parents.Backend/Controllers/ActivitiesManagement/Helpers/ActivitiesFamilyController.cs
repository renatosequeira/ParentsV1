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
using Parents.Domain.ActivitiesManagement.Helpers;
using Parents.Backend.Models;

namespace Parents.Backend.Controllers.ActivitiesManagement.Helpers
{
    public class ActivitiesFamilyController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ActivitiesFamily
        public async Task<ActionResult> Index()
        {
            return View(await db.ActivityFamilies.ToListAsync());
        }

        // GET: ActivitiesFamily/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityFamily activityFamily = await db.ActivityFamilies.FindAsync(id);
            if (activityFamily == null)
            {
                return HttpNotFound();
            }
            return View(activityFamily);
        }

        // GET: ActivitiesFamily/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivitiesFamily/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ActivityFamilyId,ActivityFamilyDescription")] ActivityFamily activityFamily)
        {
            if (ModelState.IsValid)
            {
                db.ActivityFamilies.Add(activityFamily);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(activityFamily);
        }

        // GET: ActivitiesFamily/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityFamily activityFamily = await db.ActivityFamilies.FindAsync(id);
            if (activityFamily == null)
            {
                return HttpNotFound();
            }
            return View(activityFamily);
        }

        // POST: ActivitiesFamily/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ActivityFamilyId,ActivityFamilyDescription")] ActivityFamily activityFamily)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityFamily).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(activityFamily);
        }

        // GET: ActivitiesFamily/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityFamily activityFamily = await db.ActivityFamilies.FindAsync(id);
            if (activityFamily == null)
            {
                return HttpNotFound();
            }
            return View(activityFamily);
        }

        // POST: ActivitiesFamily/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ActivityFamily activityFamily = await db.ActivityFamilies.FindAsync(id);
            db.ActivityFamilies.Remove(activityFamily);
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
