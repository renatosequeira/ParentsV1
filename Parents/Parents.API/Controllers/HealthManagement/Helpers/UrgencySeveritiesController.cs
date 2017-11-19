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
using Parents.Domain.HealthManagement.Categories;

namespace Parents.API.Controllers.HealthManagement.Helpers
{
    [Authorize]
    public class UrgencySeveritiesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/UrgencySeverities
        public IQueryable<UrgencySeverity> GetUrgencySeverities()
        {
            return db.UrgencySeverities;
        }

        // GET: api/UrgencySeverities/5
        [ResponseType(typeof(UrgencySeverity))]
        public async Task<IHttpActionResult> GetUrgencySeverity(int id)
        {
            UrgencySeverity urgencySeverity = await db.UrgencySeverities.FindAsync(id);
            if (urgencySeverity == null)
            {
                return NotFound();
            }

            return Ok(urgencySeverity);
        }

        // PUT: api/UrgencySeverities/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUrgencySeverity(int id, UrgencySeverity urgencySeverity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != urgencySeverity.UrgencySeverityId)
            {
                return BadRequest();
            }

            db.Entry(urgencySeverity).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UrgencySeverityExists(id))
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

        // POST: api/UrgencySeverities
        [ResponseType(typeof(UrgencySeverity))]
        public async Task<IHttpActionResult> PostUrgencySeverity(UrgencySeverity urgencySeverity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UrgencySeverities.Add(urgencySeverity);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = urgencySeverity.UrgencySeverityId }, urgencySeverity);
        }

        // DELETE: api/UrgencySeverities/5
        [ResponseType(typeof(UrgencySeverity))]
        public async Task<IHttpActionResult> DeleteUrgencySeverity(int id)
        {
            UrgencySeverity urgencySeverity = await db.UrgencySeverities.FindAsync(id);
            if (urgencySeverity == null)
            {
                return NotFound();
            }

            db.UrgencySeverities.Remove(urgencySeverity);
            await db.SaveChangesAsync();

            return Ok(urgencySeverity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UrgencySeverityExists(int id)
        {
            return db.UrgencySeverities.Count(e => e.UrgencySeverityId == id) > 0;
        }
    }
}