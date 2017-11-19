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

namespace Parents.API.Controllers.HealthManagement.Helpers
{
    public class AlergiesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Alergies
        public IQueryable<Alergy> GetAlergies()
        {
            return db.Alergies;
        }

        // GET: api/Alergies/5
        [ResponseType(typeof(Alergy))]
        public async Task<IHttpActionResult> GetAlergy(int id)
        {
            Alergy alergy = await db.Alergies.FindAsync(id);
            if (alergy == null)
            {
                return NotFound();
            }

            return Ok(alergy);
        }

        // PUT: api/Alergies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAlergy(int id, Alergy alergy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alergy.AlergyId)
            {
                return BadRequest();
            }

            db.Entry(alergy).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlergyExists(id))
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

        // POST: api/Alergies
        [ResponseType(typeof(Alergy))]
        public async Task<IHttpActionResult> PostAlergy(Alergy alergy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Alergies.Add(alergy);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = alergy.AlergyId }, alergy);
        }

        // DELETE: api/Alergies/5
        [ResponseType(typeof(Alergy))]
        public async Task<IHttpActionResult> DeleteAlergy(int id)
        {
            Alergy alergy = await db.Alergies.FindAsync(id);
            if (alergy == null)
            {
                return NotFound();
            }

            db.Alergies.Remove(alergy);
            await db.SaveChangesAsync();

            return Ok(alergy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlergyExists(int id)
        {
            return db.Alergies.Count(e => e.AlergyId == id) > 0;
        }
    }
}