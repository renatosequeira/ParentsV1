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
    public class PhysicalCharacteristicsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: PhysicalCharacteristics
        public async Task<ActionResult> Index()
        {
            var physicalCharacteristics = db.PhysicalCharacteristics.Include(p => p.HumanBodyAreas).Include(p => p.PhysicalCharacteristicType);
            return View(await physicalCharacteristics.ToListAsync());
        }

        // GET: PhysicalCharacteristics/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhysicalCharacteristic physicalCharacteristic = await db.PhysicalCharacteristics.FindAsync(id);
            if (physicalCharacteristic == null)
            {
                return HttpNotFound();
            }
            return View(physicalCharacteristic);
        }

        // GET: PhysicalCharacteristics/Create
        public ActionResult Create()
        {
            ViewBag.HumanBodyAreaId = new SelectList(db.HumanBodyAreas, "HumanBodyAreaId", "HumanBodyAreaDescription");
            ViewBag.PhysicalCharacteristicTypeId = new SelectList(db.PhysicalCharacteristicTypes, "PhysicalCharacteristicTypeId", "PhysicalCharacteristicTypeDescription");
            return View();
        }

        // POST: PhysicalCharacteristics/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PhysicalCharacteristicId,PhysicalCharacteristicTypeId,HumanBodyAreaId,PhysicalCharacteristicDescription")] PhysicalCharacteristic physicalCharacteristic)
        {
            if (ModelState.IsValid)
            {
                db.PhysicalCharacteristics.Add(physicalCharacteristic);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.HumanBodyAreaId = new SelectList(db.HumanBodyAreas, "HumanBodyAreaId", "HumanBodyAreaDescription", physicalCharacteristic.HumanBodyAreaId);
            ViewBag.PhysicalCharacteristicTypeId = new SelectList(db.PhysicalCharacteristicTypes, "PhysicalCharacteristicTypeId", "PhysicalCharacteristicTypeDescription", physicalCharacteristic.PhysicalCharacteristicTypeId);
            return View(physicalCharacteristic);
        }

        // GET: PhysicalCharacteristics/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhysicalCharacteristic physicalCharacteristic = await db.PhysicalCharacteristics.FindAsync(id);
            if (physicalCharacteristic == null)
            {
                return HttpNotFound();
            }
            ViewBag.HumanBodyAreaId = new SelectList(db.HumanBodyAreas, "HumanBodyAreaId", "HumanBodyAreaDescription", physicalCharacteristic.HumanBodyAreaId);
            ViewBag.PhysicalCharacteristicTypeId = new SelectList(db.PhysicalCharacteristicTypes, "PhysicalCharacteristicTypeId", "PhysicalCharacteristicTypeDescription", physicalCharacteristic.PhysicalCharacteristicTypeId);
            return View(physicalCharacteristic);
        }

        // POST: PhysicalCharacteristics/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PhysicalCharacteristicId,PhysicalCharacteristicTypeId,HumanBodyAreaId,PhysicalCharacteristicDescription")] PhysicalCharacteristic physicalCharacteristic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(physicalCharacteristic).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HumanBodyAreaId = new SelectList(db.HumanBodyAreas, "HumanBodyAreaId", "HumanBodyAreaDescription", physicalCharacteristic.HumanBodyAreaId);
            ViewBag.PhysicalCharacteristicTypeId = new SelectList(db.PhysicalCharacteristicTypes, "PhysicalCharacteristicTypeId", "PhysicalCharacteristicTypeDescription", physicalCharacteristic.PhysicalCharacteristicTypeId);
            return View(physicalCharacteristic);
        }

        // GET: PhysicalCharacteristics/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhysicalCharacteristic physicalCharacteristic = await db.PhysicalCharacteristics.FindAsync(id);
            if (physicalCharacteristic == null)
            {
                return HttpNotFound();
            }
            return View(physicalCharacteristic);
        }

        // POST: PhysicalCharacteristics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PhysicalCharacteristic physicalCharacteristic = await db.PhysicalCharacteristics.FindAsync(id);
            db.PhysicalCharacteristics.Remove(physicalCharacteristic);
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
