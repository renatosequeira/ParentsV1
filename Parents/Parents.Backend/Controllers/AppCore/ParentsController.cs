using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Parents.Domain;
using Parents.Backend.Models;
using Parents.Backend.Models.AppCore;
using Parents.Backend.Helpers;
using System;

namespace Parents.Backend.Controllers.AppCore
{
    [Authorize]
    public class ParentsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Parents
        public async Task<ActionResult> Index()
        {
            var parents = db.Parents.Include(p => p.ParentalType);
            return View(await parents.ToListAsync());
        }

        // GET: Parents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = await db.Parents.FindAsync(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // GET: Parents/Create
        public ActionResult Create()
        {
            ViewBag.ParentalTypeId = new SelectList(db.ParentalTypes, "ParentalTypeId", "ParentalTypeDescription");
            return View();
        }

        // POST: Parents/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ParentView view)
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

                var parent = ToParent(view);
                parent.ParentImage = pic;

                db.Parents.Add(parent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ParentalTypeId = new SelectList(db.ParentalTypes, "ParentalTypeId", "ParentalTypeDescription", view.ParentalTypeId);
            return View(view);
        }

        private Parent ToParent(ParentView view)
        {
            return new Parent
            {
                Activity = view.Activity,
                Children = view.Children,
                ParentAddress = view.ParentAddress,
                ParentalType = view.ParentalType,
                ParentalTypeId = view.ParentalTypeId,
                ParentBirthDate = view.ParentBirthDate,
                ParentEmail = view.ParentEmail,
                ParentFirstName = view.ParentFirstName,
                ParentId = view.ParentId,
                ParentIdentityCard = view.ParentIdentityCard,
                ParentImage = view.ParentImage,
                ParentLastName = view.ParentLastName,
                ParentMiddleName = view.ParentMiddleName,
                ParentMobile = view.ParentMobile,
                ParentsMeeting = view.ParentsMeeting,
                Urgency = view.Urgency
            };
        }

        // GET: Parents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = await db.Parents.FindAsync(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentalTypeId = new SelectList(db.ParentalTypes, "ParentalTypeId", "ParentalTypeDescription", parent.ParentalTypeId);

            var view = ToView(parent);

            return View(view);
        }

        private ParentView ToView(Parent parent)
        {
            return new ParentView
            {
                Activity = parent.Activity,
                Children = parent.Children,
                ParentAddress = parent.ParentAddress,
                ParentalType = parent.ParentalType,
                ParentalTypeId = parent.ParentalTypeId,
                ParentBirthDate = parent.ParentBirthDate,
                ParentEmail = parent.ParentEmail,
                ParentFirstName = parent.ParentFirstName,
                ParentId = parent.ParentId,
                ParentIdentityCard = parent.ParentIdentityCard,
                ParentImage = parent.ParentImage,
                ParentLastName = parent.ParentLastName,
                ParentMiddleName = parent.ParentMiddleName,
                ParentMobile = parent.ParentMobile,
                ParentsMeeting = parent.ParentsMeeting,
                Urgency = parent.Urgency
            };
        }

        // POST: Parents/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ParentView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.ParentImage;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var parent = ToParent(view);
                parent.ParentImage = pic;


                db.Entry(parent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ParentalTypeId = new SelectList(db.ParentalTypes, "ParentalTypeId", "ParentalTypeDescription", view.ParentalTypeId);
            return View(view);
        }

        // GET: Parents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = await db.Parents.FindAsync(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // POST: Parents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Parent parent = await db.Parents.FindAsync(id);
            db.Parents.Remove(parent);
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
