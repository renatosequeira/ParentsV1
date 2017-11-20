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
using Parents.Domain.SchoolManagement.Helpers;
using Parents.Backend.Models;

namespace Parents.Backend.Controllers.SchoolManagement.Helpers
{
    [Authorize]
    public class ExamFamiliesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ExamFamilies
        public async Task<ActionResult> Index()
        {
            return View(await db.ExamFamilies.ToListAsync());
        }

        // GET: ExamFamilies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamFamily examFamily = await db.ExamFamilies.FindAsync(id);
            if (examFamily == null)
            {
                return HttpNotFound();
            }
            return View(examFamily);
        }

        // GET: ExamFamilies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExamFamilies/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ExamFamily examFamily)
        {
            if (ModelState.IsValid)
            {
                db.ExamFamilies.Add(examFamily);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(examFamily);
        }

        // GET: ExamFamilies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamFamily examFamily = await db.ExamFamilies.FindAsync(id);
            if (examFamily == null)
            {
                return HttpNotFound();
            }
            return View(examFamily);
        }

        // POST: ExamFamilies/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ExamFamily examFamily)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examFamily).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(examFamily);
        }

        // GET: ExamFamilies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamFamily examFamily = await db.ExamFamilies.FindAsync(id);
            if (examFamily == null)
            {
                return HttpNotFound();
            }
            return View(examFamily);
        }

        // POST: ExamFamilies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ExamFamily examFamily = await db.ExamFamilies.FindAsync(id);
            db.ExamFamilies.Remove(examFamily);
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
