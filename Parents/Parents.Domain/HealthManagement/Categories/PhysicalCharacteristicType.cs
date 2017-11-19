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
    public class PhysicalCharacteristicType
    {
        [Key]
        public int PhysicalCharacteristicTypeId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("PhysicalCharacteristicType_PhysicalCharacteristicTypeDescription_Index", IsUnique = true)]
        public string PhysicalCharacteristicTypeDescription { get; set; }

        [JsonIgnore]
        public virtual ICollection<PhysicalCharacteristic> PhysicalCharacteristic { get; set; }
    }
}
