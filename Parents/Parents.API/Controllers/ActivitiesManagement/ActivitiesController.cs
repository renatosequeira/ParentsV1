namespace Parents.API.Controllers.ActivitiesManagement
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Domain;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using API.Models.ActivitiesManagement;
    using System;
    using System.Diagnostics;
    using System.IO;
    using Parents.API.Helpers;

    [Authorize]
    public class ActivitiesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Activities
        public async Task<IHttpActionResult> GetActivities()
        {
            var userId = User.Identity.GetUserId();
            
            //var activities = await db.Activities.Where((act => act.userId == userId && act.relatedChildrenIdentitiCard == "10788194" || act.invitedUserId == userId && act.relatedChildrenIdentitiCard == "10788194")).ToListAsync();
            var activities = await db.Activities.Where(act => act.userId == userId || act.invitedUserId == userId).ToListAsync();

            var activityResponse = new List<ActivityResponse>();

            foreach (var activity in activities)
            {

                activityResponse.Add(new ActivityResponse
                {
                    ActivityAddress = activity.ActivityAddress,
                    ActivityDateEnd = activity.ActivityDateEnd,
                    ActivityDateStart = activity.ActivityDateStart,
                    ActivityDescription = activity.ActivityDescription,
                    ActivityId = activity.ActivityId,
                    ActivityPrivacy = activity.ActivityPrivacy,
                    ActivityRemarks = activity.ActivityRemarks,
                    Image = activity.Image,
                    invitationAcknowledged = activity.invitationAcknowledged,
                    invitedUserId = activity.invitedUserId,
                    relatedChildrenIdentitiCard = activity.relatedChildrenIdentitiCard,
                    userId = activity.userId,
                    ChildrenId = activity.ChildrenId,
                    Status = activity.Status,
                    ChildrenActivityFamily = activity.ChildrenActivityFamily,
                    ChildrenActivityType = activity.ChildrenActivityType,
                    ActivityPriority = activity.ActivityPriority,
                    ActivityTimeEnd = activity.ActivityTimeEnd,
                    ActivityTimeStart = activity.ActivityTimeStart,
                    ActivityRepeat = activity.ActivityRepeat,
                    EventId = activity.EventId
                });

            }

            return Ok(activityResponse);
        }

        // GET: api/Activities/5
        [ResponseType(typeof(Domain.ActivitiesManagement.Activity))]
        public async Task<IHttpActionResult> GetActivity(int id)
        {
            Domain.ActivitiesManagement.Activity activity = await db.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        // PUT: api/Activities/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutActivity(int id, Domain.ActivitiesManagement.Activity activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activity.ActivityId)
            {
                return BadRequest();
            }

            db.Entry(activity).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Activities
        [ResponseType(typeof(Domain.ActivitiesManagement.Activity))]
        public async Task<IHttpActionResult> PostActivity(ActivityRequest activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (activity.ImageArray != null && activity.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(activity.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                var folder = "~/Content/Images";
                var fullPath = string.Format("{0}/{1}", folder, file);
                var response = FilesHelper.UploadPhoto(stream, folder, file);


                if (response)
                {
                    activity.Image = fullPath;
                }

            }

            var _activity = ToActivity(activity);

            bool privateActivity = activity.ActivityPrivacy;
            string userId = User.Identity.GetUserId();
            string invitedUserId = activity.invitedUserId;

            activity.userId = userId;

            try
            {
                activity.invitedUserId = invitedUserId;
            }
            catch (Exception ex)
            {
                if (invitedUserId != null)
                    activity.invitedUserId = null;

                Debug.WriteLine(ex.Message);
            }

            db.Activities.Add(_activity);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = activity.ActivityId }, _activity);
        }

        private Domain.ActivitiesManagement.Activity ToActivity(ActivityRequest view)
        {
            string userId = User.Identity.GetUserId();
            var eventId = Guid.NewGuid().ToString();

            return new Domain.ActivitiesManagement.Activity
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
                userId = userId,
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
                EventId = eventId
            };
        }

        // DELETE: api/Activities/5
        [ResponseType(typeof(Domain.ActivitiesManagement.Activity))]
        public async Task<IHttpActionResult> DeleteActivity(int id)
        {
            Domain.ActivitiesManagement.Activity activity = await db.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            db.Activities.Remove(activity);
            await db.SaveChangesAsync();

            return Ok(activity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActivityExists(int id)
        {
            return db.Activities.Count(e => e.ActivityId == id) > 0;
        }
    }
}