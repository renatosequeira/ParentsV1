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
using Parents.Domain.DomesticManagement;

namespace Parents.API.Controllers.DomesticManagement
{
    public class ItemsCategoriesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ItemsCategories
        public IQueryable<ItemsCategory> GetItemsCategories()
        {
            return db.ItemsCategories;
        }

        // GET: api/ItemsCategories/5
        [ResponseType(typeof(ItemsCategory))]
        public async Task<IHttpActionResult> GetItemsCategory(int id)
        {
            ItemsCategory itemsCategory = await db.ItemsCategories.FindAsync(id);
            if (itemsCategory == null)
            {
                return NotFound();
            }

            return Ok(itemsCategory);
        }

        // PUT: api/ItemsCategories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutItemsCategory(int id, ItemsCategory itemsCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemsCategory.ItemCategoryId)
            {
                return BadRequest();
            }

            db.Entry(itemsCategory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemsCategoryExists(id))
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

        // POST: api/ItemsCategories
        [ResponseType(typeof(ItemsCategory))]
        public async Task<IHttpActionResult> PostItemsCategory(ItemsCategory itemsCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ItemsCategories.Add(itemsCategory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = itemsCategory.ItemCategoryId }, itemsCategory);
        }

        // DELETE: api/ItemsCategories/5
        [ResponseType(typeof(ItemsCategory))]
        public async Task<IHttpActionResult> DeleteItemsCategory(int id)
        {
            ItemsCategory itemsCategory = await db.ItemsCategories.FindAsync(id);
            if (itemsCategory == null)
            {
                return NotFound();
            }

            db.ItemsCategories.Remove(itemsCategory);
            await db.SaveChangesAsync();

            return Ok(itemsCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemsCategoryExists(int id)
        {
            return db.ItemsCategories.Count(e => e.ItemCategoryId == id) > 0;
        }
    }
}