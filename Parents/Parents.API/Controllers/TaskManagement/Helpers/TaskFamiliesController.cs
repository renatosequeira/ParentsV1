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
using Parents.Domain.TasksManagement.Helpers;

namespace Parents.API.Controllers.TaskManagement.Helpers
{
    [Authorize]
    public class TaskFamiliesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/TaskFamilies
        public IQueryable<TaskFamily> GetTaskFamilies()
        {
            return db.TaskFamilies;
        }

        // GET: api/TaskFamilies/5
        [ResponseType(typeof(TaskFamily))]
        public async Task<IHttpActionResult> GetTaskFamily(int id)
        {
            TaskFamily taskFamily = await db.TaskFamilies.FindAsync(id);
            if (taskFamily == null)
            {
                return NotFound();
            }

            return Ok(taskFamily);
        }

        // PUT: api/TaskFamilies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTaskFamily(int id, TaskFamily taskFamily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != taskFamily.TaskFamilyId)
            {
                return BadRequest();
            }

            db.Entry(taskFamily).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskFamilyExists(id))
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

        // POST: api/TaskFamilies
        [ResponseType(typeof(TaskFamily))]
        public async Task<IHttpActionResult> PostTaskFamily(TaskFamily taskFamily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TaskFamilies.Add(taskFamily);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = taskFamily.TaskFamilyId }, taskFamily);
        }

        // DELETE: api/TaskFamilies/5
        [ResponseType(typeof(TaskFamily))]
        public async Task<IHttpActionResult> DeleteTaskFamily(int id)
        {
            TaskFamily taskFamily = await db.TaskFamilies.FindAsync(id);
            if (taskFamily == null)
            {
                return NotFound();
            }

            db.TaskFamilies.Remove(taskFamily);
            await db.SaveChangesAsync();

            return Ok(taskFamily);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskFamilyExists(int id)
        {
            return db.TaskFamilies.Count(e => e.TaskFamilyId == id) > 0;
        }
    }
}