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
    [Authorize]
    public class ItemsToBuyController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ItemsToBuy
        public IQueryable<ItemToBuy> GetItemToBuys()
        {
            return db.ItemToBuys;
        }

        // GET: api/ItemsToBuy/5
        [ResponseType(typeof(ItemToBuy))]
        public async Task<IHttpActionResult> GetItemToBuy(int id)
        {
            ItemToBuy itemToBuy = await db.ItemToBuys.FindAsync(id);
            if (itemToBuy == null)
            {
                return NotFound();
            }

            return Ok(itemToBuy);
        }

        // PUT: api/ItemsToBuy/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutItemToBuy(int id, ItemToBuy itemToBuy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemToBuy.ItemToBuyId)
            {
                return BadRequest();
            }

            db.Entry(itemToBuy).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemToBuyExists(id))
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

        // POST: api/ItemsToBuy
        [ResponseType(typeof(ItemToBuy))]
        public async Task<IHttpActionResult> PostItemToBuy(ItemToBuy itemToBuy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ItemToBuys.Add(itemToBuy);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = itemToBuy.ItemToBuyId }, itemToBuy);
        }

        // DELETE: api/ItemsToBuy/5
        [ResponseType(typeof(ItemToBuy))]
        public async Task<IHttpActionResult> DeleteItemToBuy(int id)
        {
            ItemToBuy itemToBuy = await db.ItemToBuys.FindAsync(id);
            if (itemToBuy == null)
            {
                return NotFound();
            }

            db.ItemToBuys.Remove(itemToBuy);
            await db.SaveChangesAsync();

            return Ok(itemToBuy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemToBuyExists(int id)
        {
            return db.ItemToBuys.Count(e => e.ItemToBuyId == id) > 0;
        }
    }
}