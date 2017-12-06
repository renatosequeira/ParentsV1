using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Parents.Domain.ActivitiesManagement.Helpers;
using Parents.Backend.Models;
using Microsoft.AspNet.Identity;

namespace Parents.Backend.Controllers.ActivitiesManagement.Helpers
{
    [Authorize]
    public class ActivitiesInstitutionTypeController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ActivitiesInstitutionType
        public async Task<ActionResult> Index()
        {
            return View(await db.ActivityInstitutionTypes.ToListAsync());
        }

        // GET: ActivitiesInstitutionType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityInstitutionType activityInstitutionType = await db.ActivityInstitutionTypes.FindAsync(id);
            if (activityInstitutionType == null)
            {
                return HttpNotFound();
            }
            return View(activityInstitutionType);
        }

        // GET: ActivitiesInstitutionType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivitiesInstitutionType/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ActivityInstitutionType activityInstitutionType)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                activityInstitutionType.userId = userId;

                db.ActivityInstitutionTypes.Add(activityInstitutionType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(activityInstitutionType);
        }

        // GET: ActivitiesInstitutionType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityInstitutionType activityInstitutionType = await db.ActivityInstitutionTypes.FindAsync(id);
            if (activityInstitutionType == null)
            {
                return HttpNotFound();
            }
            return View(activityInstitutionType);
        }

        // POST: ActivitiesInstitutionType/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ActivityInstitutionType activityInstitutionType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityInstitutionType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(activityInstitutionType);
        }

        // GET: ActivitiesInstitutionType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityInstitutionType activityInstitutionType = await db.ActivityInstitutionTypes.FindAsync(id);
            if (activityInstitutionType == null)
            {
                return HttpNotFound();
            }
            return View(activityInstitutionType);
        }

        // POST: ActivitiesInstitutionType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ActivityInstitutionType activityInstitutionType = await db.ActivityInstitutionTypes.FindAsync(id);
            db.ActivityInstitutionTypes.Remove(activityInstitutionType);
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
