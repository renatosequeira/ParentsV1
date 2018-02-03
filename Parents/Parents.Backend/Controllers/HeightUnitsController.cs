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
using Parents.Domain.Sistema;
using Parents.Backend.Models;

namespace Parents.Backend.Controllers
{
    public class HeightUnitsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: HeightUnits
        public async Task<ActionResult> Index()
        {
            return View(await db.HeightUnits.ToListAsync());
        }

        // GET: HeightUnits/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeightUnits heightUnits = await db.HeightUnits.FindAsync(id);
            if (heightUnits == null)
            {
                return HttpNotFound();
            }
            return View(heightUnits);
        }

        // GET: HeightUnits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HeightUnits/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "WeightUnitId,WeightUnitDescription")] HeightUnits heightUnits)
        {
            if (ModelState.IsValid)
            {
                db.HeightUnits.Add(heightUnits);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(heightUnits);
        }

        // GET: HeightUnits/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeightUnits heightUnits = await db.HeightUnits.FindAsync(id);
            if (heightUnits == null)
            {
                return HttpNotFound();
            }
            return View(heightUnits);
        }

        // POST: HeightUnits/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "WeightUnitId,WeightUnitDescription")] HeightUnits heightUnits)
        {
            if (ModelState.IsValid)
            {
                db.Entry(heightUnits).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(heightUnits);
        }

        // GET: HeightUnits/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeightUnits heightUnits = await db.HeightUnits.FindAsync(id);
            if (heightUnits == null)
            {
                return HttpNotFound();
            }
            return View(heightUnits);
        }

        // POST: HeightUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HeightUnits heightUnits = await db.HeightUnits.FindAsync(id);
            db.HeightUnits.Remove(heightUnits);
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
