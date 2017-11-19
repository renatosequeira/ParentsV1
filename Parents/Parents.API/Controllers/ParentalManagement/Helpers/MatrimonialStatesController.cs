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

namespace Parents.API.Controllers.ParentalManagement.Helpers
{
    [Authorize]
    public class MatrimonialStatesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/MatrimonialStates
        public IQueryable<MatrimonialState> GetMatrimonialStates()
        {
            return db.MatrimonialStates;
        }

        // GET: api/MatrimonialStates/5
        [ResponseType(typeof(MatrimonialState))]
        public async Task<IHttpActionResult> GetMatrimonialState(int id)
        {
            MatrimonialState matrimonialState = await db.MatrimonialStates.FindAsync(id);
            if (matrimonialState == null)
            {
                return NotFound();
            }

            return Ok(matrimonialState);
        }

        // PUT: api/MatrimonialStates/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMatrimonialState(int id, MatrimonialState matrimonialState)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != matrimonialState.MatrimonialStateId)
            {
                return BadRequest();
            }

            db.Entry(matrimonialState).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatrimonialStateExists(id))
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

        // POST: api/MatrimonialStates
        [ResponseType(typeof(MatrimonialState))]
        public async Task<IHttpActionResult> PostMatrimonialState(MatrimonialState matrimonialState)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MatrimonialStates.Add(matrimonialState);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = matrimonialState.MatrimonialStateId }, matrimonialState);
        }

        // DELETE: api/MatrimonialStates/5
        [ResponseType(typeof(MatrimonialState))]
        public async Task<IHttpActionResult> DeleteMatrimonialState(int id)
        {
            MatrimonialState matrimonialState = await db.MatrimonialStates.FindAsync(id);
            if (matrimonialState == null)
            {
                return NotFound();
            }

            db.MatrimonialStates.Remove(matrimonialState);
            await db.SaveChangesAsync();

            return Ok(matrimonialState);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatrimonialStateExists(int id)
        {
            return db.MatrimonialStates.Count(e => e.MatrimonialStateId == id) > 0;
        }
    }
}