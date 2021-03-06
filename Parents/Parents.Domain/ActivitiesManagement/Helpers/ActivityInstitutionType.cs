﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parents.Domain.ActivitiesManagement.Helpers
{
    public class ActivityInstitutionType
    {
        /*
         School
         Pool
         Tenos court
         Gimnasium
         Etc...
             */

        [Key]
        public int ActivityInstitutionTypeId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("ActivityInstitutionType_ActivityInstitutionTypeDescription_Index", IsUnique = true)]
        public string ActivityInstitutionTypeDescription { get; set; }

        [JsonIgnore]
        public virtual ICollection<Activities> Activity { get; set; }

        public string userId { get; set; }
    }
}
