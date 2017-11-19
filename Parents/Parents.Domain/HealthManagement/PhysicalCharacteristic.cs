using Newtonsoft.Json;
using Parents.Domain.HealthManagement.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.HealthManagement
{
    public class PhysicalCharacteristic
    {
        [Key]
        public int PhysicalCharacteristicId { get; set; }

        public int? PhysicalCharacteristicTypeId { get; set; }

        public int? HumanBodyAreaId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(150, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string PhysicalCharacteristicDescription { get; set; }

        [JsonIgnore]
        public virtual PhysicalCharacteristicType PhysicalCharacteristicType { get; set; }

        [JsonIgnore]
        public virtual HumanBodyAreas HumanBodyAreas { get; set; }

    }
}
