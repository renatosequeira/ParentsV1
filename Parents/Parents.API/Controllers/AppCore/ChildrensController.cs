namespace Parents.API.Controllers.AppCore
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Domain;
    using API.Models.AppCore;
    using Microsoft.AspNet.Identity;
    using System.IO;
    using Parents.API.Helpers;

    [Authorize]
    public class ChildrensController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Childrens
        public async Task<IHttpActionResult> GetChildren()
        {
            var userId = User.Identity.GetUserId();

            var childrens = await db.Children.Where(child => child.FirstParentId == userId ||
            child.SecondParendId == userId).ToListAsync();
            //var childrens = await db.Children.ToListAsync();
            var childrensResponse = new List<ChildrenResponse>();

            foreach (var children in childrens)
            {

                childrensResponse.Add(new ChildrenResponse
                {
                    BloodInformationDescription = children.BloodInformationDescription,
                    ChildrenAddress = children.ChildrenAddress,
                    ChildrenBirthDate = (DateTime)children.ChildrenBirthDate,
                    ChildrenEmail = children.ChildrenEmail,
                    ChildrenFamilyDoctor = children.ChildrenFamilyDoctor,
                    ChildrenFirstName = children.ChildrenFirstName,
                    ChildrenId = children.ChildrenId,
                    ChildrenIdentityCard = children.ChildrenIdentityCard,
                    ChildrenImage = children.ChildrenImage,
                    ChildrenLastName = children.ChildrenLastName,
                    ChildrenMiddleName = children.ChildrenMiddleName,
                    ChildrenMobile = children.ChildrenMobile,
                    CurrentSchool = children.CurrentSchool,
                    FirstParentId = children.FirstParentId,
                    SecondParentId = children.SecondParendId,
                    SchoolContact = children.SchoolContact,
                    ChildrenSex = children.ChildrenSex,
                });
            }

            return Ok(childrensResponse);
        }

        // GET: api/Childrens/5
        [ResponseType(typeof(Children))]
        public async Task<IHttpActionResult> GetChildren(int id)
        {
            Children children = await db.Children.FindAsync(id);
            if (children == null)
            {
                return NotFound();
            }

            

            return Ok(children);
        }

        // PUT: api/Childrens/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutChildren(int id, Children children)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != children.ChildrenId)
            {
                return BadRequest();
            }

            db.Entry(children).State = EntityState.Modified;

           

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
                    return BadRequest("This Identification Number is already registered to another children!");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Childrens
        [ResponseType(typeof(Children))]
        public async Task<IHttpActionResult> PostChildren(ChildrenRequest children)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (children.ImageArray != null && children.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(children.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                var folder = "~/Content/Images";
                var fullPath = string.Format("{0}/{1}", folder, file);
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    children.ChildrenImage = fullPath;
                }
            }

            var _children = ToChildren(children);
            
            string parentId = User.Identity.GetUserId();
            children.FirstParentId = parentId;

            db.Children.Add(_children);

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
                    return BadRequest("This Identification Number is already registered to another children!");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = children.ChildrenId }, _children);
        }

        private Children ToChildren(ChildrenRequest view)
        {
            string userId = User.Identity.GetUserId();

            return new Children
            {
                BloodInformationDescription = view.BloodInformationDescription,
                ChildrenAddress = view.ChildrenAddress,
                ChildrenBirthDate = view.ChildrenBirthDate,
                ChildrenEmail = view.ChildrenEmail,
                ChildrenFamilyDoctor = view.ChildrenFamilyDoctor,
                ChildrenFirstName = view.ChildrenFirstName,
                ChildrenIdentityCard = view.ChildrenIdentityCard,
                ChildrenImage = view.ChildrenImage,
                ChildrenLastName = view.ChildrenLastName,
                ChildrenMiddleName = view.ChildrenMiddleName,
                ChildrenMobile = view.ChildrenMobile,
                ChildrenSex = view.ChildrenSex,
                CurrentSchool = view.CurrentSchool,
                SchoolContact = view.SchoolContact,
                FirstParentId = userId,
                SecondParendId = view.SecondParendId
            };
        }

        // DELETE: api/Childrens/5
        [ResponseType(typeof(Children))]
        public async Task<IHttpActionResult> DeleteChildren(int id)
        {
            Children children = await db.Children.FindAsync(id);
            if (children == null)
            {
                return NotFound();
            }

            db.Children.Remove(children);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    return BadRequest("You can't delete this record because it has related records!");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok(children);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChildrenExists(int id)
        {
            return db.Children.Count(e => e.ChildrenId == id) > 0;
        }
    }
}