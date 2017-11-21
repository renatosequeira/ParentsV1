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
using Parents.Domain.ParentalManagement.Helpers;
using Parents.Backend.Models;
using Parents.Backend.Models.ParentalManagement;
using Parents.Backend.Helpers;

namespace Parents.Backend.Controllers.ParentalManagement
{
    [Authorize]
    public class ChildSupportPaymentsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ChildSupportPayments
        public async Task<ActionResult> Index()
        {
            return View(await db.ChildSupportPayments.ToListAsync());
        }

        // GET: ChildSupportPayments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildSupportPayment childSupportPayment = await db.ChildSupportPayments.FindAsync(id);
            if (childSupportPayment == null)
            {
                return HttpNotFound();
            }
            return View(childSupportPayment);
        }

        // GET: ChildSupportPayments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChildSupportPayments/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ChildSupportPaymentView view)
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

                var childSupportPayment = ToChildSupportPayment(view);
                childSupportPayment.ChildSupportPaymentImage = pic;

                db.ChildSupportPayments.Add(childSupportPayment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        private ChildSupportPayment ToChildSupportPayment(ChildSupportPaymentView view)
        {
            return new ChildSupportPayment
            {
                ChildSupportPaymentDate = view.ChildSupportPaymentDate,
                ChildSupportPaymentId = view.ChildSupportPaymentId,
                ChildSupportPaymentRemarks = view.ChildSupportPaymentRemarks,
                ChildSupportPaymentValue = view.ChildSupportPaymentValue,
                ChildSupportPaymentImage = view.ChildSupportPaymentImage
            };
        }

        // GET: ChildSupportPayments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildSupportPayment childSupportPayment = await db.ChildSupportPayments.FindAsync(id);
            if (childSupportPayment == null)
            {
                return HttpNotFound();
            }

            var view = ToView(childSupportPayment);

            return View(view);
        }

        private ChildSupportPaymentView ToView(ChildSupportPayment childSupportPayment)
        {
            return new ChildSupportPaymentView
            {
                ChildSupportPaymentDate = childSupportPayment.ChildSupportPaymentDate,
                ChildSupportPaymentId = childSupportPayment.ChildSupportPaymentId,
                ChildSupportPaymentRemarks = childSupportPayment.ChildSupportPaymentRemarks,
                ChildSupportPaymentValue = childSupportPayment.ChildSupportPaymentValue,
                ChildSupportPaymentImage = childSupportPayment.ChildSupportPaymentImage
            };
        }

        // POST: ChildSupportPayments/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ChildSupportPaymentView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.ChildSupportPaymentImage;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var childSupportPayment = ToChildSupportPayment(view);
                childSupportPayment.ChildSupportPaymentImage = pic;


                db.Entry(childSupportPayment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(view);
        }

        // GET: ChildSupportPayments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildSupportPayment childSupportPayment = await db.ChildSupportPayments.FindAsync(id);
            if (childSupportPayment == null)
            {
                return HttpNotFound();
            }
            return View(childSupportPayment);
        }

        // POST: ChildSupportPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ChildSupportPayment childSupportPayment = await db.ChildSupportPayments.FindAsync(id);
            db.ChildSupportPayments.Remove(childSupportPayment);
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
