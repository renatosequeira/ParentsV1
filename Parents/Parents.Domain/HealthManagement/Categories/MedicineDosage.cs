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
    public class MedicineDosage
    {
        [Key]
        public int MedicineDosageId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(10, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("MedicineDosage_MedicineDosageDescription_Index", IsUnique = true)]
        public string MedicineDosageDescription { get; set; }

        [JsonIgnore]
        public virtual ICollection<Disease> Disease { get; set; }

        [JsonIgnore]
        public virtual ICollection<Treatment> Treatment { get; set; }
    }
}
