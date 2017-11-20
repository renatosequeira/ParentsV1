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
    [Authorize]
    public class DiseasesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Diseases
        public async Task<ActionResult> Index()
        {
            var diseases = db.Diseases.Include(d => d.DiseaseFamily).Include(d => d.DiseaseType).Include(d => d.Medicine).Include(d => d.MedicineDosage).Include(d => d.MedicinePharmaceuticalForm);
            return View(await diseases.ToListAsync());
        }

        // GET: Diseases/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease disease = await db.Diseases.FindAsync(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        // GET: Diseases/Create
        public ActionResult Create()
        {
            ViewBag.DiseaseFamilyId = new SelectList(db.DiseaseFamilies, "DiseaseFamilyId", "DiseaseFamilyDescription");
            ViewBag.DiseaseTypeId = new SelectList(db.DiseaseTypes, "DiseaseTypeId", "DiseaseTypeDescription");
            ViewBag.MedicineId = new SelectList(db.Medicines, "MedicineId", "MedicineName");
            ViewBag.MedicineDosageId = new SelectList(db.MedicineDosages, "MedicineDosageId", "MedicineDosageDescription");
            ViewBag.MedicinePharmaceuticalFormId = new SelectList(db.MedicinePharmaceuticalForms, "MedicinePharmaceuticalFormId", "MedicinePharmaceuticalFormDescription");
            return View();
        }

        // POST: Diseases/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Disease disease)
        {
            if (ModelState.IsValid)
            {
                db.Diseases.Add(disease);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DiseaseFamilyId = new SelectList(db.DiseaseFamilies, "DiseaseFamilyId", "DiseaseFamilyDescription", disease.DiseaseFamilyId);
            ViewBag.DiseaseTypeId = new SelectList(db.DiseaseTypes, "DiseaseTypeId", "DiseaseTypeDescription", disease.DiseaseTypeId);
            ViewBag.MedicineId = new SelectList(db.Medicines, "MedicineId", "MedicineName", disease.MedicineId);
            ViewBag.MedicineDosageId = new SelectList(db.MedicineDosages, "MedicineDosageId", "MedicineDosageDescription", disease.MedicineDosageId);
            ViewBag.MedicinePharmaceuticalFormId = new SelectList(db.MedicinePharmaceuticalForms, "MedicinePharmaceuticalFormId", "MedicinePharmaceuticalFormDescription", disease.MedicinePharmaceuticalFormId);
            return View(disease);
        }

        // GET: Diseases/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease disease = await db.Diseases.FindAsync(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiseaseFamilyId = new SelectList(db.DiseaseFamilies, "DiseaseFamilyId", "DiseaseFamilyDescription", disease.DiseaseFamilyId);
            ViewBag.DiseaseTypeId = new SelectList(db.DiseaseTypes, "DiseaseTypeId", "DiseaseTypeDescription", disease.DiseaseTypeId);
            ViewBag.MedicineId = new SelectList(db.Medicines, "MedicineId", "MedicineName", disease.MedicineId);
            ViewBag.MedicineDosageId = new SelectList(db.MedicineDosages, "MedicineDosageId", "MedicineDosageDescription", disease.MedicineDosageId);
            ViewBag.MedicinePharmaceuticalFormId = new SelectList(db.MedicinePharmaceuticalForms, "MedicinePharmaceuticalFormId", "MedicinePharmaceuticalFormDescription", disease.MedicinePharmaceuticalFormId);
            return View(disease);
        }

        // POST: Diseases/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Disease disease)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disease).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DiseaseFamilyId = new SelectList(db.DiseaseFamilies, "DiseaseFamilyId", "DiseaseFamilyDescription", disease.DiseaseFamilyId);
            ViewBag.DiseaseTypeId = new SelectList(db.DiseaseTypes, "DiseaseTypeId", "DiseaseTypeDescription", disease.DiseaseTypeId);
            ViewBag.MedicineId = new SelectList(db.Medicines, "MedicineId", "MedicineName", disease.MedicineId);
            ViewBag.MedicineDosageId = new SelectList(db.MedicineDosages, "MedicineDosageId", "MedicineDosageDescription", disease.MedicineDosageId);
            ViewBag.MedicinePharmaceuticalFormId = new SelectList(db.MedicinePharmaceuticalForms, "MedicinePharmaceuticalFormId", "MedicinePharmaceuticalFormDescription", disease.MedicinePharmaceuticalFormId);
            return View(disease);
        }

        // GET: Diseases/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease disease = await db.Diseases.FindAsync(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        // POST: Diseases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Disease disease = await db.Diseases.FindAsync(id);
            db.Diseases.Remove(disease);
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
