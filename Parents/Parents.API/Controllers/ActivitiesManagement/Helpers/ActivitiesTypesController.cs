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
using Parents.Domain.ActivitiesManagement.Helpers;
using Microsoft.AspNet.Identity;
using Parents.API.Models.ActivitiesManagement.Helpers;

namespace Parents.API.Controllers.ActivitiesManagement.Helpers
{
    [Authorize]
    public class ActivitiesTypesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ActivitiesTypes
        public async Task<IHttpActionResult> GetActivityTypes()
        {
            var userId = User.Identity.GetUserId();
            var activityTypes = await db.ActivityTypes.Where(at => at.userId == userId || at.userId == null).ToListAsync();

            var activityTypesResponse = new List<ActivityTypeResponse>();

            foreach (var type in activityTypes)
            {
                activityTypesResponse.Add(new ActivityTypeResponse
                {
                    ActivityTypeDescription = type.ActivityTypeDescription,
                    ActivityTypeId = type.ActivityTypeId,
                    ActivityTypePrivacy = type.ActivityTypePrivacy,
                    userId = type.userId
                });
            }

            return Ok(activityTypesResponse);
        }

        // GET: api/ActivitiesTypes/5
        [ResponseType(typeof(ActivityType))]
        public async Task<IHttpActionResult> GetActivityType(int id)
        {
            ActivityType activityType = await db.ActivityTypes.FindAsync(id);
            if (activityType == null)
            {
                return NotFound();
            }

            return Ok(activityType);
        }

        // PUT: api/ActivitiesTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutActivityType(int id, ActivityType activityType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activityType.ActivityTypeId)
            {
                return BadRequest();
            }

            db.Entry(activityType).State = EntityState.Modified;

            //START - CHECK DATABASE FOR EXISTING ACTIVITY FAMILIES

            var userId = User.Identity.GetUserId();
            var enteredDescription = activityType.ActivityTypeDescription;
            var IsPrivate = activityType.ActivityTypePrivacy;

            //ja sabemos que é privada

            //verificar se item existe na bd pública
            //publico
            object databasePrivacyForCurrentItem = db.ActivityTypes.Where(
                b => b.ActivityTypeDescription == enteredDescription &&
                b.ActivityTypePrivacy == false).FirstOrDefault();

            //privado
            object userDatabaseDescription = db.ActivityTypes.Where(
                b => b.ActivityTypeDescription == enteredDescription &&
                b.ActivityTypePrivacy == true &&
                b.userId == userId).FirstOrDefault();

            if (!IsPrivate)
            {
                if (databasePrivacyForCurrentItem != null)
                {
                    return BadRequest("There is a activity type with this description in Database already. Registry can't be added");
                }
                else
                {
                    if (IsPrivate != true)
                    {
                        activityType.ActivityTypePrivacy = IsPrivate;
                        activityType.userId = null;
                    }
                    else
                    {
                        return BadRequest("If you want to make Type public, please change it before proceeding.");
                    }
                }

            }
            else
            {
                if (userDatabaseDescription != null)
                {
                    return BadRequest("There is a activity type for this user already. Registry can't be added");
                }
                else
                {
                    if (IsPrivate == true)
                    {
                        activityType.ActivityTypePrivacy = IsPrivate;
                        activityType.userId = userId;
                    }
                    else
                    {
                        return BadRequest("If you want to make Type private, please change it before proceeding.");
                    }
                }
            }

            //END - CHECK DATABASE FOR EXISTING ACTIVITY FAMILIES

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityTypeExists(id))
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

        // POST: api/ActivitiesTypes
        [ResponseType(typeof(ActivityType))]
        public async Task<IHttpActionResult> PostActivityType(ActivityType activityType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //START - CHECK DATABASE FOR EXISTING ACTIVITY FAMILIES
            var userId = User.Identity.GetUserId();
            var enteredDescription = activityType.ActivityTypeDescription;
            var IsPrivate = activityType.ActivityTypePrivacy;

            //publico
            object databasePrivacyForCurrentItem = db.ActivityTypes.Where(
                b => b.ActivityTypeDescription == enteredDescription &&
                b.ActivityTypePrivacy == false).FirstOrDefault();

            //privado
            object userDatabaseDescription = db.ActivityTypes.Where(
                b => b.ActivityTypeDescription == enteredDescription &&
                b.ActivityTypePrivacy == true &&
                b.userId == userId).FirstOrDefault();

            //verifica se é privado ou publico
            if (IsPrivate)
            {
                if (userDatabaseDescription != null)
                {
                    return BadRequest("This user already has this item in Database. Registry can't be added");
                }
            }
            else
            {
                if (databasePrivacyForCurrentItem != null) //se existir na base de dados
                {

                    return BadRequest("There is a activity type with this description in Database already. Registry can't be added");

                }
            }

            //END - CHECK DATABASE FOR EXISTING ACTIVITY FAMILIES

            db.ActivityTypes.Add(activityType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = activityType.ActivityTypeId }, activityType);
        }

        // DELETE: api/ActivitiesTypes/5
        [ResponseType(typeof(ActivityType))]
        public async Task<IHttpActionResult> DeleteActivityType(int id)
        {
            ActivityType activityType = await db.ActivityTypes.FindAsync(id);
            if (activityType == null)
            {
                return NotFound();
            }

            db.ActivityTypes.Remove(activityType);
            await db.SaveChangesAsync();

            return Ok(activityType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActivityTypeExists(int id)
        {
            return db.ActivityTypes.Count(e => e.ActivityTypeId == id) > 0;
        }
    }
}