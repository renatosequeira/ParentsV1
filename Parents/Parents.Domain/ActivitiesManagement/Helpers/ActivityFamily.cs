using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.ActivitiesManagement.Helpers
{
    /*
     Extra-Curricular
     Curricular
         */
       
    public class ActivityFamily
    {
        [Key]
        public int ActivityFamilyId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("ActivityFamily_ActivityFamilyDescription_Index", IsUnique = true)]
        public string ActivityFamilyDescription { get; set; }

        [JsonIgnore]
        public ICollection<Activity> Activity { get; set; }
    }
}
