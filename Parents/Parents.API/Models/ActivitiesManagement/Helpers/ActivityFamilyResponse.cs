using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parents.API.Models.ActivitiesManagement.Helpers
{
    public class ActivityFamilyResponse
    {
        public int ActivityFamilyId { get; set; }

        public string ActivityFamilyDescription { get; set; }

        public string userId { get; set; }
    }
}