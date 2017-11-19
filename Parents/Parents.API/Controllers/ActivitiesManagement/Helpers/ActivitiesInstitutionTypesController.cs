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
    public class ActivitiesInstitutionTypesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ActivitiesInstitutionTypes
        public IQueryable<ActivityInstitutionType> GetActivityInstitutionTypes()
        {
            return db.ActivityInstitutionTypes;
        }

        // GET: api/ActivitiesInstitutionTypes/5
        [ResponseType(typeof(ActivityInstitutionType))]
        public async Task<IHttpActionResult> GetActivityInstitutionType(int id)
        {
            ActivityInstitutionType activityInstitutionType = await db.ActivityInstitutionTypes.FindAsync(id);
            if (activityInstitutionType == null)
            {
                return NotFound();
            }

            return Ok(activityInstitutionType);
        }

        // PUT: api/ActivitiesInstitutionTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutActivityInstitutionType(int id, ActivityInstitutionType activityInstitutionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activityInstitutionType.ActivityInstitutionTypeId)
            {
                return BadRequest();
            }

            db.Entry(activityInstitutionType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityInstitutionTypeExists(id))
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

        // POST: api/ActivitiesInstitutionTypes
        [ResponseType(typeof(ActivityInstitutionType))]
        public async Task<IHttpActionResult> PostActivityInstitutionType(ActivityInstitutionType activityInstitutionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ActivityInstitutionTypes.Add(activityInstitutionType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = activityInstitutionType.ActivityInstitutionTypeId }, activityInstitutionType);
        }

        // DELETE: api/ActivitiesInstitutionTypes/5
        [ResponseType(typeof(ActivityInstitutionType))]
        public async Task<IHttpActionResult> DeleteActivityInstitutionType(int id)
        {
            ActivityInstitutionType activityInstitutionType = await db.ActivityInstitutionTypes.FindAsync(id);
            if (activityInstitutionType == null)
            {
                return NotFound();
            }

            db.ActivityInstitutionTypes.Remove(activityInstitutionType);
            await db.SaveChangesAsync();

            return Ok(activityInstitutionType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActivityInstitutionTypeExists(int id)
        {
            return db.ActivityInstitutionTypes.Count(e => e.ActivityInstitutionTypeId == id) > 0;
        }
    }
}