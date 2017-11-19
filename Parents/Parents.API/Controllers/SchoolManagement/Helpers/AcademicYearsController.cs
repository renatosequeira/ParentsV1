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
using Parents.Domain.SchoolManagement.Helpers;

namespace Parents.API.Controllers.SchoolManagement.Helpers
{
    [Authorize]
    public class AcademicYearsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/AcademicYears
        public IQueryable<AcademicYear> GetAcademicYears()
        {
            return db.AcademicYears;
        }

        // GET: api/AcademicYears/5
        [ResponseType(typeof(AcademicYear))]
        public async Task<IHttpActionResult> GetAcademicYear(int id)
        {
            AcademicYear academicYear = await db.AcademicYears.FindAsync(id);
            if (academicYear == null)
            {
                return NotFound();
            }

            return Ok(academicYear);
        }

        // PUT: api/AcademicYears/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAcademicYear(int id, AcademicYear academicYear)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != academicYear.AcademicYearId)
            {
                return BadRequest();
            }

            db.Entry(academicYear).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcademicYearExists(id))
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

        // POST: api/AcademicYears
        [ResponseType(typeof(AcademicYear))]
        public async Task<IHttpActionResult> PostAcademicYear(AcademicYear academicYear)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AcademicYears.Add(academicYear);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = academicYear.AcademicYearId }, academicYear);
        }

        // DELETE: api/AcademicYears/5
        [ResponseType(typeof(AcademicYear))]
        public async Task<IHttpActionResult> DeleteAcademicYear(int id)
        {
            AcademicYear academicYear = await db.AcademicYears.FindAsync(id);
            if (academicYear == null)
            {
                return NotFound();
            }

            db.AcademicYears.Remove(academicYear);
            await db.SaveChangesAsync();

            return Ok(academicYear);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AcademicYearExists(int id)
        {
            return db.AcademicYears.Count(e => e.AcademicYearId == id) > 0;
        }
    }
}