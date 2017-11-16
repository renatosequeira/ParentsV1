using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.ActivitiesManagement.Helpers
{
    public class ActivityPeriodicity
    {
        [Key]
        public int ActivityPeriodicityId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("ActivityPeriodicity_ActivityPeriodicityDescription_Index", IsUnique = true)]
        public string ActivityPeriodicityDescription { get; set; }

        public virtual ICollection<Activity> Activity { get; set; }
    }
}
