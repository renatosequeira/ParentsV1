namespace Parents.Backend.Controllers.AppCore
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using Parents.Domain;
    using Parents.Backend.Models;
    using Parents.Domain.HealthManagement.Categories;
    using System.Collections.Generic;
    using System.Linq;
    using Parents.Backend.Models.AppCore;
    using Parents.Backend.Helpers;
    using Microsoft.AspNet.Identity;

    [Authorize]
    public class ChildrensController : Controller
    {
        private DataContextLocal db = new DataContextLocal();


        public List<BloodInformation> GetBloodInformation()
        {
            var customers = db.BloodInformations.ToList();
            var response = new List<BloodInformation>();

            foreach (var cust in customers)
            {
                response.Add(new BloodInformation()
                {
                    BoodInformationId = cust.BoodInformationId,
                    // all other properties
                    BloodInformationDescription = cust.BloodInformationDescription,
                    BloodInformationRemarks = cust.BloodInformationRemarks
                });
            }
            return response; //Here CityName is not virtual so will not cause exception. 
        }

        // GET: Childrens
        public async Task<ActionResult> Index()
        {
            string userId = User.Identity.GetUserId();
            var children = db.Children.Include(c => c.BloodInformation);
            return View(await children.Where(child => child.FirstParentId == userId || child.SecondParendId == userId).ToListAsync());
        }

        // GET: Childrens/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Children children = await db.Children.FindAsync(id);
            if (children == null)
            {
                return HttpNotFound();
            }
            return View(children);
        }

        // GET: Childrens/Create
        public ActionResult Create()
        {

            ViewBag.BoodInformationId = new SelectList(db.BloodInformations, "BoodInformationId", "BloodInformationDescription");
            ViewBag.MatrimonialStateId = new SelectList(db.MatrimonialStates, "MatrimonialStateId", "MatrimonialStateDescription");

            return View();
        }

        // POST: Childrens/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ChildrenView view)
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

                var children = ToChildren(view);
                children.ChildrenImage = pic;

                string parentId = User.Identity.GetUserId();
                children.FirstParentId = parentId;

                db.Children.Add(children);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BoodInformationId = new SelectList(db.BloodInformations, "BoodInformationId", "BloodInformationDescription", view.BoodInformationId);
            return View(view);
        }

        private Children ToChildren(ChildrenView view)
        {
            return new Children
            {
                BloodInformation = view.BloodInformation,
                BloodInformationDescription = view.BloodInformationDescription,
                BoodInformationId = view.BoodInformationId,
                ChildrenAddress = view.ChildrenAddress,
                ChildrenBirthDate = view.ChildrenBirthDate,
                ChildrenEmail = view.ChildrenEmail,
                ChildrenFamilyDoctor = view.ChildrenFamilyDoctor,
                ChildrenFirstName = view.ChildrenFirstName,
                ChildrenId = view.ChildrenId,
                ChildrenIdentityCard = view.ChildrenIdentityCard,
                //ChildrenImage = view.ChildrenImage,
                ChildrenLastName = view.ChildrenLastName,
                ChildrenMiddleName = view.ChildrenMiddleName,
                ChildrenMobile = view.ChildrenMobile,
                CurrentSchool = view.CurrentSchool,
                FirstParentId = view.FirstParentId,
                SecondParendId = view.SecondParendId,
                SchoolContact = view.SchoolContact,
                ChildrenSex = view.ChildrenSex,
                IsMale = view.IsMale,
                ChildWithHealthIssues = view.ChildWithHealthIssues
            };
        }

        // GET: Childrens/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Children children = await db.Children.FindAsync(id);
            if (children == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoodInformationId = new SelectList(db.BloodInformations, "BoodInformationId", "BloodInformationDescription", children.BoodInformationId);

            var view = ToView(children);

            return View(view);
        }

        private ChildrenView ToView(Children children)
        {
            return new ChildrenView
            {
                BloodInformation = children.BloodInformation,
                BloodInformationDescription = children.BloodInformationDescription,
                BoodInformationId = children.BoodInformationId,
                ChildrenAddress = children.ChildrenAddress,
                ChildrenBirthDate = children.ChildrenBirthDate,
                ChildrenEmail = children.ChildrenEmail,
                ChildrenFamilyDoctor = children.ChildrenFamilyDoctor,
                ChildrenFirstName = children.ChildrenFirstName,
                ChildrenId = children.ChildrenId,
                ChildrenIdentityCard = children.ChildrenIdentityCard,
                //ChildrenImage = children.ChildrenImage,
                ChildrenLastName = children.ChildrenLastName,
                ChildrenMiddleName = children.ChildrenMiddleName,
                ChildrenMobile = children.ChildrenMobile,
                CurrentSchool = children.CurrentSchool,
                FirstParentId = children.FirstParentId,
                SecondParendId = children.SecondParendId,
                SchoolContact = children.SchoolContact,
                ChildrenSex = children.ChildrenSex,
                IsMale = children.IsMale,
                ChildWithHealthIssues = children.ChildWithHealthIssues
            };
        }

        // POST: Childrens/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ChildrenView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.ChildrenImage;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var children = ToChildren(view);
                children.ChildrenImage = pic;


                db.Entry(children).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BoodInformationId = new SelectList(db.BloodInformations, "BoodInformationId", "BloodInformationDescription", view.BoodInformationId);
            return View(view);
        }

        // GET: Childrens/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Children children = await db.Children.FindAsync(id);
            if (children == null)
            {
                return HttpNotFound();
            }
            return View(children);
        }

        // POST: Childrens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Children children = await db.Children.FindAsync(id);
            db.Children.Remove(children);
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
