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
    public class BloodInformationsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/BloodInformations
        public IQueryable<BloodInformation> GetBloodInformations()
        {
            return db.BloodInformations;
        }

        // GET: api/BloodInformations/5
        [ResponseType(typeof(BloodInformation))]
        public async Task<IHttpActionResult> GetBloodInformation(int id)
        {
            BloodInformation bloodInformation = await db.BloodInformations.FindAsync(id);
            if (bloodInformation == null)
            {
                return NotFound();
            }

            return Ok(bloodInformation);
        }

        // PUT: api/BloodInformations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBloodInformation(int id, BloodInformation bloodInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bloodInformation.BoodInformationId)
            {
                return BadRequest();
            }

            db.Entry(bloodInformation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloodInformationExists(id))
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

        // POST: api/BloodInformations
        [ResponseType(typeof(BloodInformation))]
        public async Task<IHttpActionResult> PostBloodInformation(BloodInformation bloodInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BloodInformations.Add(bloodInformation);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = bloodInformation.BoodInformationId }, bloodInformation);
        }

        // DELETE: api/BloodInformations/5
        [ResponseType(typeof(BloodInformation))]
        public async Task<IHttpActionResult> DeleteBloodInformation(int id)
        {
            BloodInformation bloodInformation = await db.BloodInformations.FindAsync(id);
            if (bloodInformation == null)
            {
                return NotFound();
            }

            db.BloodInformations.Remove(bloodInformation);
            await db.SaveChangesAsync();

            return Ok(bloodInformation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BloodInformationExists(int id)
        {
            return db.BloodInformations.Count(e => e.BoodInformationId == id) > 0;
        }
    }
}