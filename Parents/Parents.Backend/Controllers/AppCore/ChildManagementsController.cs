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
using Parents.Domain.AppCore;
using Parents.Backend.Models;

namespace Parents.Backend.Controllers.AppCore
{
    [Authorize]
    public class ChildManagementsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ChildManagements
        public async Task<ActionResult> Index()
        {
            var childManagements = db.ChildManagements.Include(c => c.Children).Include(c => c.ChildSupportLastVisit).Include(c => c.ChildSupportVisitType).Include(c => c.Parent).Include(c => c.ParentalGuardTerm).Include(c => c.ParentalType).Include(c => c.ParentsMatrimonialState);
            return View(await childManagements.ToListAsync());
        }

        // GET: ChildManagements/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildManagement childManagement = await db.ChildManagements.FindAsync(id);
            if (childManagement == null)
            {
                return HttpNotFound();
            }
            return View(childManagement);
        }

        // GET: ChildManagements/Create
        public ActionResult Create()
        {
            ViewBag.ChildrenId = new SelectList(db.Children, "ChildrenId", "ChildrenFirstName");
            ViewBag.ChildSupportVisitId = new SelectList(db.ChildSupportVisits, "ChildSupportVisitId", "ChildSupportVisitId");
            ViewBag.ChildSupportVisitTypeId = new SelectList(db.ChildSupportVisitTypes, "ChildSupportVisitTypeId", "ChildSupportVisitTypeDescription");
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName");
            ViewBag.ParentalGuardTermId = new SelectList(db.ParentalGuardTerms, "ParentalGuardTermId", "ParentalGuardTermDescription");
            ViewBag.ParentalTypeId = new SelectList(db.ParentalTypes, "ParentalTypeId", "ParentalTypeDescription");
            ViewBag.MatrimonialStateId = new SelectList(db.MatrimonialStates, "MatrimonialStateId", "MatrimonialStateDescription");
            return View();
        }

        // POST: ChildManagements/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ChildManagementId,ChildrenId,ParentId,ParentalTypeId,ChildSupportVisitTypeId,ChildSupportVisitId,ChildSupportAgreedValue,MatrimonialStateId,ParentalGuardTermId")] ChildManagement childManagement)
        {
            if (ModelState.IsValid)
            {
                db.ChildManagements.Add(childManagement);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ChildrenId = new SelectList(db.Children, "ChildrenId", "ChildrenFirstName", childManagement.ChildrenId);
            ViewBag.ChildSupportVisitId = new SelectList(db.ChildSupportVisits, "ChildSupportVisitId", "ChildSupportVisitId", childManagement.ChildSupportVisitId);
            ViewBag.ChildSupportVisitTypeId = new SelectList(db.ChildSupportVisitTypes, "ChildSupportVisitTypeId", "ChildSupportVisitTypeDescription", childManagement.ChildSupportVisitTypeId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", childManagement.ParentId);
            ViewBag.ParentalGuardTermId = new SelectList(db.ParentalGuardTerms, "ParentalGuardTermId", "ParentalGuardTermDescription", childManagement.ParentalGuardTermId);
            ViewBag.ParentalTypeId = new SelectList(db.ParentalTypes, "ParentalTypeId", "ParentalTypeDescription", childManagement.ParentalTypeId);
            ViewBag.MatrimonialStateId = new SelectList(db.MatrimonialStates, "MatrimonialStateId", "MatrimonialStateDescription", childManagement.MatrimonialStateId);
            return View(childManagement);
        }

        // GET: ChildManagements/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildManagement childManagement = await db.ChildManagements.FindAsync(id);
            if (childManagement == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChildrenId = new SelectList(db.Children, "ChildrenId", "ChildrenFirstName", childManagement.ChildrenId);
            ViewBag.ChildSupportVisitId = new SelectList(db.ChildSupportVisits, "ChildSupportVisitId", "ChildSupportVisitId", childManagement.ChildSupportVisitId);
            ViewBag.ChildSupportVisitTypeId = new SelectList(db.ChildSupportVisitTypes, "ChildSupportVisitTypeId", "ChildSupportVisitTypeDescription", childManagement.ChildSupportVisitTypeId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", childManagement.ParentId);
            ViewBag.ParentalGuardTermId = new SelectList(db.ParentalGuardTerms, "ParentalGuardTermId", "ParentalGuardTermDescription", childManagement.ParentalGuardTermId);
            ViewBag.ParentalTypeId = new SelectList(db.ParentalTypes, "ParentalTypeId", "ParentalTypeDescription", childManagement.ParentalTypeId);
            ViewBag.MatrimonialStateId = new SelectList(db.MatrimonialStates, "MatrimonialStateId", "MatrimonialStateDescription", childManagement.MatrimonialStateId);
            return View(childManagement);
        }

        // POST: ChildManagements/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ChildManagementId,ChildrenId,ParentId,ParentalTypeId,ChildSupportVisitTypeId,ChildSupportVisitId,ChildSupportAgreedValue,MatrimonialStateId,ParentalGuardTermId")] ChildManagement childManagement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(childManagement).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ChildrenId = new SelectList(db.Children, "ChildrenId", "ChildrenFirstName", childManagement.ChildrenId);
            ViewBag.ChildSupportVisitId = new SelectList(db.ChildSupportVisits, "ChildSupportVisitId", "ChildSupportVisitId", childManagement.ChildSupportVisitId);
            ViewBag.ChildSupportVisitTypeId = new SelectList(db.ChildSupportVisitTypes, "ChildSupportVisitTypeId", "ChildSupportVisitTypeDescription", childManagement.ChildSupportVisitTypeId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", childManagement.ParentId);
            ViewBag.ParentalGuardTermId = new SelectList(db.ParentalGuardTerms, "ParentalGuardTermId", "ParentalGuardTermDescription", childManagement.ParentalGuardTermId);
            ViewBag.ParentalTypeId = new SelectList(db.ParentalTypes, "ParentalTypeId", "ParentalTypeDescription", childManagement.ParentalTypeId);
            ViewBag.MatrimonialStateId = new SelectList(db.MatrimonialStates, "MatrimonialStateId", "MatrimonialStateDescription", childManagement.MatrimonialStateId);
            return View(childManagement);
        }

        // GET: ChildManagements/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildManagement childManagement = await db.ChildManagements.FindAsync(id);
            if (childManagement == null)
            {
                return HttpNotFound();
            }
            return View(childManagement);
        }

        // POST: ChildManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ChildManagement childManagement = await db.ChildManagements.FindAsync(id);
            db.ChildManagements.Remove(childManagement);
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
