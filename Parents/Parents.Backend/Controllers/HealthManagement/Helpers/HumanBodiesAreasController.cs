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
    public class HumanBodiesAreasController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: HumanBodiesAreas
        public async Task<ActionResult> Index()
        {
            return View(await db.HumanBodyAreas.ToListAsync());
        }

        // GET: HumanBodiesAreas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HumanBodyAreas humanBodyAreas = await db.HumanBodyAreas.FindAsync(id);
            if (humanBodyAreas == null)
            {
                return HttpNotFound();
            }
            return View(humanBodyAreas);
        }

        // GET: HumanBodiesAreas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HumanBodiesAreas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "HumanBodyAreaId,HumanBodyAreaDescription,HumanBodyAreaSide")] HumanBodyAreas humanBodyAreas)
        {
            if (ModelState.IsValid)
            {
                db.HumanBodyAreas.Add(humanBodyAreas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(humanBodyAreas);
        }

        // GET: HumanBodiesAreas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HumanBodyAreas humanBodyAreas = await db.HumanBodyAreas.FindAsync(id);
            if (humanBodyAreas == null)
            {
                return HttpNotFound();
            }
            return View(humanBodyAreas);
        }

        // POST: HumanBodiesAreas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "HumanBodyAreaId,HumanBodyAreaDescription,HumanBodyAreaSide")] HumanBodyAreas humanBodyAreas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(humanBodyAreas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(humanBodyAreas);
        }

        // GET: HumanBodiesAreas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HumanBodyAreas humanBodyAreas = await db.HumanBodyAreas.FindAsync(id);
            if (humanBodyAreas == null)
            {
                return HttpNotFound();
            }
            return View(humanBodyAreas);
        }

        // POST: HumanBodiesAreas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HumanBodyAreas humanBodyAreas = await db.HumanBodyAreas.FindAsync(id);
            db.HumanBodyAreas.Remove(humanBodyAreas);
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
