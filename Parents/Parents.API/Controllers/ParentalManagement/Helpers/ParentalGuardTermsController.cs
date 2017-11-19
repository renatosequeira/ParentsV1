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
    public class ParentalGuardTermsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ParentalGuardTerms
        public IQueryable<ParentalGuardTerm> GetParentalGuardTerms()
        {
            return db.ParentalGuardTerms;
        }

        // GET: api/ParentalGuardTerms/5
        [ResponseType(typeof(ParentalGuardTerm))]
        public async Task<IHttpActionResult> GetParentalGuardTerm(int id)
        {
            ParentalGuardTerm parentalGuardTerm = await db.ParentalGuardTerms.FindAsync(id);
            if (parentalGuardTerm == null)
            {
                return NotFound();
            }

            return Ok(parentalGuardTerm);
        }

        // PUT: api/ParentalGuardTerms/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutParentalGuardTerm(int id, ParentalGuardTerm parentalGuardTerm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parentalGuardTerm.ParentalGuardTermId)
            {
                return BadRequest();
            }

            db.Entry(parentalGuardTerm).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentalGuardTermExists(id))
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

        // POST: api/ParentalGuardTerms
        [ResponseType(typeof(ParentalGuardTerm))]
        public async Task<IHttpActionResult> PostParentalGuardTerm(ParentalGuardTerm parentalGuardTerm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ParentalGuardTerms.Add(parentalGuardTerm);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = parentalGuardTerm.ParentalGuardTermId }, parentalGuardTerm);
        }

        // DELETE: api/ParentalGuardTerms/5
        [ResponseType(typeof(ParentalGuardTerm))]
        public async Task<IHttpActionResult> DeleteParentalGuardTerm(int id)
        {
            ParentalGuardTerm parentalGuardTerm = await db.ParentalGuardTerms.FindAsync(id);
            if (parentalGuardTerm == null)
            {
                return NotFound();
            }

            db.ParentalGuardTerms.Remove(parentalGuardTerm);
            await db.SaveChangesAsync();

            return Ok(parentalGuardTerm);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParentalGuardTermExists(int id)
        {
            return db.ParentalGuardTerms.Count(e => e.ParentalGuardTermId == id) > 0;
        }
    }
}