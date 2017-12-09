namespace Parents.Domain.ActivitiesManagement.Helpers
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ActivityType
    {
        /*
         Parents Meeting
         WorkGroup
         Study Trips
         */

        [Key]
        public int ActivityTypeId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("ActivityType_ActivityTypeDescription_Index", IsUnique = false)]
        [Display(Name ="Activity Tyoe Description")]
        public string ActivityTypeDescription { get; set; }

        [JsonIgnore]
        public virtual ICollection<Activity> Activity { get; set; }

        public string userId { get; set; }

        public bool ActivityTypePrivacy { get; set; }
    }
}
