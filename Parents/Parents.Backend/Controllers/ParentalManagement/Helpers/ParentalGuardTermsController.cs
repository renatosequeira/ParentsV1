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
using Parents.Domain.ParentalManagement.Helpers;
using Parents.Backend.Models;

namespace Parents.Backend.Controllers.ParentalManagement.Helpers
{
    public class ParentalGuardTermsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ParentalGuardTerms
        public async Task<ActionResult> Index()
        {
            return View(await db.ParentalGuardTerms.ToListAsync());
        }

        // GET: ParentalGuardTerms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentalGuardTerm parentalGuardTerm = await db.ParentalGuardTerms.FindAsync(id);
            if (parentalGuardTerm == null)
            {
                return HttpNotFound();
            }
            return View(parentalGuardTerm);
        }

        // GET: ParentalGuardTerms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParentalGuardTerms/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ParentalGuardTermId,ParentalGuardTermDescription,ParentalGuardTermRemarks")] ParentalGuardTerm parentalGuardTerm)
        {
            if (ModelState.IsValid)
            {
                db.ParentalGuardTerms.Add(parentalGuardTerm);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(parentalGuardTerm);
        }

        // GET: ParentalGuardTerms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentalGuardTerm parentalGuardTerm = await db.ParentalGuardTerms.FindAsync(id);
            if (parentalGuardTerm == null)
            {
                return HttpNotFound();
            }
            return View(parentalGuardTerm);
        }

        // POST: ParentalGuardTerms/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ParentalGuardTermId,ParentalGuardTermDescription,ParentalGuardTermRemarks")] ParentalGuardTerm parentalGuardTerm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parentalGuardTerm).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(parentalGuardTerm);
        }

        // GET: ParentalGuardTerms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentalGuardTerm parentalGuardTerm = await db.ParentalGuardTerms.FindAsync(id);
            if (parentalGuardTerm == null)
            {
                return HttpNotFound();
            }
            return View(parentalGuardTerm);
        }

        // POST: ParentalGuardTerms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ParentalGuardTerm parentalGuardTerm = await db.ParentalGuardTerms.FindAsync(id);
            db.ParentalGuardTerms.Remove(parentalGuardTerm);
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
