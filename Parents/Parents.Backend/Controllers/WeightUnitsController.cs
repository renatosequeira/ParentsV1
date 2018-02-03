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
    public class WeightUnitsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: WeightUnits
        public async Task<ActionResult> Index()
        {
            return View(await db.WeightUnits.ToListAsync());
        }

        // GET: WeightUnits/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeightUnits weightUnits = await db.WeightUnits.FindAsync(id);
            if (weightUnits == null)
            {
                return HttpNotFound();
            }
            return View(weightUnits);
        }

        // GET: WeightUnits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WeightUnits/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "WeightUnitId,WeightUnitDescription")] WeightUnits weightUnits)
        {
            if (ModelState.IsValid)
            {
                db.WeightUnits.Add(weightUnits);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(weightUnits);
        }

        // GET: WeightUnits/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeightUnits weightUnits = await db.WeightUnits.FindAsync(id);
            if (weightUnits == null)
            {
                return HttpNotFound();
            }
            return View(weightUnits);
        }

        // POST: WeightUnits/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "WeightUnitId,WeightUnitDescription")] WeightUnits weightUnits)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weightUnits).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(weightUnits);
        }

        // GET: WeightUnits/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeightUnits weightUnits = await db.WeightUnits.FindAsync(id);
            if (weightUnits == null)
            {
                return HttpNotFound();
            }
            return View(weightUnits);
        }

        // POST: WeightUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WeightUnits weightUnits = await db.WeightUnits.FindAsync(id);
            db.WeightUnits.Remove(weightUnits);
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
