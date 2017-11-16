using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.HealthManagement.Categories
{
    public class HumanBodyAreas
    {
        [Key]
        public int HumanBodyAreaId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("HumanBodyAreas_HumanBodyAreaDescription_Index", IsUnique = true)]
        public string HumanBodyAreaDescription { get; set; }

        public string HumanBodyAreaSide { get; set; }

        public ICollection<PhysicalCharacteristic> PhysicalCharacteristic { get; set; }
    }
}
