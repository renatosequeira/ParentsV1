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
    public class ActivitiesPeriodicityController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ActivitiesPeriodicity
        public IQueryable<ActivityPeriodicity> GetActivityPeriodicities()
        {
            return db.ActivityPeriodicities;
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

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityPeriodicityExists(id))
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

        // POST: api/ActivitiesPeriodicity
        [ResponseType(typeof(ActivityPeriodicity))]
        public async Task<IHttpActionResult> PostActivityPeriodicity(ActivityPeriodicity activityPeriodicity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ActivityPeriodicities.Add(activityPeriodicity);
            await db.SaveChangesAsync();

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