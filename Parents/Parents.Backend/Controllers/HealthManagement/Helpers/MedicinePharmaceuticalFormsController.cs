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
    public class MedicinePharmaceuticalFormsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: MedicinePharmaceuticalForms
        public async Task<ActionResult> Index()
        {
            return View(await db.MedicinePharmaceuticalForms.ToListAsync());
        }

        // GET: MedicinePharmaceuticalForms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicinePharmaceuticalForm medicinePharmaceuticalForm = await db.MedicinePharmaceuticalForms.FindAsync(id);
            if (medicinePharmaceuticalForm == null)
            {
                return HttpNotFound();
            }
            return View(medicinePharmaceuticalForm);
        }

        // GET: MedicinePharmaceuticalForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicinePharmaceuticalForms/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MedicinePharmaceuticalForm medicinePharmaceuticalForm)
        {
            if (ModelState.IsValid)
            {
                db.MedicinePharmaceuticalForms.Add(medicinePharmaceuticalForm);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(medicinePharmaceuticalForm);
        }

        // GET: MedicinePharmaceuticalForms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicinePharmaceuticalForm medicinePharmaceuticalForm = await db.MedicinePharmaceuticalForms.FindAsync(id);
            if (medicinePharmaceuticalForm == null)
            {
                return HttpNotFound();
            }
            return View(medicinePharmaceuticalForm);
        }

        // POST: MedicinePharmaceuticalForms/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MedicinePharmaceuticalForm medicinePharmaceuticalForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicinePharmaceuticalForm).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(medicinePharmaceuticalForm);
        }

        // GET: MedicinePharmaceuticalForms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicinePharmaceuticalForm medicinePharmaceuticalForm = await db.MedicinePharmaceuticalForms.FindAsync(id);
            if (medicinePharmaceuticalForm == null)
            {
                return HttpNotFound();
            }
            return View(medicinePharmaceuticalForm);
        }

        // POST: MedicinePharmaceuticalForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MedicinePharmaceuticalForm medicinePharmaceuticalForm = await db.MedicinePharmaceuticalForms.FindAsync(id);
            db.MedicinePharmaceuticalForms.Remove(medicinePharmaceuticalForm);
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
