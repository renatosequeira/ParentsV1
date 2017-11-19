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
using Parents.Domain.HealthManagement;

namespace Parents.API.Controllers.HealthManagement
{
    public class UrgenciesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Urgencies
        public IQueryable<Urgency> GetUrgencies()
        {
            return db.Urgencies;
        }

        // GET: api/Urgencies/5
        [ResponseType(typeof(Urgency))]
        public async Task<IHttpActionResult> GetUrgency(int id)
        {
            Urgency urgency = await db.Urgencies.FindAsync(id);
            if (urgency == null)
            {
                return NotFound();
            }

            return Ok(urgency);
        }

        // PUT: api/Urgencies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUrgency(int id, Urgency urgency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != urgency.UrgencyId)
            {
                return BadRequest();
            }

            db.Entry(urgency).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UrgencyExists(id))
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

        // POST: api/Urgencies
        [ResponseType(typeof(Urgency))]
        public async Task<IHttpActionResult> PostUrgency(Urgency urgency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Urgencies.Add(urgency);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = urgency.UrgencyId }, urgency);
        }

        // DELETE: api/Urgencies/5
        [ResponseType(typeof(Urgency))]
        public async Task<IHttpActionResult> DeleteUrgency(int id)
        {
            Urgency urgency = await db.Urgencies.FindAsync(id);
            if (urgency == null)
            {
                return NotFound();
            }

            db.Urgencies.Remove(urgency);
            await db.SaveChangesAsync();

            return Ok(urgency);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UrgencyExists(int id)
        {
            return db.Urgencies.Count(e => e.UrgencyId == id) > 0;
        }
    }
}