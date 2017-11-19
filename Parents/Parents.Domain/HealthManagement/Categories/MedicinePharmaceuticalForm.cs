using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.HealthManagement.Categories
{
    public class MedicinePharmaceuticalForm
    {
        [Key]
        public int MedicinePharmaceuticalFormId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("MedicinePharmaceuticalForm_MedicinePharmaceuticalFormDescription_Index", IsUnique = true)]
        public string MedicinePharmaceuticalFormDescription { get; set; }

        [JsonIgnore]
        public virtual ICollection<Disease> Disease { get; set; }
    }
}
