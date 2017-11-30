namespace Parents.Domain.ActivitiesManagement
{
    using Newtonsoft.Json;
    using Parents.Domain.ActivitiesManagement.Helpers;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        public int? ActivityFamilyId { get; set; }

        [JsonIgnore]
        public virtual ActivityFamily ActivityFamily { get; set; }

        public int? ActivityTypeId { get; set; }
        [Display(Name = "Activity Type")]
        [JsonIgnore]
        public virtual ActivityType ActivityType { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name ="Activity Description")]
        [MaxLength(250, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string ActivityDescription { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Activity date start")]
        public DateTime ActivityDateStart { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Activity date start")]
        public Nullable<DateTime> ActivityDateEnd { get; set; }

        [DataType(DataType.MultilineText)]
        public string ActivityRemarks { get; set; }

        public int? ActivityPeriodicityId { get; set; }
        [JsonIgnore]
        public virtual ActivityPeriodicity ActivityPeriodicity { get; set; }

        public int? ParentId { get; set; }
        [Display(Name ="Responsible In Charge")]
        [JsonIgnore]
        public virtual Parent ParentInCharge { get; set; }

        public int? ActivityInstitutionTypeId { get; set; }
        [JsonIgnore]
        public virtual ActivityInstitutionType ActivityInstitutionType { get; set; }

        public string ActivityAddress { get; set; }

        public string Image { get; set; }
    }
}
