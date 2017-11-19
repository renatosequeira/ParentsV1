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
    public class Alergy
    {
        [Key]
        public int AlergyId { get; set; }

        public int? AlergyTypeId { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string AlergyDescription { get; set; }

        [JsonIgnore]
        public virtual AlergyType AlergyTypes { get; set; }
    }
}
