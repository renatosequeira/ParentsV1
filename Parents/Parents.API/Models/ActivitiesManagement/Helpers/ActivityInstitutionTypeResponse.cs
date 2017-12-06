using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parents.API.Models.ActivitiesManagement.Helpers
{
    public class ActivityInstitutionTypeResponse
    {
        public int ActivityInstitutionTypeId { get; set; }

        public string ActivityInstitutionTypeDescription { get; set; }

        public string userId { get; set; }
    }
}