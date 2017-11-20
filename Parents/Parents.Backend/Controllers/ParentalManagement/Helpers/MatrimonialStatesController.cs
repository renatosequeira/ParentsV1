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
    public class MatrimonialStatesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: MatrimonialStates
        public async Task<ActionResult> Index()
        {
            return View(await db.MatrimonialStates.ToListAsync());
        }

        // GET: MatrimonialStates/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatrimonialState matrimonialState = await db.MatrimonialStates.FindAsync(id);
            if (matrimonialState == null)
            {
                return HttpNotFound();
            }
            return View(matrimonialState);
        }

        // GET: MatrimonialStates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MatrimonialStates/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MatrimonialState matrimonialState)
        {
            if (ModelState.IsValid)
            {
                db.MatrimonialStates.Add(matrimonialState);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(matrimonialState);
        }

        // GET: MatrimonialStates/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatrimonialState matrimonialState = await db.MatrimonialStates.FindAsync(id);
            if (matrimonialState == null)
            {
                return HttpNotFound();
            }
            return View(matrimonialState);
        }

        // POST: MatrimonialStates/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MatrimonialState matrimonialState)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matrimonialState).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(matrimonialState);
        }

        // GET: MatrimonialStates/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatrimonialState matrimonialState = await db.MatrimonialStates.FindAsync(id);
            if (matrimonialState == null)
            {
                return HttpNotFound();
            }
            return View(matrimonialState);
        }

        // POST: MatrimonialStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MatrimonialState matrimonialState = await db.MatrimonialStates.FindAsync(id);
            db.MatrimonialStates.Remove(matrimonialState);
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
