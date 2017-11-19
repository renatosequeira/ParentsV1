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
    public class AlergyType
    {
        [Key]
        public int AlergyTypeId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("AlergyTypes_AlergyTypeDescriptiom_Index", IsUnique = true)]
        public string AlergyTypeDescriptiom { get; set; }

        [DataType(DataType.MultilineText)]
        public string AlergyTypeCharacteristics { get; set; }

        [JsonIgnore]
        public virtual ICollection<Alergy> Alergy { get; set; }
    }
}
