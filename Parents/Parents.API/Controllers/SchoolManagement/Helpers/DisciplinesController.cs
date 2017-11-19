﻿using System;
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
using Parents.Domain.SchoolManagement.Helpers;

namespace Parents.API.Controllers.SchoolManagement.Helpers
{
    public class DisciplinesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Disciplines
        public IQueryable<Discipline> GetDisciplines()
        {
            return db.Disciplines;
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

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisciplineExists(id))
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

        // POST: api/Disciplines
        [ResponseType(typeof(Discipline))]
        public async Task<IHttpActionResult> PostDiscipline(Discipline discipline)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Disciplines.Add(discipline);
            await db.SaveChangesAsync();

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