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
    public class ExamFamiliesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ExamFamilies
        public IQueryable<ExamFamily> GetExamFamilies()
        {
            return db.ExamFamilies;
        }

        // GET: api/ExamFamilies/5
        [ResponseType(typeof(ExamFamily))]
        public async Task<IHttpActionResult> GetExamFamily(int id)
        {
            ExamFamily examFamily = await db.ExamFamilies.FindAsync(id);
            if (examFamily == null)
            {
                return NotFound();
            }

            return Ok(examFamily);
        }

        // PUT: api/ExamFamilies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutExamFamily(int id, ExamFamily examFamily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != examFamily.ExamFamilyId)
            {
                return BadRequest();
            }

            db.Entry(examFamily).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamFamilyExists(id))
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

        // POST: api/ExamFamilies
        [ResponseType(typeof(ExamFamily))]
        public async Task<IHttpActionResult> PostExamFamily(ExamFamily examFamily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExamFamilies.Add(examFamily);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = examFamily.ExamFamilyId }, examFamily);
        }

        // DELETE: api/ExamFamilies/5
        [ResponseType(typeof(ExamFamily))]
        public async Task<IHttpActionResult> DeleteExamFamily(int id)
        {
            ExamFamily examFamily = await db.ExamFamilies.FindAsync(id);
            if (examFamily == null)
            {
                return NotFound();
            }

            db.ExamFamilies.Remove(examFamily);
            await db.SaveChangesAsync();

            return Ok(examFamily);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExamFamilyExists(int id)
        {
            return db.ExamFamilies.Count(e => e.ExamFamilyId == id) > 0;
        }
    }
}