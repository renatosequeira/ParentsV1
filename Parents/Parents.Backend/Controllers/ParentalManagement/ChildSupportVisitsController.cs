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

namespace Parents.Backend.Controllers.ParentalManagement
{
    [Authorize]
    public class ChildSupportVisitsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ChildSupportVisits
        public async Task<ActionResult> Index()
        {
            var childSupportVisits = db.ChildSupportVisits.Include(c => c.ChildSupportVisitType);
            return View(await childSupportVisits.ToListAsync());
        }

        // GET: ChildSupportVisits/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildSupportVisit childSupportVisit = await db.ChildSupportVisits.FindAsync(id);
            if (childSupportVisit == null)
            {
                return HttpNotFound();
            }
            return View(childSupportVisit);
        }

        // GET: ChildSupportVisits/Create
        public ActionResult Create()
        {
            ViewBag.ChildSupportVisitTypeId = new SelectList(db.ChildSupportVisitTypes, "ChildSupportVisitTypeId", "ChildSupportVisitTypeDescription");
            return View();
        }

        // POST: ChildSupportVisits/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ChildSupportVisit childSupportVisit)
        {
            if (ModelState.IsValid)
            {
                db.ChildSupportVisits.Add(childSupportVisit);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ChildSupportVisitTypeId = new SelectList(db.ChildSupportVisitTypes, "ChildSupportVisitTypeId", "ChildSupportVisitTypeDescription", childSupportVisit.ChildSupportVisitTypeId);
            return View(childSupportVisit);
        }

        // GET: ChildSupportVisits/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildSupportVisit childSupportVisit = await db.ChildSupportVisits.FindAsync(id);
            if (childSupportVisit == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChildSupportVisitTypeId = new SelectList(db.ChildSupportVisitTypes, "ChildSupportVisitTypeId", "ChildSupportVisitTypeDescription", childSupportVisit.ChildSupportVisitTypeId);
            return View(childSupportVisit);
        }

        // POST: ChildSupportVisits/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ChildSupportVisit childSupportVisit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(childSupportVisit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ChildSupportVisitTypeId = new SelectList(db.ChildSupportVisitTypes, "ChildSupportVisitTypeId", "ChildSupportVisitTypeDescription", childSupportVisit.ChildSupportVisitTypeId);
            return View(childSupportVisit);
        }

        // GET: ChildSupportVisits/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildSupportVisit childSupportVisit = await db.ChildSupportVisits.FindAsync(id);
            if (childSupportVisit == null)
            {
                return HttpNotFound();
            }
            return View(childSupportVisit);
        }

        // POST: ChildSupportVisits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ChildSupportVisit childSupportVisit = await db.ChildSupportVisits.FindAsync(id);
            db.ChildSupportVisits.Remove(childSupportVisit);
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
