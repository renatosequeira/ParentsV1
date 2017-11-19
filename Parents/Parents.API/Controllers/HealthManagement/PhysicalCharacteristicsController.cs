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
    public class PhysicalCharacteristicsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/PhysicalCharacteristics
        public IQueryable<PhysicalCharacteristic> GetPhysicalCharacteristics()
        {
            return db.PhysicalCharacteristics;
        }

        // GET: api/PhysicalCharacteristics/5
        [ResponseType(typeof(PhysicalCharacteristic))]
        public async Task<IHttpActionResult> GetPhysicalCharacteristic(int id)
        {
            PhysicalCharacteristic physicalCharacteristic = await db.PhysicalCharacteristics.FindAsync(id);
            if (physicalCharacteristic == null)
            {
                return NotFound();
            }

            return Ok(physicalCharacteristic);
        }

        // PUT: api/PhysicalCharacteristics/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPhysicalCharacteristic(int id, PhysicalCharacteristic physicalCharacteristic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != physicalCharacteristic.PhysicalCharacteristicId)
            {
                return BadRequest();
            }

            db.Entry(physicalCharacteristic).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhysicalCharacteristicExists(id))
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

        // POST: api/PhysicalCharacteristics
        [ResponseType(typeof(PhysicalCharacteristic))]
        public async Task<IHttpActionResult> PostPhysicalCharacteristic(PhysicalCharacteristic physicalCharacteristic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PhysicalCharacteristics.Add(physicalCharacteristic);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = physicalCharacteristic.PhysicalCharacteristicId }, physicalCharacteristic);
        }

        // DELETE: api/PhysicalCharacteristics/5
        [ResponseType(typeof(PhysicalCharacteristic))]
        public async Task<IHttpActionResult> DeletePhysicalCharacteristic(int id)
        {
            PhysicalCharacteristic physicalCharacteristic = await db.PhysicalCharacteristics.FindAsync(id);
            if (physicalCharacteristic == null)
            {
                return NotFound();
            }

            db.PhysicalCharacteristics.Remove(physicalCharacteristic);
            await db.SaveChangesAsync();

            return Ok(physicalCharacteristic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhysicalCharacteristicExists(int id)
        {
            return db.PhysicalCharacteristics.Count(e => e.PhysicalCharacteristicId == id) > 0;
        }
    }
}