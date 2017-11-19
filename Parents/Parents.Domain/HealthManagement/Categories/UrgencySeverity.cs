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
    public class UrgencySeverity
    {
        [Key]
        public int UrgencySeverityId { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "{0} is mandatory!")]
        [MaxLength(50, ErrorMessage = "{0} should be less than {1} characteres")]
        [Index("UrgencySeverity_UrgencySeverityDescription_Index", IsUnique = true)]
        public string UrgencySeverityDescription { get; set; }

        [JsonIgnore]
        public virtual ICollection<Urgency> Urgency { get; set; }
    }
}
