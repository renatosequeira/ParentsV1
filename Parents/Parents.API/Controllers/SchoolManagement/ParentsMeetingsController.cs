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
using Parents.Domain.SchoolManagement;

namespace Parents.API.Controllers.SchoolManagement
{
    public class ParentsMeetingsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ParentsMeetings
        public IQueryable<ParentsMeeting> GetParentsMeetings()
        {
            return db.ParentsMeetings;
        }

        // GET: api/ParentsMeetings/5
        [ResponseType(typeof(ParentsMeeting))]
        public async Task<IHttpActionResult> GetParentsMeeting(int id)
        {
            ParentsMeeting parentsMeeting = await db.ParentsMeetings.FindAsync(id);
            if (parentsMeeting == null)
            {
                return NotFound();
            }

            return Ok(parentsMeeting);
        }

        // PUT: api/ParentsMeetings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutParentsMeeting(int id, ParentsMeeting parentsMeeting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parentsMeeting.ParentsMeetingId)
            {
                return BadRequest();
            }

            db.Entry(parentsMeeting).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentsMeetingExists(id))
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

        // POST: api/ParentsMeetings
        [ResponseType(typeof(ParentsMeeting))]
        public async Task<IHttpActionResult> PostParentsMeeting(ParentsMeeting parentsMeeting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ParentsMeetings.Add(parentsMeeting);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = parentsMeeting.ParentsMeetingId }, parentsMeeting);
        }

        // DELETE: api/ParentsMeetings/5
        [ResponseType(typeof(ParentsMeeting))]
        public async Task<IHttpActionResult> DeleteParentsMeeting(int id)
        {
            ParentsMeeting parentsMeeting = await db.ParentsMeetings.FindAsync(id);
            if (parentsMeeting == null)
            {
                return NotFound();
            }

            db.ParentsMeetings.Remove(parentsMeeting);
            await db.SaveChangesAsync();

            return Ok(parentsMeeting);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParentsMeetingExists(int id)
        {
            return db.ParentsMeetings.Count(e => e.ParentsMeetingId == id) > 0;
        }
    }
}