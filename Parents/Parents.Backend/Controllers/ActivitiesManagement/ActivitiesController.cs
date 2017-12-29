namespace Parents.Backend.Controllers.ActivitiesManagement
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using Parents.Domain.ActivitiesManagement;
    using Backend.Models;
    using Backend.Models.ActivitiesManagement;
    using Backend.Helpers;
    using Microsoft.AspNet.Identity;
    using System;

    [Authorize]
    public class ActivitiesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Activities
        public async Task<ActionResult> Index()
        {
            var activities = db.Activities.Include(a => a.ActivityFamily).Include(a => a.ActivityInstitutionType).Include(a => a.ActivityPeriodicity).Include(a => a.ActivityType).Include(a => a.ParentInCharge);
            return View(await activities.ToListAsync());
        }

        // GET: Activities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = await db.Activities.FindAsync(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            ViewBag.ChildrenId = new SelectList(db.Children, "ChildrenId", "ChildrenFirstName");
            ViewBag.ActivityFamilyId = new SelectList(db.ActivityFamilies, "ActivityFamilyId", "ActivityFamilyDescription");
            ViewBag.ActivityInstitutionTypeId = new SelectList(db.ActivityInstitutionTypes, "ActivityInstitutionTypeId", "ActivityInstitutionTypeDescription");
            ViewBag.ActivityPeriodicityId = new SelectList(db.ActivityPeriodicities, "ActivityPeriodicityId", "ActivityPeriodicityDescription");
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "ActivityTypeId", "ActivityTypeDescription");
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName");
            return View();
        }

        // POST: Activities/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ActivitiesView view)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                view.userId = userId;
                view.invitedUserId = null;

                var guid = Guid.NewGuid().ToString();
                view.EventId = guid;

                view.EventSeries = DateTime.Now.TimeOfDay.ToString();

                var pic = string.Empty;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var activity = ToActivity(view);
                activity.Image = pic;

                db.Activities.Add(activity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ChildrenId = new SelectList(db.Children, "ChildrenIdentityCard", "ChildrenIdentityCard");
            ViewBag.ActivityFamilyId = new SelectList(db.ActivityFamilies, "ActivityFamilyId", "ActivityFamilyDescription", view.ActivityFamilyId);
            ViewBag.ActivityInstitutionTypeId = new SelectList(db.ActivityInstitutionTypes, "ActivityInstitutionTypeId", "ActivityInstitutionTypeDescription", view.ActivityInstitutionTypeId);
            ViewBag.ActivityPeriodicityId = new SelectList(db.ActivityPeriodicities, "ActivityPeriodicityId", "ActivityPeriodicityDescription", view.ActivityPeriodicityId);
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "ActivityTypeId", "ActivityTypeDescription", view.ActivityTypeId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", view.ParentId);
            return View(view);
        }

        private Activity ToActivity(ActivitiesView view)
        {
            return new Activity
            {
                ActivityAddress = view.ActivityAddress,
                ActivityDateEnd = view.ActivityDateEnd,
                ActivityDateStart = view.ActivityDateStart,
                ActivityDescription = view.ActivityDescription,
                ActivityFamily = view.ActivityFamily,
                ActivityFamilyId = view.ActivityFamilyId,
                ActivityId = view.ActivityId,
                ActivityInstitutionType = view.ActivityInstitutionType,
                ActivityInstitutionTypeId = view.ActivityInstitutionTypeId,
                ActivityPeriodicity = view.ActivityPeriodicity,
                ActivityPeriodicityId = view.ActivityPeriodicityId,
                ActivityRemarks = view.ActivityRemarks,
                ActivityType = view.ActivityType,
                ActivityTypeId = view.ActivityTypeId,
                ParentId = view.ParentId,
                ParentInCharge = view.ParentInCharge,
                ActivityPrivacy = view.ActivityPrivacy,
                userId = view.userId,
                relatedChildrenIdentitiCard = view.relatedChildrenIdentitiCard,
                Children = view.Children,
                ChildrenId = view.ChildrenId,
                Image = view.Image,
                invitationAcknowledged = view.invitationAcknowledged,
                invitedUserId = view.invitedUserId,
                ChildrenActivityFamily = view.ChildrenActivityFamily,
                ChildrenActivityType = view.ChildrenActivityType,
                Status = view.Status,
                ActivityPriority = view.ActivityPriority,
                ActivityTimeStart = view.ActivityTimeStart,
                ActivityTimeEnd = view.ActivityTimeEnd,
                ActivityRepeat = view.ActivityRepeat,
                EventId = view.EventId,
                EventSeries = view.EventSeries
            };
        }

        // GET: Activities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = await db.Activities.FindAsync(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityFamilyId = new SelectList(db.ActivityFamilies, "ActivityFamilyId", "ActivityFamilyDescription", activity.ActivityFamilyId);
            ViewBag.ActivityInstitutionTypeId = new SelectList(db.ActivityInstitutionTypes, "ActivityInstitutionTypeId", "ActivityInstitutionTypeDescription", activity.ActivityInstitutionTypeId);
            ViewBag.ActivityPeriodicityId = new SelectList(db.ActivityPeriodicities, "ActivityPeriodicityId", "ActivityPeriodicityDescription", activity.ActivityPeriodicityId);
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "ActivityTypeId", "ActivityTypeDescription", activity.ActivityTypeId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", activity.ParentId);

            var view = ToView(activity);

            return View(view);
        }

        private ActivitiesView ToView(Activity activity)
        {
            return new ActivitiesView
            {
                ActivityAddress = activity.ActivityAddress,
                ActivityDateEnd = activity.ActivityDateEnd,
                ActivityDateStart = activity.ActivityDateStart,
                ActivityDescription = activity.ActivityDescription,
                ActivityFamily = activity.ActivityFamily,
                ActivityFamilyId = activity.ActivityFamilyId,
                ActivityId = activity.ActivityId,
                ActivityInstitutionType = activity.ActivityInstitutionType,
                ActivityInstitutionTypeId = activity.ActivityInstitutionTypeId,
                ActivityPeriodicity = activity.ActivityPeriodicity,
                ActivityPeriodicityId = activity.ActivityPeriodicityId,
                ActivityRemarks = activity.ActivityRemarks,
                ActivityType = activity.ActivityType,
                ActivityTypeId = activity.ActivityTypeId,
                ParentId = activity.ParentId,
                ParentInCharge = activity.ParentInCharge,
                ActivityPrivacy = activity.ActivityPrivacy,
                relatedChildrenIdentitiCard = activity.relatedChildrenIdentitiCard,
                userId = activity.userId,
                Children = activity.Children,
                ChildrenActivityFamily = activity.ChildrenActivityFamily,
                ChildrenActivityType = activity.ChildrenActivityType,
                ChildrenId = activity.ChildrenId,
                Image = activity.Image,
                invitationAcknowledged = activity.invitationAcknowledged,
                invitedUserId = activity.invitedUserId,
                Status = activity.Status                
            };
        }

        // POST: Activities/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ActivitiesView view)
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

                var activity = ToActivity(view);
                activity.Image = pic;

                db.Entry(activity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityFamilyId = new SelectList(db.ActivityFamilies, "ActivityFamilyId", "ActivityFamilyDescription", view.ActivityFamilyId);
            ViewBag.ActivityInstitutionTypeId = new SelectList(db.ActivityInstitutionTypes, "ActivityInstitutionTypeId", "ActivityInstitutionTypeDescription", view.ActivityInstitutionTypeId);
            ViewBag.ActivityPeriodicityId = new SelectList(db.ActivityPeriodicities, "ActivityPeriodicityId", "ActivityPeriodicityDescription", view.ActivityPeriodicityId);
            ViewBag.ActivityTypeId = new SelectList(db.ActivityTypes, "ActivityTypeId", "ActivityTypeDescription", view.ActivityTypeId);
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentFirstName", view.ParentId);
            return View(view);
        }

        // GET: Activities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = await db.Activities.FindAsync(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Activity activity = await db.Activities.FindAsync(id);
            db.Activities.Remove(activity);
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
