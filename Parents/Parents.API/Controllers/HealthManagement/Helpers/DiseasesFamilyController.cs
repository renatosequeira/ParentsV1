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
    public class DiseasesFamilyController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/DiseasesFamily
        public IQueryable<DiseaseFamily> GetDiseaseFamilies()
        {
            return db.DiseaseFamilies;
        }

        // GET: api/DiseasesFamily/5
        [ResponseType(typeof(DiseaseFamily))]
        public async Task<IHttpActionResult> GetDiseaseFamily(int id)
        {
            DiseaseFamily diseaseFamily = await db.DiseaseFamilies.FindAsync(id);
            if (diseaseFamily == null)
            {
                return NotFound();
            }

            return Ok(diseaseFamily);
        }

        // PUT: api/DiseasesFamily/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDiseaseFamily(int id, DiseaseFamily diseaseFamily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != diseaseFamily.DiseaseFamilyId)
            {
                return BadRequest();
            }

            db.Entry(diseaseFamily).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiseaseFamilyExists(id))
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

        // POST: api/DiseasesFamily
        [ResponseType(typeof(DiseaseFamily))]
        public async Task<IHttpActionResult> PostDiseaseFamily(DiseaseFamily diseaseFamily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DiseaseFamilies.Add(diseaseFamily);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = diseaseFamily.DiseaseFamilyId }, diseaseFamily);
        }

        // DELETE: api/DiseasesFamily/5
        [ResponseType(typeof(DiseaseFamily))]
        public async Task<IHttpActionResult> DeleteDiseaseFamily(int id)
        {
            DiseaseFamily diseaseFamily = await db.DiseaseFamilies.FindAsync(id);
            if (diseaseFamily == null)
            {
                return NotFound();
            }

            db.DiseaseFamilies.Remove(diseaseFamily);
            await db.SaveChangesAsync();

            return Ok(diseaseFamily);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiseaseFamilyExists(int id)
        {
            return db.DiseaseFamilies.Count(e => e.DiseaseFamilyId == id) > 0;
        }
    }
}