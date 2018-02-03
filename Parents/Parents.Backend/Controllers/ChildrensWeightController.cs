using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Parents.Domain;
using Parents.Domain.HealthManagement;
using Parents.Backend.Models;

namespace Parents.Backend.Controllers
{
    public class ChildrensWeightController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ChildrensWeight
        public async Task<ActionResult> Index()
        {
            return View(await db.ChildrenWeights.ToListAsync());
        }

        // GET: ChildrensWeight/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildrenWeight childrenWeight = await db.ChildrenWeights.FindAsync(id);
            if (childrenWeight == null)
            {
                return HttpNotFound();
            }
            return View(childrenWeight);
        }

        // GET: ChildrensWeight/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChildrensWeight/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ChildrenWeightId,ChildrenId,WeightVaue,WeightUnit")] ChildrenWeight childrenWeight)
        {
            if (ModelState.IsValid)
            {
                db.ChildrenWeights.Add(childrenWeight);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(childrenWeight);
        }

        // GET: ChildrensWeight/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildrenWeight childrenWeight = await db.ChildrenWeights.FindAsync(id);
            if (childrenWeight == null)
            {
                return HttpNotFound();
            }
            return View(childrenWeight);
        }

        // POST: ChildrensWeight/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ChildrenWeightId,ChildrenId,WeightVaue,WeightUnit")] ChildrenWeight childrenWeight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(childrenWeight).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(childrenWeight);
        }

        // GET: ChildrensWeight/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildrenWeight childrenWeight = await db.ChildrenWeights.FindAsync(id);
            if (childrenWeight == null)
            {
                return HttpNotFound();
            }
            return View(childrenWeight);
        }

        // POST: ChildrensWeight/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ChildrenWeight childrenWeight = await db.ChildrenWeights.FindAsync(id);
            db.ChildrenWeights.Remove(childrenWeight);
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
