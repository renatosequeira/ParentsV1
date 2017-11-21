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
using Parents.Backend.Models.HealthManagement;
using Parents.Backend.Helpers;

namespace Parents.Backend.Controllers.HealthManagement
{
    [Authorize]
    public class PhysicalCharacteristicsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: PhysicalCharacteristics
        public async Task<ActionResult> Index()
        {
            var physicalCharacteristics = db.PhysicalCharacteristics.Include(p => p.HumanBodyAreas).Include(p => p.PhysicalCharacteristicType);
            return View(await physicalCharacteristics.ToListAsync());
        }

        // GET: PhysicalCharacteristics/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhysicalCharacteristic physicalCharacteristic = await db.PhysicalCharacteristics.FindAsync(id);
            if (physicalCharacteristic == null)
            {
                return HttpNotFound();
            }
            return View(physicalCharacteristic);
        }

        // GET: PhysicalCharacteristics/Create
        public ActionResult Create()
        {
            ViewBag.HumanBodyAreaId = new SelectList(db.HumanBodyAreas, "HumanBodyAreaId", "HumanBodyAreaDescription");
            ViewBag.PhysicalCharacteristicTypeId = new SelectList(db.PhysicalCharacteristicTypes, "PhysicalCharacteristicTypeId", "PhysicalCharacteristicTypeDescription");
            return View();
        }

        // POST: PhysicalCharacteristics/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PhisicalCharacteristicsView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var physicalCharacteristic = ToPhysicalCharacteristic(view);
                physicalCharacteristic.PhysicalCharacteristicImage = pic;

                db.PhysicalCharacteristics.Add(physicalCharacteristic);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.HumanBodyAreaId = new SelectList(db.HumanBodyAreas, "HumanBodyAreaId", "HumanBodyAreaDescription", view.HumanBodyAreaId);
            ViewBag.PhysicalCharacteristicTypeId = new SelectList(db.PhysicalCharacteristicTypes, "PhysicalCharacteristicTypeId", "PhysicalCharacteristicTypeDescription", view.PhysicalCharacteristicTypeId);
            return View(view);
        }

        private PhysicalCharacteristic ToPhysicalCharacteristic(PhisicalCharacteristicsView view)
        {
            return new PhysicalCharacteristic
            {
               HumanBodyAreaId = view.HumanBodyAreaId,
               HumanBodyAreas = view.HumanBodyAreas,
               PhysicalCharacteristicDescription = view.PhysicalCharacteristicDescription,
               PhysicalCharacteristicId = view.PhysicalCharacteristicId,
               PhysicalCharacteristicType = view.PhysicalCharacteristicType,
               PhysicalCharacteristicTypeId = view.PhysicalCharacteristicTypeId,
               PhysicalCharacteristicImage = view.PhysicalCharacteristicImage
            };
        }

        // GET: PhysicalCharacteristics/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhysicalCharacteristic physicalCharacteristic = await db.PhysicalCharacteristics.FindAsync(id);
            if (physicalCharacteristic == null)
            {
                return HttpNotFound();
            }
            ViewBag.HumanBodyAreaId = new SelectList(db.HumanBodyAreas, "HumanBodyAreaId", "HumanBodyAreaDescription", physicalCharacteristic.HumanBodyAreaId);
            ViewBag.PhysicalCharacteristicTypeId = new SelectList(db.PhysicalCharacteristicTypes, "PhysicalCharacteristicTypeId", "PhysicalCharacteristicTypeDescription", physicalCharacteristic.PhysicalCharacteristicTypeId);

            var view = ToView(physicalCharacteristic);

            return View(view);
        }

        private PhisicalCharacteristicsView ToView(PhysicalCharacteristic physicalCharacteristic)
        {
            return new PhisicalCharacteristicsView
            {
                HumanBodyAreaId = physicalCharacteristic.HumanBodyAreaId,
                HumanBodyAreas = physicalCharacteristic.HumanBodyAreas,
                PhysicalCharacteristicDescription = physicalCharacteristic.PhysicalCharacteristicDescription,
                PhysicalCharacteristicId = physicalCharacteristic.PhysicalCharacteristicId,
                PhysicalCharacteristicType = physicalCharacteristic.PhysicalCharacteristicType,
                PhysicalCharacteristicTypeId = physicalCharacteristic.PhysicalCharacteristicTypeId,
                PhysicalCharacteristicImage = physicalCharacteristic.PhysicalCharacteristicImage
            };
        }

        // POST: PhysicalCharacteristics/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PhisicalCharacteristicsView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.PhysicalCharacteristicImage;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var physicalCharacteristic = ToPhysicalCharacteristic(view);
                physicalCharacteristic.PhysicalCharacteristicImage = pic;


                db.Entry(physicalCharacteristic).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HumanBodyAreaId = new SelectList(db.HumanBodyAreas, "HumanBodyAreaId", "HumanBodyAreaDescription", view.HumanBodyAreaId);
            ViewBag.PhysicalCharacteristicTypeId = new SelectList(db.PhysicalCharacteristicTypes, "PhysicalCharacteristicTypeId", "PhysicalCharacteristicTypeDescription", view.PhysicalCharacteristicTypeId);
            return View(view);
        }

        // GET: PhysicalCharacteristics/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhysicalCharacteristic physicalCharacteristic = await db.PhysicalCharacteristics.FindAsync(id);
            if (physicalCharacteristic == null)
            {
                return HttpNotFound();
            }
            return View(physicalCharacteristic);
        }

        // POST: PhysicalCharacteristics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PhysicalCharacteristic physicalCharacteristic = await db.PhysicalCharacteristics.FindAsync(id);
            db.PhysicalCharacteristics.Remove(physicalCharacteristic);
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
