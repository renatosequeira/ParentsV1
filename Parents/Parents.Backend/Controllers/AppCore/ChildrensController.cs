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
    using Parents.Backend.Models.HealthManagement.Helpers;

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
            var children = db.Children.Include(c => c.BloodInformation).Include(c => c.ParentsMatrimonialState);
            return View(await children.ToListAsync());
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


            ViewBag.BloodInformation = new SelectList(db.BloodInformations, "BoodInformationId", "BloodInformationDescription");
            //ViewBag.BoodInformationId = new SelectList(GetBloodInformation().ToList(), "BoodInformationId", "BloodInformationDescription");

            
            ViewBag.MatrimonialStateId = new SelectList(db.MatrimonialStates, "MatrimonialStateId", "MatrimonialStateDescription");

            return View();
        }

        // POST: Childrens/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ChildrenId,ParentId,BoodInformationId,MatrimonialStateId,ChildrenFirstName,ChildrenMiddleName,ChildrenLastName,ChildrenIdentityCard,ChildrenBirthDate,ChildrenFamilyDoctor,ChildrenEmail,ChildrenMobile,ChildrenAddress,CurrentSchool,SchoolContact,FatherId,MotherId")] Children children)
        {
            if (ModelState.IsValid)
            {
                db.Children.Add(children);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BloodInformation = new SelectList(db.BloodInformations, "BoodInformationId", "BloodInformationDescription", 3);
            ViewBag.MatrimonialStateId = new SelectList(db.MatrimonialStates, "MatrimonialStateId", "MatrimonialStateDescription", children.MatrimonialStateId);
            return View(children);
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
            ViewBag.MatrimonialStateId = new SelectList(db.MatrimonialStates, "MatrimonialStateId", "MatrimonialStateDescription", children.MatrimonialStateId);
            return View(children);
        }

        // POST: Childrens/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ChildrenId,ParentId,BoodInformationId,MatrimonialStateId,ChildrenFirstName,ChildrenMiddleName,ChildrenLastName,ChildrenIdentityCard,ChildrenBirthDate,ChildrenFamilyDoctor,ChildrenEmail,ChildrenMobile,ChildrenAddress,CurrentSchool,SchoolContact,FatherId,MotherId")] Children children)
        {
            if (ModelState.IsValid)
            {
                db.Entry(children).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BoodInformationId = new SelectList(db.BloodInformations, "BoodInformationId", "BloodInformationDescription", children.BoodInformationId);
            ViewBag.MatrimonialStateId = new SelectList(db.MatrimonialStates, "MatrimonialStateId", "MatrimonialStateDescription", children.MatrimonialStateId);
            return View(children);
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
