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
    public class AlergyTypesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: AlergyTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.AlergyTypes.ToListAsync());
        }

        // GET: AlergyTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlergyType alergyType = await db.AlergyTypes.FindAsync(id);
            if (alergyType == null)
            {
                return HttpNotFound();
            }
            return View(alergyType);
        }

        // GET: AlergyTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AlergyTypes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AlergyType alergyType)
        {
            if (ModelState.IsValid)
            {
                db.AlergyTypes.Add(alergyType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(alergyType);
        }

        // GET: AlergyTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlergyType alergyType = await db.AlergyTypes.FindAsync(id);
            if (alergyType == null)
            {
                return HttpNotFound();
            }
            return View(alergyType);
        }

        // POST: AlergyTypes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AlergyType alergyType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alergyType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(alergyType);
        }

        // GET: AlergyTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlergyType alergyType = await db.AlergyTypes.FindAsync(id);
            if (alergyType == null)
            {
                return HttpNotFound();
            }
            return View(alergyType);
        }

        // POST: AlergyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AlergyType alergyType = await db.AlergyTypes.FindAsync(id);
            db.AlergyTypes.Remove(alergyType);
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
