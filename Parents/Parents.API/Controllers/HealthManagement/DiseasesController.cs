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
    [Authorize]
    public class DiseasesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Diseases
        public IQueryable<Disease> GetDiseases()
        {
            return db.Diseases;
        }

        // GET: api/Diseases/5
        [ResponseType(typeof(Disease))]
        public async Task<IHttpActionResult> GetDisease(int id)
        {
            Disease disease = await db.Diseases.FindAsync(id);
            if (disease == null)
            {
                return NotFound();
            }

            return Ok(disease);
        }

        // PUT: api/Diseases/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDisease(int id, Disease disease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != disease.DiseaseId)
            {
                return BadRequest();
            }

            db.Entry(disease).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiseaseExists(id))
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

        // POST: api/Diseases
        [ResponseType(typeof(Disease))]
        public async Task<IHttpActionResult> PostDisease(Disease disease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Diseases.Add(disease);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = disease.DiseaseId }, disease);
        }

        // DELETE: api/Diseases/5
        [ResponseType(typeof(Disease))]
        public async Task<IHttpActionResult> DeleteDisease(int id)
        {
            Disease disease = await db.Diseases.FindAsync(id);
            if (disease == null)
            {
                return NotFound();
            }

            db.Diseases.Remove(disease);
            await db.SaveChangesAsync();

            return Ok(disease);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiseaseExists(int id)
        {
            return db.Diseases.Count(e => e.DiseaseId == id) > 0;
        }
    }
}