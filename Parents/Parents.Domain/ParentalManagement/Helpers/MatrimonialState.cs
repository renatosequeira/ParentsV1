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
    public class MatrimonialState
    {
        [Key]
        public int MatrimonialStateId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("MatrimonialState_MatrimonialStateDescription_Index", IsUnique = true)]
        public string MatrimonialStateDescription { get; set; }

        [DataType(DataType.MultilineText)]
        public string MatrimonialStateRemarks { get; set; }

        public virtual ICollection<Children> Children { get; set; }

        public virtual ICollection<ChildManagement> ChildManagement { get; set; }
    }
}
