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
    public class AlergiesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Alergies
        public async Task<ActionResult> Index()
        {
            var alergies = db.Alergies.Include(a => a.AlergyTypes);
            return View(await alergies.ToListAsync());
        }

        // GET: Alergies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alergy alergy = await db.Alergies.FindAsync(id);
            if (alergy == null)
            {
                return HttpNotFound();
            }
            return View(alergy);
        }

        // GET: Alergies/Create
        public ActionResult Create()
        {
            ViewBag.AlergyTypeId = new SelectList(db.AlergyTypes, "AlergyTypeId", "AlergyTypeDescriptiom");
            return View();
        }

        // POST: Alergies/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AlergyId,AlergyTypeId,AlergyDescription")] Alergy alergy)
        {
            if (ModelState.IsValid)
            {
                db.Alergies.Add(alergy);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AlergyTypeId = new SelectList(db.AlergyTypes, "AlergyTypeId", "AlergyTypeDescriptiom", alergy.AlergyTypeId);
            return View(alergy);
        }

        // GET: Alergies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alergy alergy = await db.Alergies.FindAsync(id);
            if (alergy == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlergyTypeId = new SelectList(db.AlergyTypes, "AlergyTypeId", "AlergyTypeDescriptiom", alergy.AlergyTypeId);
            return View(alergy);
        }

        // POST: Alergies/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AlergyId,AlergyTypeId,AlergyDescription")] Alergy alergy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alergy).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AlergyTypeId = new SelectList(db.AlergyTypes, "AlergyTypeId", "AlergyTypeDescriptiom", alergy.AlergyTypeId);
            return View(alergy);
        }

        // GET: Alergies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alergy alergy = await db.Alergies.FindAsync(id);
            if (alergy == null)
            {
                return HttpNotFound();
            }
            return View(alergy);
        }

        // POST: Alergies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Alergy alergy = await db.Alergies.FindAsync(id);
            db.Alergies.Remove(alergy);
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
