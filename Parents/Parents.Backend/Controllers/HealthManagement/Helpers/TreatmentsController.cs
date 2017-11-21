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
    public class TreatmentsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Treatments
        public async Task<ActionResult> Index()
        {
            var treatments = db.Treatments.Include(t => t.Medicine).Include(t => t.MedicineDosage).Include(t => t.MedicinePharmaceuticalForm);
            return View(await treatments.ToListAsync());
        }

        // GET: Treatments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = await db.Treatments.FindAsync(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // GET: Treatments/Create
        public ActionResult Create()
        {
            ViewBag.MedicineId = new SelectList(db.Medicines, "MedicineId", "MedicineName");
            ViewBag.MedicineDosageId = new SelectList(db.MedicineDosages, "MedicineDosageId", "MedicineDosageDescription");
            ViewBag.MedicinePharmaceuticalFormId = new SelectList(db.MedicinePharmaceuticalForms, "MedicinePharmaceuticalFormId", "MedicinePharmaceuticalFormDescription");
            return View();
        }

        // POST: Treatments/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TreatmentId,TreatmentRemarks,TreatmentStartDate,TreatmentEndDate,TreatmentStartHour,TreatmentRepeatsPerDay,TreatmentDosagePerRepeat,TreatmentStarted,TreadmentFinished,MedicineId,MedicineDosageId,MedicinePharmaceuticalFormId")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                db.Treatments.Add(treatment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MedicineId = new SelectList(db.Medicines, "MedicineId", "MedicineName", treatment.MedicineId);
            ViewBag.MedicineDosageId = new SelectList(db.MedicineDosages, "MedicineDosageId", "MedicineDosageDescription", treatment.MedicineDosageId);
            ViewBag.MedicinePharmaceuticalFormId = new SelectList(db.MedicinePharmaceuticalForms, "MedicinePharmaceuticalFormId", "MedicinePharmaceuticalFormDescription", treatment.MedicinePharmaceuticalFormId);
            return View(treatment);
        }

        // GET: Treatments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = await db.Treatments.FindAsync(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicineId = new SelectList(db.Medicines, "MedicineId", "MedicineName", treatment.MedicineId);
            ViewBag.MedicineDosageId = new SelectList(db.MedicineDosages, "MedicineDosageId", "MedicineDosageDescription", treatment.MedicineDosageId);
            ViewBag.MedicinePharmaceuticalFormId = new SelectList(db.MedicinePharmaceuticalForms, "MedicinePharmaceuticalFormId", "MedicinePharmaceuticalFormDescription", treatment.MedicinePharmaceuticalFormId);
            return View(treatment);
        }

        // POST: Treatments/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TreatmentId,TreatmentRemarks,TreatmentStartDate,TreatmentEndDate,TreatmentStartHour,TreatmentRepeatsPerDay,TreatmentDosagePerRepeat,TreatmentStarted,TreadmentFinished,MedicineId,MedicineDosageId,MedicinePharmaceuticalFormId")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treatment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MedicineId = new SelectList(db.Medicines, "MedicineId", "MedicineName", treatment.MedicineId);
            ViewBag.MedicineDosageId = new SelectList(db.MedicineDosages, "MedicineDosageId", "MedicineDosageDescription", treatment.MedicineDosageId);
            ViewBag.MedicinePharmaceuticalFormId = new SelectList(db.MedicinePharmaceuticalForms, "MedicinePharmaceuticalFormId", "MedicinePharmaceuticalFormDescription", treatment.MedicinePharmaceuticalFormId);
            return View(treatment);
        }

        // GET: Treatments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = await db.Treatments.FindAsync(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // POST: Treatments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Treatment treatment = await db.Treatments.FindAsync(id);
            db.Treatments.Remove(treatment);
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
