namespace Parents.API.Controllers.ActivitiesManagement.Helpers
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Parents.Domain;
    using Parents.Domain.ActivitiesManagement.Helpers;
    using System.Collections.Generic;
    using Parents.API.Models.ActivitiesManagement.Helpers;
    using System;
    using Microsoft.AspNet.Identity;

    [Authorize]
    public class ActivitiesInstitutionTypesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ActivitiesInstitutionTypes
        public async Task<IHttpActionResult> GetActivityInstitutionTypes()
        {
            var activityInstitutionTypes = await db.ActivityInstitutionTypes.ToListAsync();

            var activityInstitutionTypesResponse = new List<ActivityInstitutionTypeResponse>();

            foreach (var activityIntitutionType in activityInstitutionTypes)
            {
                activityInstitutionTypesResponse.Add(new ActivityInstitutionTypeResponse
                {
                    ActivityInstitutionTypeDescription = activityIntitutionType.ActivityInstitutionTypeDescription,
                    userId = activityIntitutionType.userId,
                    ActivityInstitutionTypeId = activityIntitutionType.ActivityInstitutionTypeId
                });
            }

            return Ok(activityInstitutionTypesResponse);
        }

        // GET: api/ActivitiesInstitutionTypes/5
        [ResponseType(typeof(ActivityInstitutionType))]
        public async Task<IHttpActionResult> GetActivityInstitutionType(int id)
        {
            ActivityInstitutionType activityInstitutionType = await db.ActivityInstitutionTypes.FindAsync(id);
            if (activityInstitutionType == null)
            {
                return NotFound();
            }

            return Ok(activityInstitutionType);
        }

        // PUT: api/ActivitiesInstitutionTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutActivityInstitutionType(int id, ActivityInstitutionType activityInstitutionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activityInstitutionType.ActivityInstitutionTypeId)
            {
                return BadRequest();
            }

            db.Entry(activityInstitutionType).State = EntityState.Modified;

            string currentUser = User.Identity.GetUserId();
            string userId = activityInstitutionType.userId;

            if (currentUser == userId)
            {
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
                        return BadRequest("There activity institution type already exists. Please use search function or add another one!");
                    }
                    else
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
            else
            {
                return BadRequest("You are not the proprietary of this topic. Only owner can change it");
                
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ActivitiesInstitutionTypes
        [ResponseType(typeof(ActivityInstitutionType))]
        public async Task<IHttpActionResult> PostActivityInstitutionType(ActivityInstitutionType activityInstitutionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ActivityInstitutionTypes.Add(activityInstitutionType);
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
                    return BadRequest("There activity institution type already exists. Please use search function or add another one!");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = activityInstitutionType.ActivityInstitutionTypeId }, activityInstitutionType);
        }

        // DELETE: api/ActivitiesInstitutionTypes/5
        [ResponseType(typeof(ActivityInstitutionType))]
        public async Task<IHttpActionResult> DeleteActivityInstitutionType(int id)
        {
            ActivityInstitutionType activityInstitutionType = await db.ActivityInstitutionTypes.FindAsync(id);
            if (activityInstitutionType == null)
            {
                return NotFound();
            }

            db.ActivityInstitutionTypes.Remove(activityInstitutionType);
            await db.SaveChangesAsync();

            return Ok(activityInstitutionType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActivityInstitutionTypeExists(int id)
        {
            return db.ActivityInstitutionTypes.Count(e => e.ActivityInstitutionTypeId == id) > 0;
        }
    }
}