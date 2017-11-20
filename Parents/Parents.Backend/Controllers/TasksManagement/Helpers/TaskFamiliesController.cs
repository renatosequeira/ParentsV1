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
using Parents.Domain.TasksManagement.Helpers;
using Parents.Backend.Models;

namespace Parents.Backend.Controllers.TasksManagement.Helpers
{
    [Authorize]
    public class TaskFamiliesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: TaskFamilies
        public async Task<ActionResult> Index()
        {
            return View(await db.TaskFamilies.ToListAsync());
        }

        // GET: TaskFamilies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskFamily taskFamily = await db.TaskFamilies.FindAsync(id);
            if (taskFamily == null)
            {
                return HttpNotFound();
            }
            return View(taskFamily);
        }

        // GET: TaskFamilies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskFamilies/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TaskFamily taskFamily)
        {
            if (ModelState.IsValid)
            {
                db.TaskFamilies.Add(taskFamily);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(taskFamily);
        }

        // GET: TaskFamilies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskFamily taskFamily = await db.TaskFamilies.FindAsync(id);
            if (taskFamily == null)
            {
                return HttpNotFound();
            }
            return View(taskFamily);
        }

        // POST: TaskFamilies/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TaskFamily taskFamily)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskFamily).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(taskFamily);
        }

        // GET: TaskFamilies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskFamily taskFamily = await db.TaskFamilies.FindAsync(id);
            if (taskFamily == null)
            {
                return HttpNotFound();
            }
            return View(taskFamily);
        }

        // POST: TaskFamilies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TaskFamily taskFamily = await db.TaskFamilies.FindAsync(id);
            db.TaskFamilies.Remove(taskFamily);
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
