using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Parents.Domain;
using Parents.Domain.ActivitiesManagement.Helpers;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using Parents.API.Models.ActivitiesManagement.Helpers;
using System;

namespace Parents.API.Controllers.ActivitiesManagement.Helpers
{
    [Authorize]
    public class ActivitiesFamilyController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ActivitiesFamily
        public async Task<IHttpActionResult> GetActivityFamilies()
        {
            var userId = User.Identity.GetUserId();
            //var activityFamilies = await db.ActivityFamilies.ToListAsync();
            var activityFamilies = await db.ActivityFamilies.Where(family => family.userId == userId || family.userId == null).ToListAsync();

            var activityFamiliesResponse = new List<ActivityFamilyResponse>();

            foreach (var activity in activityFamilies)
            {

                    activityFamiliesResponse.Add(new ActivityFamilyResponse
                    {
                        ActivityFamilyId = activity.ActivityFamilyId,
                        ActivityFamilyDescription = activity.ActivityFamilyDescription,
                        userId = activity.userId,
                        Privacy = activity.Privacy
                    });

            }

            return Ok(activityFamiliesResponse);
        }

        // GET: api/ActivitiesFamily/5
        [ResponseType(typeof(ActivityFamily))]
        public async Task<IHttpActionResult> GetActivityFamily(int id)
        {
            ActivityFamily activityFamily = await db.ActivityFamilies.FindAsync(id);
            if (activityFamily == null)
            {
                return NotFound();
            }

            return Ok(activityFamily);
        }

        // PUT: api/ActivitiesFamily/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutActivityFamily(int id, ActivityFamily activityFamily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activityFamily.ActivityFamilyId)
            {
                return BadRequest();
            }

            db.Entry(activityFamily).State = EntityState.Modified;

            //START - CHECK DATABASE FOR EXISTING ACTIVITY FAMILIES

            var userId = User.Identity.GetUserId();
            var enteredDescription = activityFamily.ActivityFamilyDescription;
            var IsPrivate = activityFamily.Privacy;

            //ja sabemos que é privada

            //verificar se item existe na bd pública
            //publico
            object databasePrivacyForCurrentItem = db.ActivityFamilies.Where(
                b => b.ActivityFamilyDescription == enteredDescription &&
                b.Privacy == false).FirstOrDefault();

            //privado
            //object userDatabaseDescription = db.ActivityFamilies.Where(
            //    b => b.ActivityFamilyDescription == enteredDescription &&
            //    b.Privacy == true &&
            //    b.userId == userId).FirstOrDefault();

            if (databasePrivacyForCurrentItem != null)
            {
                return BadRequest("There is a activity family with this description in Database already. Registry can't be added");
            }
            else
            {
                if(IsPrivate != true)
                {
                    activityFamily.Privacy = IsPrivate;
                    activityFamily.userId = null;
                }
                else
                {
                    return BadRequest("If you want to make Family public, please change it before proceeding.");
                }
            }

            //END - CHECK DATABASE FOR EXISTING ACTIVITY FAMILIES


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
                    return BadRequest("There is a activity family with this description in Database already. Registry can't be added");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ActivitiesFamily
        [ResponseType(typeof(ActivityFamily))]
        public async Task<IHttpActionResult> PostActivityFamily(ActivityFamily activityFamily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool privateActivity = activityFamily.Privacy;
            string userId = User.Identity.GetUserId();

            if (privateActivity)
            {
                activityFamily.userId = userId;
            }
            else
            {
                activityFamily.userId = null;
            }


            //START - CHECK DATABASE FOR EXISTING ACTIVITY FAMILIES

            var enteredDescription = activityFamily.ActivityFamilyDescription;
            var IsPrivate = activityFamily.Privacy;

            //publico
            object databasePrivacyForCurrentItem = db.ActivityFamilies.Where(
                b => b.ActivityFamilyDescription == enteredDescription &&
                b.Privacy == false).FirstOrDefault();

            //privado
            object userDatabaseDescription = db.ActivityFamilies.Where(
                b => b.ActivityFamilyDescription == enteredDescription &&
                b.Privacy == true &&
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

                    return BadRequest("There is a activity family with this description in Database already. Registry can't be added");

                }
            }

            //END - CHECK DATABASE FOR EXISTING ACTIVITY FAMILIES

            db.ActivityFamilies.Add(activityFamily);

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
                    return BadRequest("There is a activity family with this description in Database already. Registry can't be added");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = activityFamily.ActivityFamilyId }, activityFamily);
        }

        private void CheckDatabaseForExistingActivityFamilies()
        {
            throw new NotImplementedException();
        }

        // DELETE: api/ActivitiesFamily/5
        [ResponseType(typeof(ActivityFamily))]
        public async Task<IHttpActionResult> DeleteActivityFamily(int id)
        {
            ActivityFamily activityFamily = await db.ActivityFamilies.FindAsync(id);
            if (activityFamily == null)
            {
                return NotFound();
            }

            db.ActivityFamilies.Remove(activityFamily);
            await db.SaveChangesAsync();

            return Ok(activityFamily);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActivityFamilyExists(int id)
        {
            return db.ActivityFamilies.Count(e => e.ActivityFamilyId == id) > 0;
        }
    }
}