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
using Parents.Domain.TasksManagement;
using Parents.Backend.Models;

namespace Parents.Backend.Controllers.TasksManagement
{
    [Authorize]
    public class TaskFunctionsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: TaskFunctions
        public async Task<ActionResult> Index()
        {
            var tasks = db.Tasks.Include(t => t.TaskFamily).Include(t => t.TaskResponsible);
            return View(await tasks.ToListAsync());
        }

        // GET: TaskFunctions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskFunction taskFunction = await db.Tasks.FindAsync(id);
            if (taskFunction == null)
            {
                return HttpNotFound();
            }
            return View(taskFunction);
        }

        // GET: TaskFunctions/Create
        public ActionResult Create()
        {
            ViewBag.TaskFamilyId = new SelectList(db.TaskFamilies, "TaskFamilyId", "TaskFamilyDescription");
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName");
            return View();
        }

        // POST: TaskFunctions/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TaskId,TaskDescription,TaskOwner,ParentId,TaskCreationDate,TaskStatus,TaskConclusionDate,TaskFamilyId,TaskRemarks")] TaskFunction taskFunction)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(taskFunction);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TaskFamilyId = new SelectList(db.TaskFamilies, "TaskFamilyId", "TaskFamilyDescription", taskFunction.TaskFamilyId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", taskFunction.ParentId);
            return View(taskFunction);
        }

        // GET: TaskFunctions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskFunction taskFunction = await db.Tasks.FindAsync(id);
            if (taskFunction == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaskFamilyId = new SelectList(db.TaskFamilies, "TaskFamilyId", "TaskFamilyDescription", taskFunction.TaskFamilyId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", taskFunction.ParentId);
            return View(taskFunction);
        }

        // POST: TaskFunctions/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TaskId,TaskDescription,TaskOwner,ParentId,TaskCreationDate,TaskStatus,TaskConclusionDate,TaskFamilyId,TaskRemarks")] TaskFunction taskFunction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskFunction).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TaskFamilyId = new SelectList(db.TaskFamilies, "TaskFamilyId", "TaskFamilyDescription", taskFunction.TaskFamilyId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", taskFunction.ParentId);
            return View(taskFunction);
        }

        // GET: TaskFunctions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskFunction taskFunction = await db.Tasks.FindAsync(id);
            if (taskFunction == null)
            {
                return HttpNotFound();
            }
            return View(taskFunction);
        }

        // POST: TaskFunctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TaskFunction taskFunction = await db.Tasks.FindAsync(id);
            db.Tasks.Remove(taskFunction);
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
