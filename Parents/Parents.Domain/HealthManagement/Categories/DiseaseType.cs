using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.HealthManagement.Categories
{
    public class DiseaseType
    {
        [Key]
        public int DiseaseTypeId { get; set; }

        public int DiseaseFamilyId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("DiseaseType_DiseaseTypeDescription_Index", IsUnique = true)]
        public string DiseaseTypeDescription { get; set; }

        [DataType(DataType.MultilineText)]
        public string DiseaseRemarks { get; set; }

        [DataType(DataType.Url)]
        public string DiseaseExternalLink { get; set; }

        public virtual DiseaseFamily DiseaseFamily { get; set; }
    }
}
