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
    public class BloodInformationsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: BloodInformations
        public async Task<ActionResult> Index()
        {
            return View(await db.BloodInformations.ToListAsync());
        }

        // GET: BloodInformations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodInformation bloodInformation = await db.BloodInformations.FindAsync(id);
            if (bloodInformation == null)
            {
                return HttpNotFound();
            }
            return View(bloodInformation);
        }

        // GET: BloodInformations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BloodInformations/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BoodInformationId,BloodInformationDescription,BloodInformationRemarks")] BloodInformation bloodInformation)
        {
            if (ModelState.IsValid)
            {
                db.BloodInformations.Add(bloodInformation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bloodInformation);
        }

        // GET: BloodInformations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodInformation bloodInformation = await db.BloodInformations.FindAsync(id);
            if (bloodInformation == null)
            {
                return HttpNotFound();
            }
            return View(bloodInformation);
        }

        // POST: BloodInformations/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BoodInformationId,BloodInformationDescription,BloodInformationRemarks")] BloodInformation bloodInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bloodInformation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bloodInformation);
        }

        // GET: BloodInformations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodInformation bloodInformation = await db.BloodInformations.FindAsync(id);
            if (bloodInformation == null)
            {
                return HttpNotFound();
            }
            return View(bloodInformation);
        }

        // POST: BloodInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BloodInformation bloodInformation = await db.BloodInformations.FindAsync(id);
            db.BloodInformations.Remove(bloodInformation);
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
