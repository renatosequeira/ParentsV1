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
    [Authorize]
    public class ExamsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Exams
        public async Task<ActionResult> Index()
        {
            var exams = db.Exams.Include(e => e.Discipline).Include(e => e.ExamFamily).Include(e => e.School);
            return View(await exams.ToListAsync());
        }

        // GET: Exams/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = await db.Exams.FindAsync(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // GET: Exams/Create
        public ActionResult Create()
        {
            ViewBag.DisciplineId = new SelectList(db.Disciplines, "DisciplineId", "DisciplineDescription");
            ViewBag.ExamFamilyId = new SelectList(db.ExamFamilies, "ExamFamilyId", "ExamFamilyDescription");
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "SchoolName");
            return View();
        }

        // POST: Exams/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ExamId,ExamDate,DisciplineId,ExamFamilyId,SchoolId,ExamFinalNote")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Exams.Add(exam);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DisciplineId = new SelectList(db.Disciplines, "DisciplineId", "DisciplineDescription", exam.DisciplineId);
            ViewBag.ExamFamilyId = new SelectList(db.ExamFamilies, "ExamFamilyId", "ExamFamilyDescription", exam.ExamFamilyId);
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "SchoolName", exam.SchoolId);
            return View(exam);
        }

        // GET: Exams/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = await db.Exams.FindAsync(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            ViewBag.DisciplineId = new SelectList(db.Disciplines, "DisciplineId", "DisciplineDescription", exam.DisciplineId);
            ViewBag.ExamFamilyId = new SelectList(db.ExamFamilies, "ExamFamilyId", "ExamFamilyDescription", exam.ExamFamilyId);
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "SchoolName", exam.SchoolId);
            return View(exam);
        }

        // POST: Exams/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ExamId,ExamDate,DisciplineId,ExamFamilyId,SchoolId,ExamFinalNote")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exam).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DisciplineId = new SelectList(db.Disciplines, "DisciplineId", "DisciplineDescription", exam.DisciplineId);
            ViewBag.ExamFamilyId = new SelectList(db.ExamFamilies, "ExamFamilyId", "ExamFamilyDescription", exam.ExamFamilyId);
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "SchoolName", exam.SchoolId);
            return View(exam);
        }

        // GET: Exams/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = await db.Exams.FindAsync(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Exam exam = await db.Exams.FindAsync(id);
            db.Exams.Remove(exam);
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
