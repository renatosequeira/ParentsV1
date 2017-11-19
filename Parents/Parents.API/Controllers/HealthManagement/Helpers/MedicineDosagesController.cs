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
    public class MedicineDosagesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/MedicineDosages
        public IQueryable<MedicineDosage> GetMedicineDosages()
        {
            return db.MedicineDosages;
        }

        // GET: api/MedicineDosages/5
        [ResponseType(typeof(MedicineDosage))]
        public async Task<IHttpActionResult> GetMedicineDosage(int id)
        {
            MedicineDosage medicineDosage = await db.MedicineDosages.FindAsync(id);
            if (medicineDosage == null)
            {
                return NotFound();
            }

            return Ok(medicineDosage);
        }

        // PUT: api/MedicineDosages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMedicineDosage(int id, MedicineDosage medicineDosage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicineDosage.MedicineDosageId)
            {
                return BadRequest();
            }

            db.Entry(medicineDosage).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineDosageExists(id))
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

        // POST: api/MedicineDosages
        [ResponseType(typeof(MedicineDosage))]
        public async Task<IHttpActionResult> PostMedicineDosage(MedicineDosage medicineDosage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MedicineDosages.Add(medicineDosage);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = medicineDosage.MedicineDosageId }, medicineDosage);
        }

        // DELETE: api/MedicineDosages/5
        [ResponseType(typeof(MedicineDosage))]
        public async Task<IHttpActionResult> DeleteMedicineDosage(int id)
        {
            MedicineDosage medicineDosage = await db.MedicineDosages.FindAsync(id);
            if (medicineDosage == null)
            {
                return NotFound();
            }

            db.MedicineDosages.Remove(medicineDosage);
            await db.SaveChangesAsync();

            return Ok(medicineDosage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MedicineDosageExists(int id)
        {
            return db.MedicineDosages.Count(e => e.MedicineDosageId == id) > 0;
        }
    }
}