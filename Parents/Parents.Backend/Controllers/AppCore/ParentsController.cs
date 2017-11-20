using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Parents.Domain;
using Parents.Backend.Models;

namespace Parents.Backend.Controllers.AppCore
{
    [Authorize]
    public class ParentsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Parents
        public async Task<ActionResult> Index()
        {
            var parents = db.Parents.Include(p => p.ParentalType);
            return View(await parents.ToListAsync());
        }

        // GET: Parents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = await db.Parents.FindAsync(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // GET: Parents/Create
        public ActionResult Create()
        {
            ViewBag.ParentalTypeId = new SelectList(db.ParentalTypes, "ParentalTypeId", "ParentalTypeDescription");
            return View();
        }

        // POST: Parents/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Parent parent)
        {
            if (ModelState.IsValid)
            {
                db.Parents.Add(parent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ParentalTypeId = new SelectList(db.ParentalTypes, "ParentalTypeId", "ParentalTypeDescription", parent.ParentalTypeId);
            return View(parent);
        }

        // GET: Parents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = await db.Parents.FindAsync(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentalTypeId = new SelectList(db.ParentalTypes, "ParentalTypeId", "ParentalTypeDescription", parent.ParentalTypeId);
            return View(parent);
        }

        // POST: Parents/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Parent parent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ParentalTypeId = new SelectList(db.ParentalTypes, "ParentalTypeId", "ParentalTypeDescription", parent.ParentalTypeId);
            return View(parent);
        }

        // GET: Parents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = await db.Parents.FindAsync(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // POST: Parents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Parent parent = await db.Parents.FindAsync(id);
            db.Parents.Remove(parent);
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
