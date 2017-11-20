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
    public class ChildSupportPaymentsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ChildSupportPayments
        public async Task<ActionResult> Index()
        {
            return View(await db.ChildSupportPayments.ToListAsync());
        }

        // GET: ChildSupportPayments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildSupportPayment childSupportPayment = await db.ChildSupportPayments.FindAsync(id);
            if (childSupportPayment == null)
            {
                return HttpNotFound();
            }
            return View(childSupportPayment);
        }

        // GET: ChildSupportPayments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChildSupportPayments/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ChildSupportPayment childSupportPayment)
        {
            if (ModelState.IsValid)
            {
                db.ChildSupportPayments.Add(childSupportPayment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(childSupportPayment);
        }

        // GET: ChildSupportPayments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildSupportPayment childSupportPayment = await db.ChildSupportPayments.FindAsync(id);
            if (childSupportPayment == null)
            {
                return HttpNotFound();
            }
            return View(childSupportPayment);
        }

        // POST: ChildSupportPayments/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ChildSupportPayment childSupportPayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(childSupportPayment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(childSupportPayment);
        }

        // GET: ChildSupportPayments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildSupportPayment childSupportPayment = await db.ChildSupportPayments.FindAsync(id);
            if (childSupportPayment == null)
            {
                return HttpNotFound();
            }
            return View(childSupportPayment);
        }

        // POST: ChildSupportPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ChildSupportPayment childSupportPayment = await db.ChildSupportPayments.FindAsync(id);
            db.ChildSupportPayments.Remove(childSupportPayment);
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
