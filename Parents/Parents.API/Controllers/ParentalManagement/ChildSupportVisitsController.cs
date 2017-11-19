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
using Parents.Domain.ParentalManagement.Helpers;

namespace Parents.API.Controllers.ParentalManagement
{
    [Authorize]
    public class ChildSupportVisitsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ChildSupportVisits
        public IQueryable<ChildSupportVisit> GetChildSupportVisits()
        {
            return db.ChildSupportVisits;
        }

        // GET: api/ChildSupportVisits/5
        [ResponseType(typeof(ChildSupportVisit))]
        public async Task<IHttpActionResult> GetChildSupportVisit(int id)
        {
            ChildSupportVisit childSupportVisit = await db.ChildSupportVisits.FindAsync(id);
            if (childSupportVisit == null)
            {
                return NotFound();
            }

            return Ok(childSupportVisit);
        }

        // PUT: api/ChildSupportVisits/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutChildSupportVisit(int id, ChildSupportVisit childSupportVisit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != childSupportVisit.ChildSupportVisitId)
            {
                return BadRequest();
            }

            db.Entry(childSupportVisit).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChildSupportVisitExists(id))
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

        // POST: api/ChildSupportVisits
        [ResponseType(typeof(ChildSupportVisit))]
        public async Task<IHttpActionResult> PostChildSupportVisit(ChildSupportVisit childSupportVisit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ChildSupportVisits.Add(childSupportVisit);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = childSupportVisit.ChildSupportVisitId }, childSupportVisit);
        }

        // DELETE: api/ChildSupportVisits/5
        [ResponseType(typeof(ChildSupportVisit))]
        public async Task<IHttpActionResult> DeleteChildSupportVisit(int id)
        {
            ChildSupportVisit childSupportVisit = await db.ChildSupportVisits.FindAsync(id);
            if (childSupportVisit == null)
            {
                return NotFound();
            }

            db.ChildSupportVisits.Remove(childSupportVisit);
            await db.SaveChangesAsync();

            return Ok(childSupportVisit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChildSupportVisitExists(int id)
        {
            return db.ChildSupportVisits.Count(e => e.ChildSupportVisitId == id) > 0;
        }
    }
}