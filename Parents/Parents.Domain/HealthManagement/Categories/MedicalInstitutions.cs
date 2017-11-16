using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.HealthManagement.Categories
{
    public class MedicalInstitutions
    {
        [Key]
        public int MedicalInstitutionId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(150, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("MedicalInstitutions_MedicalInstitutionName_Index", IsUnique = true)]
        public string MedicalInstitutionName { get; set; }

        public string MedicalInstitutionAddress { get; set; }

        public virtual ICollection<Urgency> Urgency { get; set; }
    }
}
