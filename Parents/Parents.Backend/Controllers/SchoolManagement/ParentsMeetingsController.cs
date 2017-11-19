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
using Parents.Domain.SchoolManagement;
using Parents.Backend.Models;

namespace Parents.Backend.Controllers.SchoolManagement
{
    public class ParentsMeetingsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ParentsMeetings
        public async Task<ActionResult> Index()
        {
            var parentsMeetings = db.ParentsMeetings.Include(p => p.Parent);
            return View(await parentsMeetings.ToListAsync());
        }

        // GET: ParentsMeetings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentsMeeting parentsMeeting = await db.ParentsMeetings.FindAsync(id);
            if (parentsMeeting == null)
            {
                return HttpNotFound();
            }
            return View(parentsMeeting);
        }

        // GET: ParentsMeetings/Create
        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName");
            return View();
        }

        // POST: ParentsMeetings/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ParentsMeetingId,ParentsMeetingDate,ParentId,ParentsMeetingRemarks")] ParentsMeeting parentsMeeting)
        {
            if (ModelState.IsValid)
            {
                db.ParentsMeetings.Add(parentsMeeting);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", parentsMeeting.ParentId);
            return View(parentsMeeting);
        }

        // GET: ParentsMeetings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentsMeeting parentsMeeting = await db.ParentsMeetings.FindAsync(id);
            if (parentsMeeting == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", parentsMeeting.ParentId);
            return View(parentsMeeting);
        }

        // POST: ParentsMeetings/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ParentsMeetingId,ParentsMeetingDate,ParentId,ParentsMeetingRemarks")] ParentsMeeting parentsMeeting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parentsMeeting).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", parentsMeeting.ParentId);
            return View(parentsMeeting);
        }

        // GET: ParentsMeetings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentsMeeting parentsMeeting = await db.ParentsMeetings.FindAsync(id);
            if (parentsMeeting == null)
            {
                return HttpNotFound();
            }
            return View(parentsMeeting);
        }

        // POST: ParentsMeetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ParentsMeeting parentsMeeting = await db.ParentsMeetings.FindAsync(id);
            db.ParentsMeetings.Remove(parentsMeeting);
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
