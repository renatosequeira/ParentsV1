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
    public class DiseasesFamiliesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: DiseasesFamilies
        public async Task<ActionResult> Index()
        {
            return View(await db.DiseaseFamilies.ToListAsync());
        }

        // GET: DiseasesFamilies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiseaseFamily diseaseFamily = await db.DiseaseFamilies.FindAsync(id);
            if (diseaseFamily == null)
            {
                return HttpNotFound();
            }
            return View(diseaseFamily);
        }

        // GET: DiseasesFamilies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiseasesFamilies/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DiseaseFamily diseaseFamily)
        {
            if (ModelState.IsValid)
            {
                db.DiseaseFamilies.Add(diseaseFamily);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(diseaseFamily);
        }

        // GET: DiseasesFamilies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiseaseFamily diseaseFamily = await db.DiseaseFamilies.FindAsync(id);
            if (diseaseFamily == null)
            {
                return HttpNotFound();
            }
            return View(diseaseFamily);
        }

        // POST: DiseasesFamilies/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DiseaseFamily diseaseFamily)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diseaseFamily).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(diseaseFamily);
        }

        // GET: DiseasesFamilies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiseaseFamily diseaseFamily = await db.DiseaseFamilies.FindAsync(id);
            if (diseaseFamily == null)
            {
                return HttpNotFound();
            }
            return View(diseaseFamily);
        }

        // POST: DiseasesFamilies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DiseaseFamily diseaseFamily = await db.DiseaseFamilies.FindAsync(id);
            db.DiseaseFamilies.Remove(diseaseFamily);
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
