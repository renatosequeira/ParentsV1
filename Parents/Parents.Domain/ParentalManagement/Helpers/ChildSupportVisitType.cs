using Parents.Domain.AppCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.ParentalManagement.Helpers
{
    /*
     Fim-de-semana
     Natal
     Aniversário Pai
     Aniversário Mãe
     */

    public class ChildSupportVisitType
    {
        [Key]
        public int ChildSupportVisitTypeId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("ChildSupportVisitType_ChildSupportVisitTypeDescription_Index", IsUnique = true)]
        public string ChildSupportVisitTypeDescription { get; set; }

        [Display(Name ="Remarks")]
        [DataType(DataType.MultilineText)]
        public string ChildSupportVisitTypeRemarks { get; set; }

        public virtual ICollection<ChildManagement> ChildManagement { get; set; }
        public virtual ICollection<ChildSupportVisit> ChildSupportVisit { get; set; }
    }
}
