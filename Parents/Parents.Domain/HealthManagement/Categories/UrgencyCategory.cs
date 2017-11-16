using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.HealthManagement.Categories
{
    public class UrgencyCategory
    {
        [Key]
        public int UrgencyCategoryId { get; set; }

        [Display(Name ="Description")]
        [Required(ErrorMessage ="{0} is mandatory!")]
        [MaxLength(50,ErrorMessage ="{0} should be less than {1} characteres")]
        [Index("UrgencyCategory_UrgencyCategoryDescription_Index", IsUnique = true)]
        public string UrgencyCategoryDescription { get; set; }

        public virtual ICollection<Urgency> Urgency { get; set; }
    }
}
