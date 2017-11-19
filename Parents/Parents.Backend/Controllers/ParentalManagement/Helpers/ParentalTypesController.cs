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
    public class ParentalTypesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ParentalTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.ParentalTypes.ToListAsync());
        }

        // GET: ParentalTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentalType parentalType = await db.ParentalTypes.FindAsync(id);
            if (parentalType == null)
            {
                return HttpNotFound();
            }
            return View(parentalType);
        }

        // GET: ParentalTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParentalTypes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ParentalTypeId,ParentalTypeDescription")] ParentalType parentalType)
        {
            if (ModelState.IsValid)
            {
                db.ParentalTypes.Add(parentalType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(parentalType);
        }

        // GET: ParentalTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentalType parentalType = await db.ParentalTypes.FindAsync(id);
            if (parentalType == null)
            {
                return HttpNotFound();
            }
            return View(parentalType);
        }

        // POST: ParentalTypes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ParentalTypeId,ParentalTypeDescription")] ParentalType parentalType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parentalType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(parentalType);
        }

        // GET: ParentalTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentalType parentalType = await db.ParentalTypes.FindAsync(id);
            if (parentalType == null)
            {
                return HttpNotFound();
            }
            return View(parentalType);
        }

        // POST: ParentalTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ParentalType parentalType = await db.ParentalTypes.FindAsync(id);
            db.ParentalTypes.Remove(parentalType);
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
