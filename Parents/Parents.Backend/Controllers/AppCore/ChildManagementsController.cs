using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Parents.Domain.AppCore;
using Parents.Backend.Models;
using Parents.Backend.Models.AppCore;
using Parents.Backend.Helpers;
using System;

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
            ViewBag.ManagementTypreId = new SelectList(db.ManagementTypes, "ManagementTypreId", "ManagementTypeDescription");
            return View();
        }

        // POST: ChildManagements/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ChildManagementView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var childManagement = ToChildManagement(view);
                childManagement.Image = pic;

                db.ChildManagements.Add(childManagement);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ChildrenId = new SelectList(db.Children, "ChildrenId", "ChildrenFirstName", view.ChildrenId);
            ViewBag.ChildSupportVisitId = new SelectList(db.ChildSupportVisits, "ChildSupportVisitId", "ChildSupportVisitId", view.ChildSupportVisitId);
            ViewBag.ChildSupportVisitTypeId = new SelectList(db.ChildSupportVisitTypes, "ChildSupportVisitTypeId", "ChildSupportVisitTypeDescription", view.ChildSupportVisitTypeId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", view.ParentId);
            ViewBag.ParentalGuardTermId = new SelectList(db.ParentalGuardTerms, "ParentalGuardTermId", "ParentalGuardTermDescription", view.ParentalGuardTermId);
            ViewBag.ParentalTypeId = new SelectList(db.ParentalTypes, "ParentalTypeId", "ParentalTypeDescription", view.ParentalTypeId);
            ViewBag.MatrimonialStateId = new SelectList(db.MatrimonialStates, "MatrimonialStateId", "MatrimonialStateDescription", view.MatrimonialStateId);
            ViewBag.ManagementTypreId = new SelectList(db.ManagementTypes, "ManagementTypreId", "ManagementTypeDescription", view.ManagementTypreId);
            return View(view);
        }

        private ChildManagement ToChildManagement(ChildManagementView view)
        {
            return new ChildManagement
            {
                ChildManagementId = view.ChildManagementId,
                Children = view.Children,
                ChildrenId = view.ChildrenId,
                ChildSupportAgreedValue = view.ChildSupportAgreedValue,
                ChildSupportLastVisit = view.ChildSupportLastVisit,
                ChildSupportVisitId = view.ChildSupportVisitId,
                ChildSupportVisitType = view.ChildSupportVisitType,
                ChildSupportVisitTypeId = view.ChildSupportVisitTypeId,
                MatrimonialStateId = view.MatrimonialStateId,
                Parent = view.Parent,
                ManagementType = view.ManagementType,
                ManagementTypreId = view.ManagementTypreId,
                ParentalGuardTerm = view.ParentalGuardTerm,
                ParentalGuardTermId = view.ParentalGuardTermId,
                ParentalType = view.ParentalType,
                ParentalTypeId = view.ParentalTypeId,
                ParentId = view.ParentId,
                ParentsMatrimonialState = view.ParentsMatrimonialState
            };
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
            ViewBag.ManagementTypreId = new SelectList(db.ManagementTypes, "ManagementTypreId", "ManagementTypeDescription");
            var view = ToView(childManagement);

            return View(view);
        }

        private ChildManagementView ToView(ChildManagement childManagement)
        {
            return new ChildManagementView
            {
                ChildManagementId = childManagement.ChildManagementId,
                Children = childManagement.Children,
                ChildrenId = childManagement.ChildrenId,
                ChildSupportAgreedValue = childManagement.ChildSupportAgreedValue,
                ChildSupportLastVisit = childManagement.ChildSupportLastVisit,
                ChildSupportVisitId = childManagement.ChildSupportVisitId,
                ChildSupportVisitType = childManagement.ChildSupportVisitType,
                ChildSupportVisitTypeId = childManagement.ChildSupportVisitTypeId,
                MatrimonialStateId = childManagement.MatrimonialStateId,
                Parent = childManagement.Parent,
                ManagementType = childManagement.ManagementType,
                ManagementTypreId = childManagement.ManagementTypreId,
                ParentalGuardTerm = childManagement.ParentalGuardTerm,
                ParentalGuardTermId = childManagement.ParentalGuardTermId,
                ParentalType = childManagement.ParentalType,
                ParentalTypeId = childManagement.ParentalTypeId,
                ParentId = childManagement.ParentId,
                ParentsMatrimonialState = childManagement.ParentsMatrimonialState
            };
        }

        // POST: ChildManagements/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ChildManagementView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.Image;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var childManagement = ToChildManagement(view);
                childManagement.Image = pic;

                db.Entry(childManagement).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ChildrenId = new SelectList(db.Children, "ChildrenId", "ChildrenFirstName", view.ChildrenId);
            ViewBag.ChildSupportVisitId = new SelectList(db.ChildSupportVisits, "ChildSupportVisitId", "ChildSupportVisitId", view.ChildSupportVisitId);
            ViewBag.ChildSupportVisitTypeId = new SelectList(db.ChildSupportVisitTypes, "ChildSupportVisitTypeId", "ChildSupportVisitTypeDescription", view.ChildSupportVisitTypeId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", view.ParentId);
            ViewBag.ParentalGuardTermId = new SelectList(db.ParentalGuardTerms, "ParentalGuardTermId", "ParentalGuardTermDescription", view.ParentalGuardTermId);
            ViewBag.ParentalTypeId = new SelectList(db.ParentalTypes, "ParentalTypeId", "ParentalTypeDescription", view.ParentalTypeId);
            ViewBag.MatrimonialStateId = new SelectList(db.MatrimonialStates, "MatrimonialStateId", "MatrimonialStateDescription", view.MatrimonialStateId);
            ViewBag.ManagementTypreId = new SelectList(db.ManagementTypes, "ManagementTypreId", "ManagementTypeDescription");
            return View(view);
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
