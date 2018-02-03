namespace Parents.API.Controllers.HealthManagement
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Microsoft.AspNet.Identity;
    using Parents.API.Models.HealthManagement;
    using Parents.Domain;
    using Parents.Domain.HealthManagement;

    [RoutePrefix("api/ChildrensWeight")]
    public class ChildrensWeightController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ChildrensWeight
        public async Task<IHttpActionResult> GetChildrenWeights()
        {
            var userId = User.Identity.GetUserId();

            var ChildrenWeight = await db.ChildrenWeights.ToListAsync();

            var ChildrensList = await db.Children.Where(chi => chi.FirstParentId == userId).ToListAsync();

            var childrenWeightResponse = new List<ChildrenWeightResponse>();
            
            foreach (var childrenWeight in ChildrenWeight)
            {
                foreach (var user in ChildrensList)
                {
                    if(user.ChildrenId == childrenWeight.ChildrenId)
                    {
                        childrenWeightResponse.Add(new ChildrenWeightResponse
                        {
                            ChildrenId = childrenWeight.ChildrenId,
                            ChildrenWeightId = childrenWeight.ChildrenWeightId,
                            WeightUnit = childrenWeight.WeightUnit,
                            WeightVaue = childrenWeight.WeightVaue,
                            RegistryDate = childrenWeight.RegistryDate
                        });
                    }
                }
                
            }

            return Ok(childrenWeightResponse);
        }

        // GET: api/ChildrensWeight/id
        [Route("WeightForSpecificChildren/{id}")]
        public async Task<IHttpActionResult> GetSpecificChildrenWeights(int id)
        {
            var userId = User.Identity.GetUserId();

            var ChildrenWeight = await db.ChildrenWeights.Where(wei => wei.ChildrenId == id).ToListAsync();

            var ChildrensList = await db.Children.Where(chi => chi.FirstParentId == userId).ToListAsync();

            var childrenWeightResponse = new List<ChildrenWeightResponse>();

            foreach (var childrenWeight in ChildrenWeight)
            {
                childrenWeightResponse.Add(new ChildrenWeightResponse
                {
                    ChildrenId = childrenWeight.ChildrenId,
                    ChildrenWeightId = childrenWeight.ChildrenWeightId,
                    WeightUnit = childrenWeight.WeightUnit,
                    WeightVaue = childrenWeight.WeightVaue,
                    RegistryDate = childrenWeight.RegistryDate
                });
            }

            return Ok(childrenWeightResponse);
        }

        // GET: api/ChildrensWeight/5
        [ResponseType(typeof(ChildrenWeight))]
        public async Task<IHttpActionResult> GetChildrenWeight(int id)
        {
            ChildrenWeight childrenWeight = await db.ChildrenWeights.FindAsync(id);
            if (childrenWeight == null)
            {
                return NotFound();
            }

            return Ok(childrenWeight);
        }

        // PUT: api/ChildrensWeight/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutChildrenWeight(int id, ChildrenWeight childrenWeight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != childrenWeight.ChildrenWeightId)
            {
                return BadRequest();
            }

            var _childrenWeight = ToChildrenWeight(childrenWeight);
            db.Entry(childrenWeight).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChildrenWeightExists(id))
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

        // POST: api/ChildrensWeight
        [ResponseType(typeof(ChildrenWeight))]
        public async Task<IHttpActionResult> PostChildrenWeight(ChildrenWeight childrenWeight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ChildrenWeights.Add(childrenWeight);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = childrenWeight.ChildrenWeightId }, childrenWeight);
        }

        private ChildrenWeight ToChildrenWeight(ChildrenWeight view)
        {
            string userId = User.Identity.GetUserId();
            var eventId = Guid.NewGuid().ToString();

            return new Domain.HealthManagement.ChildrenWeight
            {
                ChildrenId = view.ChildrenId,
                ChildrenWeightId = view.ChildrenWeightId,
                RegistryDate = view.RegistryDate,
                WeightUnit = view.WeightUnit,
                WeightVaue = view.WeightVaue
            };
        }

        // DELETE: api/ChildrensWeight/5
        [ResponseType(typeof(ChildrenWeight))]
        public async Task<IHttpActionResult> DeleteChildrenWeight(int id)
        {
            ChildrenWeight childrenWeight = await db.ChildrenWeights.FindAsync(id);
            if (childrenWeight == null)
            {
                return NotFound();
            }

            db.ChildrenWeights.Remove(childrenWeight);
            await db.SaveChangesAsync();

            return Ok(childrenWeight);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChildrenWeightExists(int id)
        {
            return db.ChildrenWeights.Count(e => e.ChildrenWeightId == id) > 0;
        }
    }
}