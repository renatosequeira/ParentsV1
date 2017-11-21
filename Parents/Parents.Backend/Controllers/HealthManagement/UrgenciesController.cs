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
using Parents.Backend.Models.HealthManagement;
using Parents.Backend.Helpers;

namespace Parents.Backend.Controllers.HealthManagement
{
    [Authorize]
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
        public async Task<ActionResult> Create(UrgencyView view)
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

                var urgency = ToUrgency(view);
                urgency.UrgencyImage = pic;

                db.Urgencies.Add(urgency);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions, "MedicalInstitutionId", "MedicalInstitutionName", view.MedicalInstitutionId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", view.ParentId);
            ViewBag.UrgencyCategoryId = new SelectList(db.UrgencyCategories, "UrgencyCategoryId", "UrgencyCategoryDescription", view.UrgencyCategoryId);
            ViewBag.UrgencySeverityId = new SelectList(db.UrgencySeverities, "UrgencySeverityId", "UrgencySeverityDescription", view.UrgencySeverityId);
            return View(view);
        }

        private Urgency ToUrgency(UrgencyView view)
        {
            return new Urgency
            {
                MedicalInstitutionId = view.MedicalInstitutionId,
                MedicalInstitutions = view.MedicalInstitutions,
                ParentId = view.ParentId,
                ParentInCharge = view.ParentInCharge,
                UrgencyCategory = view.UrgencyCategory,
                UrgencyCategoryId = view.UrgencyCategoryId,
                UrgencyDateIn = view.UrgencyDateIn,
                UrgencyDateOut = view.UrgencyDateOut,
                UrgencyDescription = view.UrgencyDescription,
                UrgencyId = view.UrgencyId,
                UrgencyImage = view.UrgencyImage,
                UrgencySeverity = view.UrgencySeverity,
                UrgencySeverityId = view.UrgencySeverityId,
                UrgencyStatus = view.UrgencyStatus
            };
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

            var view = ToView(urgency);

            return View(view);
        }

        private UrgencyView ToView(Urgency urgency)
        {
            return new UrgencyView
            {
                MedicalInstitutionId = urgency.MedicalInstitutionId,
                MedicalInstitutions = urgency.MedicalInstitutions,
                ParentId = urgency.ParentId,
                ParentInCharge = urgency.ParentInCharge,
                UrgencyCategory = urgency.UrgencyCategory,
                UrgencyCategoryId = urgency.UrgencyCategoryId,
                UrgencyDateIn = urgency.UrgencyDateIn,
                UrgencyDateOut = urgency.UrgencyDateOut,
                UrgencyDescription = urgency.UrgencyDescription,
                UrgencyId = urgency.UrgencyId,
                UrgencyImage = urgency.UrgencyImage,
                UrgencySeverity = urgency.UrgencySeverity,
                UrgencySeverityId = urgency.UrgencySeverityId,
                UrgencyStatus = urgency.UrgencyStatus
            };
        }

        // POST: Urgencies/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UrgencyView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.UrgencyImage;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var urgency = ToUrgency(view);
                urgency.UrgencyImage = pic;


                db.Entry(urgency).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MedicalInstitutionId = new SelectList(db.MedicalInstitutions, "MedicalInstitutionId", "MedicalInstitutionName", view.MedicalInstitutionId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", view.ParentId);
            ViewBag.UrgencyCategoryId = new SelectList(db.UrgencyCategories, "UrgencyCategoryId", "UrgencyCategoryDescription", view.UrgencyCategoryId);
            ViewBag.UrgencySeverityId = new SelectList(db.UrgencySeverities, "UrgencySeverityId", "UrgencySeverityDescription", view.UrgencySeverityId);
            return View(view);
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
