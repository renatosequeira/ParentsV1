using Parents.Domain.ActivitiesManagement.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.ActivitiesManagement
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        public int ActivityFamilyId { get; set; }
        public virtual ActivityFamily ActivityFamily { get; set; }

        public int ActivityTypeId { get; set; }
        [Display(Name = "Activity Type")]
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

        public int ActivityPeriodicityId { get; set; }
        public virtual ActivityPeriodicity ActivityPeriodicity { get; set; }

        public int ParentId { get; set; }
        [Display(Name ="Responsible In Charge")]
        public virtual Parent ParentInCharge { get; set; }

        public int ActivityInstitutionTypeId { get; set; }
        public virtual ActivityInstitutionType ActivityInstitutionType { get; set; }

        public string ActivityAddress { get; set; }
    }
}
