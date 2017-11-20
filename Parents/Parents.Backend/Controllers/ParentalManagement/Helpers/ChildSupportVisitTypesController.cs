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
    [Authorize]
    public class ChildSupportVisitTypesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ChildSupportVisitTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.ChildSupportVisitTypes.ToListAsync());
        }

        // GET: ChildSupportVisitTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildSupportVisitType childSupportVisitType = await db.ChildSupportVisitTypes.FindAsync(id);
            if (childSupportVisitType == null)
            {
                return HttpNotFound();
            }
            return View(childSupportVisitType);
        }

        // GET: ChildSupportVisitTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChildSupportVisitTypes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ChildSupportVisitType childSupportVisitType)
        {
            if (ModelState.IsValid)
            {
                db.ChildSupportVisitTypes.Add(childSupportVisitType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(childSupportVisitType);
        }

        // GET: ChildSupportVisitTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildSupportVisitType childSupportVisitType = await db.ChildSupportVisitTypes.FindAsync(id);
            if (childSupportVisitType == null)
            {
                return HttpNotFound();
            }
            return View(childSupportVisitType);
        }

        // POST: ChildSupportVisitTypes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ChildSupportVisitType childSupportVisitType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(childSupportVisitType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(childSupportVisitType);
        }

        // GET: ChildSupportVisitTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildSupportVisitType childSupportVisitType = await db.ChildSupportVisitTypes.FindAsync(id);
            if (childSupportVisitType == null)
            {
                return HttpNotFound();
            }
            return View(childSupportVisitType);
        }

        // POST: ChildSupportVisitTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ChildSupportVisitType childSupportVisitType = await db.ChildSupportVisitTypes.FindAsync(id);
            db.ChildSupportVisitTypes.Remove(childSupportVisitType);
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
