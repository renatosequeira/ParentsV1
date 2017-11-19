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
    public class ParentalTypesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ParentalTypes
        public IQueryable<ParentalType> GetParentalTypes()
        {
            return db.ParentalTypes;
        }

        // GET: api/ParentalTypes/5
        [ResponseType(typeof(ParentalType))]
        public async Task<IHttpActionResult> GetParentalType(int id)
        {
            ParentalType parentalType = await db.ParentalTypes.FindAsync(id);
            if (parentalType == null)
            {
                return NotFound();
            }

            return Ok(parentalType);
        }

        // PUT: api/ParentalTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutParentalType(int id, ParentalType parentalType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parentalType.ParentalTypeId)
            {
                return BadRequest();
            }

            db.Entry(parentalType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentalTypeExists(id))
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

        // POST: api/ParentalTypes
        [ResponseType(typeof(ParentalType))]
        public async Task<IHttpActionResult> PostParentalType(ParentalType parentalType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ParentalTypes.Add(parentalType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = parentalType.ParentalTypeId }, parentalType);
        }

        // DELETE: api/ParentalTypes/5
        [ResponseType(typeof(ParentalType))]
        public async Task<IHttpActionResult> DeleteParentalType(int id)
        {
            ParentalType parentalType = await db.ParentalTypes.FindAsync(id);
            if (parentalType == null)
            {
                return NotFound();
            }

            db.ParentalTypes.Remove(parentalType);
            await db.SaveChangesAsync();

            return Ok(parentalType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParentalTypeExists(int id)
        {
            return db.ParentalTypes.Count(e => e.ParentalTypeId == id) > 0;
        }
    }
}