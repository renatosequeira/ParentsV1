namespace Parents.Domain.ActivitiesManagement.Helpers
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ActivityPeriodicity
    {
        [Key]
        public int ActivityPeriodicityId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("ActivityPeriodicity_ActivityPeriodicityDescription_Index", IsUnique = true)]
        public string ActivityPeriodicityDescription { get; set; }

        [JsonIgnore]
        public virtual ICollection<Activities> Activity { get; set; }

        public string userId { get; set; }
    }
}
