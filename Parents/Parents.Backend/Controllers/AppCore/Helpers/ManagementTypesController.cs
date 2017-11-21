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
using Parents.Domain.AppCore.Helpers;
using Parents.Backend.Models;

namespace Parents.Backend.Controllers.AppCore.Helpers
{
    public class ManagementTypesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ManagementTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.ManagementTypes.ToListAsync());
        }

        // GET: ManagementTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagementType managementType = await db.ManagementTypes.FindAsync(id);
            if (managementType == null)
            {
                return HttpNotFound();
            }
            return View(managementType);
        }

        // GET: ManagementTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagementTypes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ManagementTypreId,ManagementTypeDescription")] ManagementType managementType)
        {
            if (ModelState.IsValid)
            {
                db.ManagementTypes.Add(managementType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(managementType);
        }

        // GET: ManagementTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagementType managementType = await db.ManagementTypes.FindAsync(id);
            if (managementType == null)
            {
                return HttpNotFound();
            }
            return View(managementType);
        }

        // POST: ManagementTypes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ManagementTypreId,ManagementTypeDescription")] ManagementType managementType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(managementType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(managementType);
        }

        // GET: ManagementTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagementType managementType = await db.ManagementTypes.FindAsync(id);
            if (managementType == null)
            {
                return HttpNotFound();
            }
            return View(managementType);
        }

        // POST: ManagementTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ManagementType managementType = await db.ManagementTypes.FindAsync(id);
            db.ManagementTypes.Remove(managementType);
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
