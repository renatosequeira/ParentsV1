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
    public class DiseasesTypesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: DiseasesTypes
        public async Task<ActionResult> Index()
        {
            var diseaseTypes = db.DiseaseTypes.Include(d => d.DiseaseFamily);
            return View(await diseaseTypes.ToListAsync());
        }

        // GET: DiseasesTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiseaseType diseaseType = await db.DiseaseTypes.FindAsync(id);
            if (diseaseType == null)
            {
                return HttpNotFound();
            }
            return View(diseaseType);
        }

        // GET: DiseasesTypes/Create
        public ActionResult Create()
        {
            ViewBag.DiseaseFamilyId = new SelectList(db.DiseaseFamilies, "DiseaseFamilyId", "DiseaseFamilyDescription");
            return View();
        }

        // POST: DiseasesTypes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DiseaseTypeId,DiseaseFamilyId,DiseaseTypeDescription,DiseaseRemarks,DiseaseExternalLink")] DiseaseType diseaseType)
        {
            if (ModelState.IsValid)
            {
                db.DiseaseTypes.Add(diseaseType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DiseaseFamilyId = new SelectList(db.DiseaseFamilies, "DiseaseFamilyId", "DiseaseFamilyDescription", diseaseType.DiseaseFamilyId);
            return View(diseaseType);
        }

        // GET: DiseasesTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiseaseType diseaseType = await db.DiseaseTypes.FindAsync(id);
            if (diseaseType == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiseaseFamilyId = new SelectList(db.DiseaseFamilies, "DiseaseFamilyId", "DiseaseFamilyDescription", diseaseType.DiseaseFamilyId);
            return View(diseaseType);
        }

        // POST: DiseasesTypes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DiseaseTypeId,DiseaseFamilyId,DiseaseTypeDescription,DiseaseRemarks,DiseaseExternalLink")] DiseaseType diseaseType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diseaseType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DiseaseFamilyId = new SelectList(db.DiseaseFamilies, "DiseaseFamilyId", "DiseaseFamilyDescription", diseaseType.DiseaseFamilyId);
            return View(diseaseType);
        }

        // GET: DiseasesTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiseaseType diseaseType = await db.DiseaseTypes.FindAsync(id);
            if (diseaseType == null)
            {
                return HttpNotFound();
            }
            return View(diseaseType);
        }

        // POST: DiseasesTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DiseaseType diseaseType = await db.DiseaseTypes.FindAsync(id);
            db.DiseaseTypes.Remove(diseaseType);
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
