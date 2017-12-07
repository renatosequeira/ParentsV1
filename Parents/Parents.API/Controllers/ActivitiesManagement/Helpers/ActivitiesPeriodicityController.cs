
namespace Parents.API.Controllers.ActivitiesManagement.Helpers
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Parents.Domain;
    using Parents.Domain.ActivitiesManagement.Helpers;
    using Parents.API.Models.ActivitiesManagement.Helpers;
    using System.Collections.Generic;
    using System;
    using Microsoft.AspNet.Identity;

    [Authorize]
    public class ActivitiesPeriodicityController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ActivitiesPeriodicity
        public async Task<IHttpActionResult> GetActivityPeriodicities()
        {
            var activityPeridiocity = await db.ActivityPeriodicities.ToListAsync();

            var activityPeridiocityResponse = new List<ActivityPeridiocityResponse>();

            foreach (var activity in activityPeridiocity)
            {
                activityPeridiocityResponse.Add(new ActivityPeridiocityResponse
                {
                    ActivityPeriodicityDescription = activity.ActivityPeriodicityDescription,
                    ActivityPeriodicityId = activity.ActivityPeriodicityId,
                    userId = activity.userId
                });
            }

            return Ok(activityPeridiocityResponse);
        }

        // GET: api/ActivitiesPeriodicity/5
        [ResponseType(typeof(ActivityPeriodicity))]
        public async Task<IHttpActionResult> GetActivityPeriodicity(int id)
        {
            ActivityPeriodicity activityPeriodicity = await db.ActivityPeriodicities.FindAsync(id);
            if (activityPeriodicity == null)
            {
                return NotFound();
            }

            return Ok(activityPeriodicity);
        }

        // PUT: api/ActivitiesPeriodicity/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutActivityPeriodicity(int id, ActivityPeriodicity activityPeriodicity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activityPeriodicity.ActivityPeriodicityId)
            {
                return BadRequest();
            }

            db.Entry(activityPeriodicity).State = EntityState.Modified;

            string currentUser = User.Identity.GetUserId();
            string userId = activityPeriodicity.userId;

            if (currentUser == userId)
            {
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    if (ex.InnerException != null &&
                        ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("Index"))
                    {
                        return BadRequest("A Period with same description was already found. Couldn't be edited.");
                    }
                    else
                    {
                        return BadRequest(ex.Message);
                    }
                } 
            }
            else
            {
                return BadRequest("You are not the proprietary of this topic. Only owner can change it");

            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ActivitiesPeriodicity
        [ResponseType(typeof(ActivityPeriodicity))]
        public async Task<IHttpActionResult> PostActivityPeriodicity(ActivityPeriodicity activityPeriodicity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ActivityPeriodicities.Add(activityPeriodicity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                  ex.InnerException.InnerException != null &&
                  ex.InnerException.InnerException.Message.Contains("Index"))
                {
                    return BadRequest("A Period with same description was already found. Couldn't be added.");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = activityPeriodicity.ActivityPeriodicityId }, activityPeriodicity);
        }

        // DELETE: api/ActivitiesPeriodicity/5
        [ResponseType(typeof(ActivityPeriodicity))]
        public async Task<IHttpActionResult> DeleteActivityPeriodicity(int id)
        {
            ActivityPeriodicity activityPeriodicity = await db.ActivityPeriodicities.FindAsync(id);
            if (activityPeriodicity == null)
            {
                return NotFound();
            }

            db.ActivityPeriodicities.Remove(activityPeriodicity);
            await db.SaveChangesAsync();

            return Ok(activityPeriodicity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActivityPeriodicityExists(int id)
        {
            return db.ActivityPeriodicities.Count(e => e.ActivityPeriodicityId == id) > 0;
        }
    }
}