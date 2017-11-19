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
using Parents.Domain.ParentalManagement.Helpers;

namespace Parents.API.Controllers.ParentalManagement
{
    [Authorize]
    public class ChildSupportPaymentsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ChildSupportPayments
        public IQueryable<ChildSupportPayment> GetChildSupportPayments()
        {
            return db.ChildSupportPayments;
        }

        // GET: api/ChildSupportPayments/5
        [ResponseType(typeof(ChildSupportPayment))]
        public async Task<IHttpActionResult> GetChildSupportPayment(int id)
        {
            ChildSupportPayment childSupportPayment = await db.ChildSupportPayments.FindAsync(id);
            if (childSupportPayment == null)
            {
                return NotFound();
            }

            return Ok(childSupportPayment);
        }

        // PUT: api/ChildSupportPayments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutChildSupportPayment(int id, ChildSupportPayment childSupportPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != childSupportPayment.ChildSupportPaymentId)
            {
                return BadRequest();
            }

            db.Entry(childSupportPayment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChildSupportPaymentExists(id))
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

        // POST: api/ChildSupportPayments
        [ResponseType(typeof(ChildSupportPayment))]
        public async Task<IHttpActionResult> PostChildSupportPayment(ChildSupportPayment childSupportPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ChildSupportPayments.Add(childSupportPayment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = childSupportPayment.ChildSupportPaymentId }, childSupportPayment);
        }

        // DELETE: api/ChildSupportPayments/5
        [ResponseType(typeof(ChildSupportPayment))]
        public async Task<IHttpActionResult> DeleteChildSupportPayment(int id)
        {
            ChildSupportPayment childSupportPayment = await db.ChildSupportPayments.FindAsync(id);
            if (childSupportPayment == null)
            {
                return NotFound();
            }

            db.ChildSupportPayments.Remove(childSupportPayment);
            await db.SaveChangesAsync();

            return Ok(childSupportPayment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChildSupportPaymentExists(int id)
        {
            return db.ChildSupportPayments.Count(e => e.ChildSupportPaymentId == id) > 0;
        }
    }
}