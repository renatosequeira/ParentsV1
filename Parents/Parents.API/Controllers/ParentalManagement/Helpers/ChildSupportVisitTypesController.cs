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

namespace Parents.API.Controllers.ParentalManagement.Helpers
{
    public class ChildSupportVisitTypesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ChildSupportVisitTypes
        public IQueryable<ChildSupportVisitType> GetChildSupportVisitTypes()
        {
            return db.ChildSupportVisitTypes;
        }

        // GET: api/ChildSupportVisitTypes/5
        [ResponseType(typeof(ChildSupportVisitType))]
        public async Task<IHttpActionResult> GetChildSupportVisitType(int id)
        {
            ChildSupportVisitType childSupportVisitType = await db.ChildSupportVisitTypes.FindAsync(id);
            if (childSupportVisitType == null)
            {
                return NotFound();
            }

            return Ok(childSupportVisitType);
        }

        // PUT: api/ChildSupportVisitTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutChildSupportVisitType(int id, ChildSupportVisitType childSupportVisitType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != childSupportVisitType.ChildSupportVisitTypeId)
            {
                return BadRequest();
            }

            db.Entry(childSupportVisitType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChildSupportVisitTypeExists(id))
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

        // POST: api/ChildSupportVisitTypes
        [ResponseType(typeof(ChildSupportVisitType))]
        public async Task<IHttpActionResult> PostChildSupportVisitType(ChildSupportVisitType childSupportVisitType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ChildSupportVisitTypes.Add(childSupportVisitType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = childSupportVisitType.ChildSupportVisitTypeId }, childSupportVisitType);
        }

        // DELETE: api/ChildSupportVisitTypes/5
        [ResponseType(typeof(ChildSupportVisitType))]
        public async Task<IHttpActionResult> DeleteChildSupportVisitType(int id)
        {
            ChildSupportVisitType childSupportVisitType = await db.ChildSupportVisitTypes.FindAsync(id);
            if (childSupportVisitType == null)
            {
                return NotFound();
            }

            db.ChildSupportVisitTypes.Remove(childSupportVisitType);
            await db.SaveChangesAsync();

            return Ok(childSupportVisitType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChildSupportVisitTypeExists(int id)
        {
            return db.ChildSupportVisitTypes.Count(e => e.ChildSupportVisitTypeId == id) > 0;
        }
    }
}