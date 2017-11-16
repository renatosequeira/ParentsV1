using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.HealthManagement.Categories
{
    public class DiseaseFamily
    {
        [Key]
        public int DiseaseFamilyId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("DiseaseFamily_DiseaseFamilyDescription_Index", IsUnique = true)]
        public string DiseaseFamilyDescription { get; set; }

        public virtual ICollection<DiseaseType> DiseaseType { get; set; }
    }
}
