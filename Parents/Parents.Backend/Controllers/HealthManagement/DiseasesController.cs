namespace Parents.Backend.Controllers.HealthManagement
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using Parents.Domain.HealthManagement;
    using Parents.Backend.Models;
    using Parents.Backend.Models.HealthManagement;
    using Parents.Backend.Helpers;
    using Parents.Domain.HealthManagement.Categories;

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
            disease.Treatment = new List<Treatment>
            {
                new Treatment
                {
                    TreatmentRemarks = "Test remark"
                }
            };
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
        public async Task<ActionResult> Create(DiseasesView view)
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

                var disease = ToDisease(view);
                disease.DiseaseImage = pic;

                db.Diseases.Add(disease);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DiseaseFamilyId = new SelectList(db.DiseaseFamilies, "DiseaseFamilyId", "DiseaseFamilyDescription", view.DiseaseFamilyId);
            ViewBag.DiseaseTypeId = new SelectList(db.DiseaseTypes, "DiseaseTypeId", "DiseaseTypeDescription", view.DiseaseTypeId);
            ViewBag.MedicineId = new SelectList(db.Medicines, "MedicineId", "MedicineName", view.MedicineId);
            ViewBag.MedicineDosageId = new SelectList(db.MedicineDosages, "MedicineDosageId", "MedicineDosageDescription", view.MedicineDosageId);
            ViewBag.MedicinePharmaceuticalFormId = new SelectList(db.MedicinePharmaceuticalForms, "MedicinePharmaceuticalFormId", "MedicinePharmaceuticalFormDescription", view.MedicinePharmaceuticalFormId);
            return View(view);
        }

        private Disease ToDisease(DiseasesView view)
        {
            return new Disease
            {
                DateCured = view.DateCured,
                DateDiagnosed = view.DateDiagnosed,
                DiseaseDescription = view.DiseaseDescription,
                DiseaseFamily = view.DiseaseFamily,
                DiseaseFamilyId = view.DiseaseFamilyId,
                DiseaseId = view.DiseaseId,
                DiseaseImage = view.DiseaseImage,
                DiseaseType = view.DiseaseType,
                DiseaseTypeId = view.DiseaseTypeId,
                HasAssociatedMedicines = view.HasAssociatedMedicines,
                HasGeneticOrigin = view.HasGeneticOrigin,
                IsTreatable = view.IsTreatable,
                Medicine = view.Medicine,
                MedicineDosage = view.MedicineDosage,
                MedicineDosageId = view.MedicineDosageId,
                MedicineId = view.MedicineId,
                MedicinePharmaceuticalForm = view.MedicinePharmaceuticalForm,
                MedicinePharmaceuticalFormId = view.MedicinePharmaceuticalFormId
            };
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

            var view = ToView(disease);

            return View(view);
        }

        private DiseasesView ToView(Disease disease)
        {
            return new DiseasesView
            {
                DateCured = disease.DateCured,
                DateDiagnosed = disease.DateDiagnosed,
                DiseaseDescription = disease.DiseaseDescription,
                DiseaseFamily = disease.DiseaseFamily,
                DiseaseFamilyId = disease.DiseaseFamilyId,
                DiseaseId = disease.DiseaseId,
                DiseaseImage = disease.DiseaseImage,
                DiseaseType = disease.DiseaseType,
                DiseaseTypeId = disease.DiseaseTypeId,
                HasAssociatedMedicines = disease.HasAssociatedMedicines,
                HasGeneticOrigin = disease.HasGeneticOrigin,
                IsTreatable = disease.IsTreatable,
                Medicine = disease.Medicine,
                MedicineDosage = disease.MedicineDosage,
                MedicineDosageId = disease.MedicineDosageId,
                MedicineId = disease.MedicineId,
                MedicinePharmaceuticalForm = disease.MedicinePharmaceuticalForm,
                MedicinePharmaceuticalFormId = disease.MedicinePharmaceuticalFormId
            };
        }

        // POST: Diseases/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DiseasesView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.DiseaseImage;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var disease = ToDisease(view);
                disease.DiseaseImage = pic;


                db.Entry(disease).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DiseaseFamilyId = new SelectList(db.DiseaseFamilies, "DiseaseFamilyId", "DiseaseFamilyDescription", view.DiseaseFamilyId);
            ViewBag.DiseaseTypeId = new SelectList(db.DiseaseTypes, "DiseaseTypeId", "DiseaseTypeDescription", view.DiseaseTypeId);
            ViewBag.MedicineId = new SelectList(db.Medicines, "MedicineId", "MedicineName", view.MedicineId);
            ViewBag.MedicineDosageId = new SelectList(db.MedicineDosages, "MedicineDosageId", "MedicineDosageDescription", view.MedicineDosageId);
            ViewBag.MedicinePharmaceuticalFormId = new SelectList(db.MedicinePharmaceuticalForms, "MedicinePharmaceuticalFormId", "MedicinePharmaceuticalFormDescription", view.MedicinePharmaceuticalFormId);
            return View(view);
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
