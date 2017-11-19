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

namespace Parents.Backend.Controllers.HealthManagement
{
    public class UrgenciesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Urgencies
        public async Task<ActionResult> Index()
        {
            var urgencies = db.Urgencies.Include(u => u.MedicalInstitutions).Include(u => u.ParentInCharge).Include(u => u.UrgencyCategory).Include(u => u.UrgencySeverity);
            return View(await urgencies.ToListAsync());
        }

        // GET: Urgencies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urgency urgency = await db.Urgencies.FindAsync(id);
            if (urgency == null)
            {
                return HttpNotFound();
            }
            return View(urgency);
        }

        // GET: Urgencies/Create
        public ActionResult Create()
        {
            ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions, "MedicalInstitutionId", "MedicalInstitutionName");
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName");
            ViewBag.UrgencyCategoryId = new SelectList(db.UrgencyCategories, "UrgencyCategoryId", "UrgencyCategoryDescription");
            ViewBag.UrgencySeverityId = new SelectList(db.UrgencySeverities, "UrgencySeverityId", "UrgencySeverityDescription");
            return View();
        }

        // POST: Urgencies/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UrgencyId,UrgencyDescription,UrgencyDateIn,UrgencyDateOut,UrgencyStatus,UrgencySeverityId,UrgencyCategoryId,ParentId,MedicalInstitutionId")] Urgency urgency)
        {
            if (ModelState.IsValid)
            {
                db.Urgencies.Add(urgency);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions, "MedicalInstitutionId", "MedicalInstitutionName", urgency.MedicalInstitutionId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", urgency.ParentId);
            ViewBag.UrgencyCategoryId = new SelectList(db.UrgencyCategories, "UrgencyCategoryId", "UrgencyCategoryDescription", urgency.UrgencyCategoryId);
            ViewBag.UrgencySeverityId = new SelectList(db.UrgencySeverities, "UrgencySeverityId", "UrgencySeverityDescription", urgency.UrgencySeverityId);
            return View(urgency);
        }

        // GET: Urgencies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urgency urgency = await db.Urgencies.FindAsync(id);
            if (urgency == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions, "MedicalInstitutionId", "MedicalInstitutionName", urgency.MedicalInstitutionId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", urgency.ParentId);
            ViewBag.UrgencyCategoryId = new SelectList(db.UrgencyCategories, "UrgencyCategoryId", "UrgencyCategoryDescription", urgency.UrgencyCategoryId);
            ViewBag.UrgencySeverityId = new SelectList(db.UrgencySeverities, "UrgencySeverityId", "UrgencySeverityDescription", urgency.UrgencySeverityId);
            return View(urgency);
        }

        // POST: Urgencies/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UrgencyId,UrgencyDescription,UrgencyDateIn,UrgencyDateOut,UrgencyStatus,UrgencySeverityId,UrgencyCategoryId,ParentId,MedicalInstitutionId")] Urgency urgency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urgency).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions, "MedicalInstitutionId", "MedicalInstitutionName", urgency.MedicalInstitutionId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", urgency.ParentId);
            ViewBag.UrgencyCategoryId = new SelectList(db.UrgencyCategories, "UrgencyCategoryId", "UrgencyCategoryDescription", urgency.UrgencyCategoryId);
            ViewBag.UrgencySeverityId = new SelectList(db.UrgencySeverities, "UrgencySeverityId", "UrgencySeverityDescription", urgency.UrgencySeverityId);
            return View(urgency);
        }

        // GET: Urgencies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urgency urgency = await db.Urgencies.FindAsync(id);
            if (urgency == null)
            {
                return HttpNotFound();
            }
            return View(urgency);
        }

        // POST: Urgencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Urgency urgency = await db.Urgencies.FindAsync(id);
            db.Urgencies.Remove(urgency);
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
