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
using Parents.Domain.ActivitiesManagement;
using Parents.Backend.Models;

namespace Parents.Backend.Controllers.ActivitiesManagement
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Activities
        public async Task<ActionResult> Index()
        {
            var activities = db.Activities.Include(a => a.ActivityFamily).Include(a => a.ActivityInstitutionType).Include(a => a.ActivityPeriodicity).Include(a => a.ActivityType).Include(a => a.ParentInCharge);
            return View(await activities.ToListAsync());
        }

        // GET: Activities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = await db.Activities.FindAsync(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            ViewBag.ActivityFamilyId = new SelectList(db.ActivityFamilies, "ActivityFamilyId", "ActivityFamilyDescription");
            ViewBag.ActivityInstitutionTypeId = new SelectList(db.ActivityInstitutionTypes, "ActivityInstitutionTypeId", "ActivityInstitutionTypeDescription");
            ViewBag.ActivityPeriodicityId = new SelectList(db.ActivityPeriodicities, "ActivityPeriodicityId", "ActivityPeriodicityDescription");
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "ActivityTypeId", "ActivityTypeDescription");
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName");
            return View();
        }

        // POST: Activities/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ActivityId,ActivityFamilyId,ActivityTypeId,ActivityDescription,ActivityDateStart,ActivityDateEnd,ActivityRemarks,ActivityPeriodicityId,ParentId,ActivityInstitutionTypeId,ActivityAddress")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityFamilyId = new SelectList(db.ActivityFamilies, "ActivityFamilyId", "ActivityFamilyDescription", activity.ActivityFamilyId);
            ViewBag.ActivityInstitutionTypeId = new SelectList(db.ActivityInstitutionTypes, "ActivityInstitutionTypeId", "ActivityInstitutionTypeDescription", activity.ActivityInstitutionTypeId);
            ViewBag.ActivityPeriodicityId = new SelectList(db.ActivityPeriodicities, "ActivityPeriodicityId", "ActivityPeriodicityDescription", activity.ActivityPeriodicityId);
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "ActivityTypeId", "ActivityTypeDescription", activity.ActivityTypeId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", activity.ParentId);
            return View(activity);
        }

        // GET: Activities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = await db.Activities.FindAsync(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityFamilyId = new SelectList(db.ActivityFamilies, "ActivityFamilyId", "ActivityFamilyDescription", activity.ActivityFamilyId);
            ViewBag.ActivityInstitutionTypeId = new SelectList(db.ActivityInstitutionTypes, "ActivityInstitutionTypeId", "ActivityInstitutionTypeDescription", activity.ActivityInstitutionTypeId);
            ViewBag.ActivityPeriodicityId = new SelectList(db.ActivityPeriodicities, "ActivityPeriodicityId", "ActivityPeriodicityDescription", activity.ActivityPeriodicityId);
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "ActivityTypeId", "ActivityTypeDescription", activity.ActivityTypeId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", activity.ParentId);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ActivityId,ActivityFamilyId,ActivityTypeId,ActivityDescription,ActivityDateStart,ActivityDateEnd,ActivityRemarks,ActivityPeriodicityId,ParentId,ActivityInstitutionTypeId,ActivityAddress")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityFamilyId = new SelectList(db.ActivityFamilies, "ActivityFamilyId", "ActivityFamilyDescription", activity.ActivityFamilyId);
            ViewBag.ActivityInstitutionTypeId = new SelectList(db.ActivityInstitutionTypes, "ActivityInstitutionTypeId", "ActivityInstitutionTypeDescription", activity.ActivityInstitutionTypeId);
            ViewBag.ActivityPeriodicityId = new SelectList(db.ActivityPeriodicities, "ActivityPeriodicityId", "ActivityPeriodicityDescription", activity.ActivityPeriodicityId);
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "ActivityTypeId", "ActivityTypeDescription", activity.ActivityTypeId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", activity.ParentId);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = await db.Activities.FindAsync(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Activity activity = await db.Activities.FindAsync(id);
            db.Activities.Remove(activity);
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
