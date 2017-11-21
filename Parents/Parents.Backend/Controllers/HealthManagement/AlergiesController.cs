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
    public class AlergiesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Alergies
        public async Task<ActionResult> Index()
        {
            var alergies = db.Alergies.Include(a => a.AlergyTypes);
            return View(await alergies.ToListAsync());
        }

        // GET: Alergies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alergy alergy = await db.Alergies.FindAsync(id);
            if (alergy == null)
            {
                return HttpNotFound();
            }
            return View(alergy);
        }

        // GET: Alergies/Create
        public ActionResult Create()
        {
            ViewBag.AlergyTypeId = new SelectList(db.AlergyTypes, "AlergyTypeId", "AlergyTypeDescriptiom");
            return View();
        }

        // POST: Alergies/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AlergiesView view)
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

                var alergy = ToAlergy(view);
                alergy.AlergyImage = pic;
              
                db.Alergies.Add(alergy);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AlergyTypeId = new SelectList(db.AlergyTypes, "AlergyTypeId", "AlergyTypeDescriptiom", view.AlergyTypeId);
            return View(view);
        }

        private Alergy ToAlergy(AlergiesView view)
        {
            return new Alergy
            {
                AlergyDescription = view.AlergyDescription,
                AlergyId = view.AlergyId,
                AlergyTypeId = view.AlergyTypeId,
                AlergyTypes = view.AlergyTypes,
                AlergyImage = view.AlergyImage
            };
        }

        // GET: Alergies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alergy alergy = await db.Alergies.FindAsync(id);
            if (alergy == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlergyTypeId = new SelectList(db.AlergyTypes, "AlergyTypeId", "AlergyTypeDescriptiom", alergy.AlergyTypeId);

            var view = ToView(alergy);

            return View(view);
        }

        private AlergiesView ToView(Alergy alergy)
        {
            return new AlergiesView
            {
                AlergyDescription = alergy.AlergyDescription,
                AlergyId = alergy.AlergyId,
                AlergyTypeId = alergy.AlergyTypeId,
                AlergyTypes = alergy.AlergyTypes,
                AlergyImage = alergy.AlergyImage
            };
        }

        // POST: Alergies/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AlergiesView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.AlergyImage;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var alergy = ToAlergy(view);
                alergy.AlergyImage = pic;


                db.Entry(alergy).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AlergyTypeId = new SelectList(db.AlergyTypes, "AlergyTypeId", "AlergyTypeDescriptiom", view.AlergyTypeId);
            return View(view);
        }

        // GET: Alergies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alergy alergy = await db.Alergies.FindAsync(id);
            if (alergy == null)
            {
                return HttpNotFound();
            }
            return View(alergy);
        }

        // POST: Alergies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Alergy alergy = await db.Alergies.FindAsync(id);
            db.Alergies.Remove(alergy);
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
