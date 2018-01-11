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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Parents.API.Models;
using Parents.Domain;

namespace Parents.API.Controllers.AppCore
{
    //[Authorize]
    public class ParentsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Parents
        public IQueryable<Parent> GetParents()
        {
            return db.Parents;
        }

        // GET: api/Parents/5
        [Authorize]
        [ResponseType(typeof(Parent))]
        public async Task<IHttpActionResult> GetParent(int id)
        {
            Parent parent = await db.Parents.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }

            return Ok(parent);
        }

        // PUT: api/Parents/5
        [Authorize]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutParent(int id, Parent parent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parent.ParentId)
            {
                return BadRequest();
            }

            db.Entry(parent).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentExists(id))
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

        // POST: api/Parents
        [ResponseType(typeof(Parent))]
        public async Task<IHttpActionResult> PostParent(Parent parent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Parents.Add(parent);

            try
            {
                CreateUserASP(parent.ParentEmail, parent.Password);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("Index"))
                {
                    return BadRequest("There are a record with this ID card.");
                }
                else
                {
                    return BadRequest(ex.Message);
                }

            }

            return CreatedAtRoute("DefaultApi", new { id = parent.ParentId }, parent);
        }

        // DELETE: api/Parents/5
        [Authorize]
        [ResponseType(typeof(Parent))]
        public async Task<IHttpActionResult> DeleteParent(int id)
        {
            Parent parent = await db.Parents.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }

            db.Parents.Remove(parent);
            await db.SaveChangesAsync();

            return Ok(parent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParentExists(int id)
        {
            return db.Parents.Count(e => e.ParentId == id) > 0;
        }

        private bool CreateUserASP(string email, string password)
        {
            var userContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

            var userASP = new ApplicationUser
            {
                Email = email,
                UserName = email,
            };

            var result = userManager.Create(userASP, password);
            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }
    }
}