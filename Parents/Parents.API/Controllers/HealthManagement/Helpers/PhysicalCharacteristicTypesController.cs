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
    public class PhysicalCharacteristicTypesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/PhysicalCharacteristicTypes
        public IQueryable<PhysicalCharacteristicType> GetPhysicalCharacteristicTypes()
        {
            return db.PhysicalCharacteristicTypes;
        }

        // GET: api/PhysicalCharacteristicTypes/5
        [ResponseType(typeof(PhysicalCharacteristicType))]
        public async Task<IHttpActionResult> GetPhysicalCharacteristicType(int id)
        {
            PhysicalCharacteristicType physicalCharacteristicType = await db.PhysicalCharacteristicTypes.FindAsync(id);
            if (physicalCharacteristicType == null)
            {
                return NotFound();
            }

            return Ok(physicalCharacteristicType);
        }

        // PUT: api/PhysicalCharacteristicTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPhysicalCharacteristicType(int id, PhysicalCharacteristicType physicalCharacteristicType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != physicalCharacteristicType.PhysicalCharacteristicTypeId)
            {
                return BadRequest();
            }

            db.Entry(physicalCharacteristicType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhysicalCharacteristicTypeExists(id))
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

        // POST: api/PhysicalCharacteristicTypes
        [ResponseType(typeof(PhysicalCharacteristicType))]
        public async Task<IHttpActionResult> PostPhysicalCharacteristicType(PhysicalCharacteristicType physicalCharacteristicType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PhysicalCharacteristicTypes.Add(physicalCharacteristicType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = physicalCharacteristicType.PhysicalCharacteristicTypeId }, physicalCharacteristicType);
        }

        // DELETE: api/PhysicalCharacteristicTypes/5
        [ResponseType(typeof(PhysicalCharacteristicType))]
        public async Task<IHttpActionResult> DeletePhysicalCharacteristicType(int id)
        {
            PhysicalCharacteristicType physicalCharacteristicType = await db.PhysicalCharacteristicTypes.FindAsync(id);
            if (physicalCharacteristicType == null)
            {
                return NotFound();
            }

            db.PhysicalCharacteristicTypes.Remove(physicalCharacteristicType);
            await db.SaveChangesAsync();

            return Ok(physicalCharacteristicType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhysicalCharacteristicTypeExists(int id)
        {
            return db.PhysicalCharacteristicTypes.Count(e => e.PhysicalCharacteristicTypeId == id) > 0;
        }
    }
}