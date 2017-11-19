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
using Parents.Domain.TasksManagement;

namespace Parents.API.Controllers.TaskManagement
{
    public class TaskFunctionsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/TaskFunctions
        public IQueryable<TaskFunction> GetTasks()
        {
            return db.Tasks;
        }

        // GET: api/TaskFunctions/5
        [ResponseType(typeof(TaskFunction))]
        public async Task<IHttpActionResult> GetTaskFunction(int id)
        {
            TaskFunction taskFunction = await db.Tasks.FindAsync(id);
            if (taskFunction == null)
            {
                return NotFound();
            }

            return Ok(taskFunction);
        }

        // PUT: api/TaskFunctions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTaskFunction(int id, TaskFunction taskFunction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != taskFunction.TaskId)
            {
                return BadRequest();
            }

            db.Entry(taskFunction).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskFunctionExists(id))
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

        // POST: api/TaskFunctions
        [ResponseType(typeof(TaskFunction))]
        public async Task<IHttpActionResult> PostTaskFunction(TaskFunction taskFunction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tasks.Add(taskFunction);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = taskFunction.TaskId }, taskFunction);
        }

        // DELETE: api/TaskFunctions/5
        [ResponseType(typeof(TaskFunction))]
        public async Task<IHttpActionResult> DeleteTaskFunction(int id)
        {
            TaskFunction taskFunction = await db.Tasks.FindAsync(id);
            if (taskFunction == null)
            {
                return NotFound();
            }

            db.Tasks.Remove(taskFunction);
            await db.SaveChangesAsync();

            return Ok(taskFunction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskFunctionExists(int id)
        {
            return db.Tasks.Count(e => e.TaskId == id) > 0;
        }
    }
}