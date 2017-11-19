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
    public class UrgencyCategoriesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/UrgencyCategories
        public IQueryable<UrgencyCategory> GetUrgencyCategories()
        {
            return db.UrgencyCategories;
        }

        // GET: api/UrgencyCategories/5
        [ResponseType(typeof(UrgencyCategory))]
        public async Task<IHttpActionResult> GetUrgencyCategory(int id)
        {
            UrgencyCategory urgencyCategory = await db.UrgencyCategories.FindAsync(id);
            if (urgencyCategory == null)
            {
                return NotFound();
            }

            return Ok(urgencyCategory);
        }

        // PUT: api/UrgencyCategories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUrgencyCategory(int id, UrgencyCategory urgencyCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != urgencyCategory.UrgencyCategoryId)
            {
                return BadRequest();
            }

            db.Entry(urgencyCategory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UrgencyCategoryExists(id))
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

        // POST: api/UrgencyCategories
        [ResponseType(typeof(UrgencyCategory))]
        public async Task<IHttpActionResult> PostUrgencyCategory(UrgencyCategory urgencyCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UrgencyCategories.Add(urgencyCategory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = urgencyCategory.UrgencyCategoryId }, urgencyCategory);
        }

        // DELETE: api/UrgencyCategories/5
        [ResponseType(typeof(UrgencyCategory))]
        public async Task<IHttpActionResult> DeleteUrgencyCategory(int id)
        {
            UrgencyCategory urgencyCategory = await db.UrgencyCategories.FindAsync(id);
            if (urgencyCategory == null)
            {
                return NotFound();
            }

            db.UrgencyCategories.Remove(urgencyCategory);
            await db.SaveChangesAsync();

            return Ok(urgencyCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UrgencyCategoryExists(int id)
        {
            return db.UrgencyCategories.Count(e => e.UrgencyCategoryId == id) > 0;
        }
    }
}