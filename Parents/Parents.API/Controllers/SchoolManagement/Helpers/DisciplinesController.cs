namespace Parents.API.Controllers.SchoolManagement.Helpers
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Parents.Domain;
    using Parents.Domain.SchoolManagement.Helpers;
    using System.Collections.Generic;
    using Parents.API.Models.SchoolManagement.Helpers;
    using System;
    using Microsoft.AspNet.Identity;

    [Authorize]
    public class DisciplinesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Disciplines
        public async Task<IHttpActionResult> GetDisciplines()
        {
            var disciplines = await db.Disciplines.ToListAsync();

            var disciplinesResponse = new List<DisciplineResponse>();
            

            foreach (var discipline in disciplines)
            {
                disciplinesResponse.Add(new DisciplineResponse
                {
                    DisciplineId = discipline.DisciplineId,
                    DisciplineDescription = discipline.DisciplineDescription,
                    DisciplineRemarks = discipline.DisciplineRemarks,
                    userId = discipline.userId
                });
            }
            return Ok(disciplinesResponse);
        }

        // GET: api/Disciplines/5
        [ResponseType(typeof(Discipline))]
        public async Task<IHttpActionResult> GetDiscipline(int id)
        {
            Discipline discipline = await db.Disciplines.FindAsync(id);
            if (discipline == null)
            {
                return NotFound();
            }

            return Ok(discipline);
        }

        // PUT: api/Disciplines/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDiscipline(int id, Discipline discipline)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != discipline.DisciplineId)
            {
                return BadRequest();
            }

            db.Entry(discipline).State = EntityState.Modified;

            //try
            //{
            //    await db.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!DisciplineExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("Index"))
                {
                    return BadRequest("There is a discipline with this description in Database already. Registry can't be added");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Disciplines
        [ResponseType(typeof(Discipline))]
        public async Task<IHttpActionResult> PostDiscipline(Discipline discipline)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = User.Identity.GetUserId();
            discipline.userId = userId;

            db.Disciplines.Add(discipline);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("Index"))
                {
                    return BadRequest("There is a discipline with this description in Database already. Registry can't be added");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = discipline.DisciplineId }, discipline);
        }

        // DELETE: api/Disciplines/5
        [ResponseType(typeof(Discipline))]
        public async Task<IHttpActionResult> DeleteDiscipline(int id)
        {
            Discipline discipline = await db.Disciplines.FindAsync(id);
            if (discipline == null)
            {
                return NotFound();
            }

            db.Disciplines.Remove(discipline);
            await db.SaveChangesAsync();

            return Ok(discipline);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DisciplineExists(int id)
        {
            return db.Disciplines.Count(e => e.DisciplineId == id) > 0;
        }
    }
}