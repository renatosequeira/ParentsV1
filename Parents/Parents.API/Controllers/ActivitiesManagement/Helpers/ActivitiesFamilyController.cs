using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Parents.Domain;
using Parents.Domain.ActivitiesManagement.Helpers;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using Parents.API.Models.ActivitiesManagement.Helpers;

namespace Parents.API.Controllers.ActivitiesManagement.Helpers
{
    [Authorize]
    public class ActivitiesFamilyController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ActivitiesFamily
        public async Task<IHttpActionResult> GetActivityFamilies()
        {
            var activityFamilies = await db.ActivityFamilies.ToListAsync();

            var activityFamiliesResponse = new List<ActivityFamilyResponse>();

            foreach (var activity in activityFamilies)
            {
                activityFamiliesResponse.Add(new ActivityFamilyResponse
                {
                    ActivityFamilyId = activity.ActivityFamilyId,
                    ActivityFamilyDescription = activity.ActivityFamilyDescription,
                    userId = activity.userId
                });
            }

            return Ok(activityFamiliesResponse);
        }

        // GET: api/ActivitiesFamily/5
        [ResponseType(typeof(ActivityFamily))]
        public async Task<IHttpActionResult> GetActivityFamily(int id)
        {
            ActivityFamily activityFamily = await db.ActivityFamilies.FindAsync(id);
            if (activityFamily == null)
            {
                return NotFound();
            }

            return Ok(activityFamily);
        }

        // PUT: api/ActivitiesFamily/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutActivityFamily(int id, ActivityFamily activityFamily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activityFamily.ActivityFamilyId)
            {
                return BadRequest();
            }

            db.Entry(activityFamily).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityFamilyExists(id))
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

        // POST: api/ActivitiesFamily
        [ResponseType(typeof(ActivityFamily))]
        public async Task<IHttpActionResult> PostActivityFamily(ActivityFamily activityFamily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = User.Identity.GetUserId();
            activityFamily.userId = userId;

            db.ActivityFamilies.Add(activityFamily);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = activityFamily.ActivityFamilyId }, activityFamily);
        }

        // DELETE: api/ActivitiesFamily/5
        [ResponseType(typeof(ActivityFamily))]
        public async Task<IHttpActionResult> DeleteActivityFamily(int id)
        {
            ActivityFamily activityFamily = await db.ActivityFamilies.FindAsync(id);
            if (activityFamily == null)
            {
                return NotFound();
            }

            db.ActivityFamilies.Remove(activityFamily);
            await db.SaveChangesAsync();

            return Ok(activityFamily);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActivityFamilyExists(int id)
        {
            return db.ActivityFamilies.Count(e => e.ActivityFamilyId == id) > 0;
        }
    }
}