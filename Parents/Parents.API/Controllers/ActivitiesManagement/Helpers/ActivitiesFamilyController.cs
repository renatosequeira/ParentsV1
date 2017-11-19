using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Parents.Domain;
using Parents.Domain.ActivitiesManagement.Helpers;

namespace Parents.API.Controllers.ActivitiesManagement.Helpers
{
    [Authorize]
    public class ActivitiesFamilyController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ActivitiesFamily
        public IQueryable<ActivityFamily> GetActivityFamilies()
        {
            return db.ActivityFamilies;
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