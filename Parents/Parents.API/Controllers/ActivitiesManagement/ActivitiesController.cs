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
    using Domain.ActivitiesManagement;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using API.Models.ActivitiesManagement;
    using System;
    using System.Diagnostics;

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
                    ActivityStartTime = activity.ActivityStartTime,
                    ActivityEndTime = activity.ActivityEndTime,
                    ActivityRecurring = activity.ActivityRecurring
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
        public async Task<IHttpActionResult> PostActivity(Domain.ActivitiesManagement.Activity activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

            db.Activities.Add(activity);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = activity.ActivityId }, activity);
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