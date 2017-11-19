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
    public class MedicalInstitutionsPController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/MedicalInstitutionsP
        public IQueryable<MedicalInstitutions> GetMedicalInstitutions()
        {
            return db.MedicalInstitutions;
        }

        // GET: api/MedicalInstitutionsP/5
        [ResponseType(typeof(MedicalInstitutions))]
        public async Task<IHttpActionResult> GetMedicalInstitutions(int id)
        {
            MedicalInstitutions medicalInstitutions = await db.MedicalInstitutions.FindAsync(id);
            if (medicalInstitutions == null)
            {
                return NotFound();
            }

            return Ok(medicalInstitutions);
        }

        // PUT: api/MedicalInstitutionsP/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMedicalInstitutions(int id, MedicalInstitutions medicalInstitutions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicalInstitutions.MedicalInstitutionId)
            {
                return BadRequest();
            }

            db.Entry(medicalInstitutions).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalInstitutionsExists(id))
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

        // POST: api/MedicalInstitutionsP
        [ResponseType(typeof(MedicalInstitutions))]
        public async Task<IHttpActionResult> PostMedicalInstitutions(MedicalInstitutions medicalInstitutions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MedicalInstitutions.Add(medicalInstitutions);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = medicalInstitutions.MedicalInstitutionId }, medicalInstitutions);
        }

        // DELETE: api/MedicalInstitutionsP/5
        [ResponseType(typeof(MedicalInstitutions))]
        public async Task<IHttpActionResult> DeleteMedicalInstitutions(int id)
        {
            MedicalInstitutions medicalInstitutions = await db.MedicalInstitutions.FindAsync(id);
            if (medicalInstitutions == null)
            {
                return NotFound();
            }

            db.MedicalInstitutions.Remove(medicalInstitutions);
            await db.SaveChangesAsync();

            return Ok(medicalInstitutions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MedicalInstitutionsExists(int id)
        {
            return db.MedicalInstitutions.Count(e => e.MedicalInstitutionId == id) > 0;
        }
    }
}