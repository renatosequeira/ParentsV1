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
    public class PhysicalCharacteristicTypesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: PhysicalCharacteristicTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.PhysicalCharacteristicTypes.ToListAsync());
        }

        // GET: PhysicalCharacteristicTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhysicalCharacteristicType physicalCharacteristicType = await db.PhysicalCharacteristicTypes.FindAsync(id);
            if (physicalCharacteristicType == null)
            {
                return HttpNotFound();
            }
            return View(physicalCharacteristicType);
        }

        // GET: PhysicalCharacteristicTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhysicalCharacteristicTypes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PhysicalCharacteristicTypeId,PhysicalCharacteristicTypeDescription")] PhysicalCharacteristicType physicalCharacteristicType)
        {
            if (ModelState.IsValid)
            {
                db.PhysicalCharacteristicTypes.Add(physicalCharacteristicType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(physicalCharacteristicType);
        }

        // GET: PhysicalCharacteristicTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhysicalCharacteristicType physicalCharacteristicType = await db.PhysicalCharacteristicTypes.FindAsync(id);
            if (physicalCharacteristicType == null)
            {
                return HttpNotFound();
            }
            return View(physicalCharacteristicType);
        }

        // POST: PhysicalCharacteristicTypes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PhysicalCharacteristicTypeId,PhysicalCharacteristicTypeDescription")] PhysicalCharacteristicType physicalCharacteristicType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(physicalCharacteristicType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(physicalCharacteristicType);
        }

        // GET: PhysicalCharacteristicTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhysicalCharacteristicType physicalCharacteristicType = await db.PhysicalCharacteristicTypes.FindAsync(id);
            if (physicalCharacteristicType == null)
            {
                return HttpNotFound();
            }
            return View(physicalCharacteristicType);
        }

        // POST: PhysicalCharacteristicTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PhysicalCharacteristicType physicalCharacteristicType = await db.PhysicalCharacteristicTypes.FindAsync(id);
            db.PhysicalCharacteristicTypes.Remove(physicalCharacteristicType);
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
