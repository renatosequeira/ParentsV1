using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parents.API.Models.ActivitiesManagement.Helpers
{
    public class ActivityTypeResponse
    {
        public int ActivityTypeId { get; set; }

        public string ActivityTypeDescription { get; set; }

        public string userId { get; set; }

        public bool ActivityTypePrivacy { get; set; }
    }
}