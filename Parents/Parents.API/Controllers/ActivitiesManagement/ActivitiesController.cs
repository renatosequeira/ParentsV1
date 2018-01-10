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
    using Domain.ActivitiesManagement;

    [Authorize]
    public class ActivitiesController : ApiController
    {
        private DataContext db = new DataContext();

        #region FullActivitiesList
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
                    EventId = activity.EventId,
                    EventSeries = activity.EventSeries
                });

            }

            return Ok(activityResponse);
        }
        #endregion


        // GET: api/Activities
        [Route("api/AnniversaryActivities")]
        public async Task<IHttpActionResult> GetAnniversaryActivities()
        {
            var userId = User.Identity.GetUserId();

            var activities = await db.Activities.Where(act => act.ChildrenActivityType == "ANNIVERSARY" && (act.userId == userId || act.invitedUserId == userId)).ToListAsync();

            //var activities = await db.Activities.Where(act => act.ChildrenActivityType == "ANNIVERSARY").ToListAsync();

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
                    EventId = activity.EventId,
                    EventSeries = activity.EventSeries
                });

            }

            return Ok(activityResponse);
        }

        #region ActivitiesListOfSpecificChildren
        // GET: api/ActivitiesFromChildren/5
        [HttpGet]
        //[Route("api/{ActivitiesForCurrentChildren}/{id}/")]
        [Route("api/ActivitiesForCurrentChildren/{id}")]
        public async Task<IHttpActionResult> GetActivityFromChildren(int id)
        {
            var userId = User.Identity.GetUserId();

            var activities = await db.Activities.Where(
                act => act.userId == userId && act.ChildrenId == id ||
                act.invitedUserId == userId && act.ChildrenId == id).ToListAsync();

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
                    EventId = activity.EventId,
                    EventSeries = activity.EventSeries
                });

            }

            return Ok(activityResponse);
        }
        #endregion

        #region OnGoingActivitiesListOfSpecificChildren
        // GET: api/ActivitiesFromChildren/5
        [HttpGet]
        //[Route("api/{ActivitiesForCurrentChildren}/{id}/{status}")]
        [Route("api/ActivitiesForCurrentChildren/{id}/{status}")]
        public async Task<IHttpActionResult> GetOnGoingActivityFromChildren(int id, bool status)
        {
            var userId = User.Identity.GetUserId();

            var activities = await db.Activities.Where(
                act => act.userId == userId && act.ChildrenId == id && act.Status == status||
                act.invitedUserId == userId && act.ChildrenId == id && act.Status == status).ToListAsync();

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
                    EventId = activity.EventId,
                    EventSeries = activity.EventSeries
                });

            }

            return Ok(activityResponse);
        }
        #endregion

        #region FilteredActivityTypesListOfSpecificChildren
        // GET: api/ActivitiesFromChildren/5
        [HttpGet]
        //[Route("api/{ActivitiesForCurrentChildren}/{id}/{status}")]
        [Route("api/ActivitiesForCurrentChildren/{id}/{status}/{type}")]
        public async Task<IHttpActionResult> GetActivityTypesFromChildren(int id, bool status, string type)
        {
            var userId = User.Identity.GetUserId();

            var activities = await db.Activities.Where(
                act => act.userId == userId && act.ChildrenId == id && act.Status == status && act.ChildrenActivityType == type||
                act.invitedUserId == userId && act.ChildrenId == id && act.Status == status && act.ChildrenActivityType == type).ToListAsync();

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
                    EventId = activity.EventId,
                    EventSeries = activity.EventSeries
                });

            }

            return Ok(activityResponse);
        }
        #endregion

        #region SpecificActivity
        // GET: api/Activities/5
        [ResponseType(typeof(Domain.ActivitiesManagement.Activities))]
        public async Task<IHttpActionResult> GetActivity(int id)
        {
            Domain.ActivitiesManagement.Activities activity = await db.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        } 
        #endregion

        // PUT: api/Activities/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutActivity(int id, ActivityRequest activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activity.ActivityId)
            {
                return BadRequest();
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
            db.Entry(_activity).State = EntityState.Modified;

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
        [ResponseType(typeof(Activities))]
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

        private Activities ToActivity(ActivityRequest view)
        {
            string userId = User.Identity.GetUserId();
            var eventId = Guid.NewGuid().ToString();

            return new Domain.ActivitiesManagement.Activities
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
                EventId = eventId,
                EventSeries = DateTime.Now.TimeOfDay.ToString()
            };
        }

        // DELETE: api/Activities/5
        [ResponseType(typeof(Activities))]
        public async Task<IHttpActionResult> DeleteActivity(int id)
        {
            Domain.ActivitiesManagement.Activities activity = await db.Activities.FindAsync(id);
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