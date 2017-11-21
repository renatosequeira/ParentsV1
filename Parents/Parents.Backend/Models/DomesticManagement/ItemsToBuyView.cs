using Parents.Domain.DomesticManagement;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;


namespace Parents.Backend.Models.DomesticManagement
{
    [NotMapped]
    public class ItemsToBuyView : ItemToBuy
    {
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}