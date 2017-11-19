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
using Parents.Domain.AppCore;

namespace Parents.API.Controllers.AppCore
{
    [Authorize]
    public class ChildsManagementController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ChildsManagement
        public IQueryable<ChildManagement> GetChildManagements()
        {
            return db.ChildManagements;
        }

        // GET: api/ChildsManagement/5
        [ResponseType(typeof(ChildManagement))]
        public async Task<IHttpActionResult> GetChildManagement(int id)
        {
            ChildManagement childManagement = await db.ChildManagements.FindAsync(id);
            if (childManagement == null)
            {
                return NotFound();
            }

            return Ok(childManagement);
        }

        // PUT: api/ChildsManagement/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutChildManagement(int id, ChildManagement childManagement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != childManagement.ChildManagementId)
            {
                return BadRequest();
            }

            db.Entry(childManagement).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChildManagementExists(id))
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

        // POST: api/ChildsManagement
        [ResponseType(typeof(ChildManagement))]
        public async Task<IHttpActionResult> PostChildManagement(ChildManagement childManagement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ChildManagements.Add(childManagement);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = childManagement.ChildManagementId }, childManagement);
        }

        // DELETE: api/ChildsManagement/5
        [ResponseType(typeof(ChildManagement))]
        public async Task<IHttpActionResult> DeleteChildManagement(int id)
        {
            ChildManagement childManagement = await db.ChildManagements.FindAsync(id);
            if (childManagement == null)
            {
                return NotFound();
            }

            db.ChildManagements.Remove(childManagement);
            await db.SaveChangesAsync();

            return Ok(childManagement);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChildManagementExists(int id)
        {
            return db.ChildManagements.Count(e => e.ChildManagementId == id) > 0;
        }
    }
}