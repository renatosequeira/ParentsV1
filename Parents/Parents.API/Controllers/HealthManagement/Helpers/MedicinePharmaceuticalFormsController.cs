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
using Parents.Domain.HealthManagement.Categories;

namespace Parents.API.Controllers.HealthManagement.Helpers
{
    public class MedicinePharmaceuticalFormsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/MedicinePharmaceuticalForms
        public IQueryable<MedicinePharmaceuticalForm> GetMedicinePharmaceuticalForms()
        {
            return db.MedicinePharmaceuticalForms;
        }

        // GET: api/MedicinePharmaceuticalForms/5
        [ResponseType(typeof(MedicinePharmaceuticalForm))]
        public async Task<IHttpActionResult> GetMedicinePharmaceuticalForm(int id)
        {
            MedicinePharmaceuticalForm medicinePharmaceuticalForm = await db.MedicinePharmaceuticalForms.FindAsync(id);
            if (medicinePharmaceuticalForm == null)
            {
                return NotFound();
            }

            return Ok(medicinePharmaceuticalForm);
        }

        // PUT: api/MedicinePharmaceuticalForms/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMedicinePharmaceuticalForm(int id, MedicinePharmaceuticalForm medicinePharmaceuticalForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicinePharmaceuticalForm.MedicinePharmaceuticalFormId)
            {
                return BadRequest();
            }

            db.Entry(medicinePharmaceuticalForm).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicinePharmaceuticalFormExists(id))
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

        // POST: api/MedicinePharmaceuticalForms
        [ResponseType(typeof(MedicinePharmaceuticalForm))]
        public async Task<IHttpActionResult> PostMedicinePharmaceuticalForm(MedicinePharmaceuticalForm medicinePharmaceuticalForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MedicinePharmaceuticalForms.Add(medicinePharmaceuticalForm);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = medicinePharmaceuticalForm.MedicinePharmaceuticalFormId }, medicinePharmaceuticalForm);
        }

        // DELETE: api/MedicinePharmaceuticalForms/5
        [ResponseType(typeof(MedicinePharmaceuticalForm))]
        public async Task<IHttpActionResult> DeleteMedicinePharmaceuticalForm(int id)
        {
            MedicinePharmaceuticalForm medicinePharmaceuticalForm = await db.MedicinePharmaceuticalForms.FindAsync(id);
            if (medicinePharmaceuticalForm == null)
            {
                return NotFound();
            }

            db.MedicinePharmaceuticalForms.Remove(medicinePharmaceuticalForm);
            await db.SaveChangesAsync();

            return Ok(medicinePharmaceuticalForm);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MedicinePharmaceuticalFormExists(int id)
        {
            return db.MedicinePharmaceuticalForms.Count(e => e.MedicinePharmaceuticalFormId == id) > 0;
        }
    }
}