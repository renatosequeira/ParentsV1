namespace Parents.Backend.Controllers.ActivitiesManagement.Helpers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using Parents.Domain.ActivitiesManagement.Helpers;
    using Parents.Backend.Models;
    using Microsoft.AspNet.Identity;

    [Authorize]
    public class ActivitiesPeriodicityController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ActivitiesPeriodicity
        public async Task<ActionResult> Index()
        {
            return View(await db.ActivityPeriodicities.ToListAsync());
        }

        // GET: ActivitiesPeriodicity/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityPeriodicity activityPeriodicity = await db.ActivityPeriodicities.FindAsync(id);
            if (activityPeriodicity == null)
            {
                return HttpNotFound();
            }
            return View(activityPeriodicity);
        }

        // GET: ActivitiesPeriodicity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivitiesPeriodicity/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ActivityPeriodicity activityPeriodicity)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                activityPeriodicity.userId = userId;

                db.ActivityPeriodicities.Add(activityPeriodicity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(activityPeriodicity);
        }

        // GET: ActivitiesPeriodicity/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityPeriodicity activityPeriodicity = await db.ActivityPeriodicities.FindAsync(id);
            if (activityPeriodicity == null)
            {
                return HttpNotFound();
            }
            return View(activityPeriodicity);
        }

        // POST: ActivitiesPeriodicity/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ActivityPeriodicity activityPeriodicity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityPeriodicity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(activityPeriodicity);
        }

        // GET: ActivitiesPeriodicity/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityPeriodicity activityPeriodicity = await db.ActivityPeriodicities.FindAsync(id);
            if (activityPeriodicity == null)
            {
                return HttpNotFound();
            }
            return View(activityPeriodicity);
        }

        // POST: ActivitiesPeriodicity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ActivityPeriodicity activityPeriodicity = await db.ActivityPeriodicities.FindAsync(id);
            db.ActivityPeriodicities.Remove(activityPeriodicity);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
