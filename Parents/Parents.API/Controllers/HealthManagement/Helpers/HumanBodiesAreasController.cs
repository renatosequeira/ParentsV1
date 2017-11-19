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
    public class HumanBodiesAreasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/HumanBodiesAreas
        public IQueryable<HumanBodyAreas> GetHumanBodyAreas()
        {
            return db.HumanBodyAreas;
        }

        // GET: api/HumanBodiesAreas/5
        [ResponseType(typeof(HumanBodyAreas))]
        public async Task<IHttpActionResult> GetHumanBodyAreas(int id)
        {
            HumanBodyAreas humanBodyAreas = await db.HumanBodyAreas.FindAsync(id);
            if (humanBodyAreas == null)
            {
                return NotFound();
            }

            return Ok(humanBodyAreas);
        }

        // PUT: api/HumanBodiesAreas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHumanBodyAreas(int id, HumanBodyAreas humanBodyAreas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != humanBodyAreas.HumanBodyAreaId)
            {
                return BadRequest();
            }

            db.Entry(humanBodyAreas).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HumanBodyAreasExists(id))
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

        // POST: api/HumanBodiesAreas
        [ResponseType(typeof(HumanBodyAreas))]
        public async Task<IHttpActionResult> PostHumanBodyAreas(HumanBodyAreas humanBodyAreas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HumanBodyAreas.Add(humanBodyAreas);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = humanBodyAreas.HumanBodyAreaId }, humanBodyAreas);
        }

        // DELETE: api/HumanBodiesAreas/5
        [ResponseType(typeof(HumanBodyAreas))]
        public async Task<IHttpActionResult> DeleteHumanBodyAreas(int id)
        {
            HumanBodyAreas humanBodyAreas = await db.HumanBodyAreas.FindAsync(id);
            if (humanBodyAreas == null)
            {
                return NotFound();
            }

            db.HumanBodyAreas.Remove(humanBodyAreas);
            await db.SaveChangesAsync();

            return Ok(humanBodyAreas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HumanBodyAreasExists(int id)
        {
            return db.HumanBodyAreas.Count(e => e.HumanBodyAreaId == id) > 0;
        }
    }
}