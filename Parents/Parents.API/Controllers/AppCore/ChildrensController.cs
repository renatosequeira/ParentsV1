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

namespace Parents.API.Controllers.AppCore
{
    public class ChildrensController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Childrens
        public IQueryable<Children> GetChildren()
        {
            return db.Children;
        }

        // GET: api/Childrens/5
        [ResponseType(typeof(Children))]
        public async Task<IHttpActionResult> GetChildren(int id)
        {
            Children children = await db.Children.FindAsync(id);
            if (children == null)
            {
                return NotFound();
            }

            return Ok(children);
        }

        // PUT: api/Childrens/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutChildren(int id, Children children)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != children.ChildrenId)
            {
                return BadRequest();
            }

            db.Entry(children).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChildrenExists(id))
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

        // POST: api/Childrens
        [ResponseType(typeof(Children))]
        public async Task<IHttpActionResult> PostChildren(Children children)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Children.Add(children);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = children.ChildrenId }, children);
        }

        // DELETE: api/Childrens/5
        [ResponseType(typeof(Children))]
        public async Task<IHttpActionResult> DeleteChildren(int id)
        {
            Children children = await db.Children.FindAsync(id);
            if (children == null)
            {
                return NotFound();
            }

            db.Children.Remove(children);
            await db.SaveChangesAsync();

            return Ok(children);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChildrenExists(int id)
        {
            return db.Children.Count(e => e.ChildrenId == id) > 0;
        }
    }
}