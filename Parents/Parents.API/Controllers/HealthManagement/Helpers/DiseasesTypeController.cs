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
    [Authorize]
    public class DiseasesTypeController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/DiseasesType
        public IQueryable<DiseaseType> GetDiseaseTypes()
        {
            return db.DiseaseTypes;
        }

        // GET: api/DiseasesType/5
        [ResponseType(typeof(DiseaseType))]
        public async Task<IHttpActionResult> GetDiseaseType(int id)
        {
            DiseaseType diseaseType = await db.DiseaseTypes.FindAsync(id);
            if (diseaseType == null)
            {
                return NotFound();
            }

            return Ok(diseaseType);
        }

        // PUT: api/DiseasesType/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDiseaseType(int id, DiseaseType diseaseType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != diseaseType.DiseaseTypeId)
            {
                return BadRequest();
            }

            db.Entry(diseaseType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiseaseTypeExists(id))
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

        // POST: api/DiseasesType
        [ResponseType(typeof(DiseaseType))]
        public async Task<IHttpActionResult> PostDiseaseType(DiseaseType diseaseType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DiseaseTypes.Add(diseaseType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = diseaseType.DiseaseTypeId }, diseaseType);
        }

        // DELETE: api/DiseasesType/5
        [ResponseType(typeof(DiseaseType))]
        public async Task<IHttpActionResult> DeleteDiseaseType(int id)
        {
            DiseaseType diseaseType = await db.DiseaseTypes.FindAsync(id);
            if (diseaseType == null)
            {
                return NotFound();
            }

            db.DiseaseTypes.Remove(diseaseType);
            await db.SaveChangesAsync();

            return Ok(diseaseType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiseaseTypeExists(int id)
        {
            return db.DiseaseTypes.Count(e => e.DiseaseTypeId == id) > 0;
        }
    }
}