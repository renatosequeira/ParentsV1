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
    public class MedicalInstitutionsPController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: MedicalInstitutionsP
        public async Task<ActionResult> Index()
        {
            return View(await db.MedicalInstitutions.ToListAsync());
        }

        // GET: MedicalInstitutionsP/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalInstitutions medicalInstitutions = await db.MedicalInstitutions.FindAsync(id);
            if (medicalInstitutions == null)
            {
                return HttpNotFound();
            }
            return View(medicalInstitutions);
        }

        // GET: MedicalInstitutionsP/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicalInstitutionsP/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MedicalInstitutionId,MedicalInstitutionName,MedicalInstitutionAddress")] MedicalInstitutions medicalInstitutions)
        {
            if (ModelState.IsValid)
            {
                db.MedicalInstitutions.Add(medicalInstitutions);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(medicalInstitutions);
        }

        // GET: MedicalInstitutionsP/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalInstitutions medicalInstitutions = await db.MedicalInstitutions.FindAsync(id);
            if (medicalInstitutions == null)
            {
                return HttpNotFound();
            }
            return View(medicalInstitutions);
        }

        // POST: MedicalInstitutionsP/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MedicalInstitutionId,MedicalInstitutionName,MedicalInstitutionAddress")] MedicalInstitutions medicalInstitutions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalInstitutions).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(medicalInstitutions);
        }

        // GET: MedicalInstitutionsP/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalInstitutions medicalInstitutions = await db.MedicalInstitutions.FindAsync(id);
            if (medicalInstitutions == null)
            {
                return HttpNotFound();
            }
            return View(medicalInstitutions);
        }

        // POST: MedicalInstitutionsP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MedicalInstitutions medicalInstitutions = await db.MedicalInstitutions.FindAsync(id);
            db.MedicalInstitutions.Remove(medicalInstitutions);
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
