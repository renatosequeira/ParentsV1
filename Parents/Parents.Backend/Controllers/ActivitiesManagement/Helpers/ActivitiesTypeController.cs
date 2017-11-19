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
    public class ActivitiesTypeController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ActivitiesType
        public async Task<ActionResult> Index()
        {
            return View(await db.ActivityTypes.ToListAsync());
        }

        // GET: ActivitiesType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityType activityType = await db.ActivityTypes.FindAsync(id);
            if (activityType == null)
            {
                return HttpNotFound();
            }
            return View(activityType);
        }

        // GET: ActivitiesType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivitiesType/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ActivityTypeId,ActivityTypeDescription")] ActivityType activityType)
        {
            if (ModelState.IsValid)
            {
                db.ActivityTypes.Add(activityType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(activityType);
        }

        // GET: ActivitiesType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityType activityType = await db.ActivityTypes.FindAsync(id);
            if (activityType == null)
            {
                return HttpNotFound();
            }
            return View(activityType);
        }

        // POST: ActivitiesType/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ActivityTypeId,ActivityTypeDescription")] ActivityType activityType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(activityType);
        }

        // GET: ActivitiesType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityType activityType = await db.ActivityTypes.FindAsync(id);
            if (activityType == null)
            {
                return HttpNotFound();
            }
            return View(activityType);
        }

        // POST: ActivitiesType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ActivityType activityType = await db.ActivityTypes.FindAsync(id);
            db.ActivityTypes.Remove(activityType);
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
