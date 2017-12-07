using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parents.API.Models.ActivitiesManagement.Helpers
{
    public class ActivityPeridiocityResponse
    {

        public int ActivityPeriodicityId { get; set; }
        public string ActivityPeriodicityDescription { get; set; }
        public string userId { get; set; }
    }
}