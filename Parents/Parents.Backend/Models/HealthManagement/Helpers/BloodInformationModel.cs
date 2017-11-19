using Parents.Domain.HealthManagement.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parents.Backend.Models.HealthManagement.Helpers
{
    public class BloodInformationModel
    {
        public List<BloodInformation> BloodInformation { get; set; }
        public int? BloodInformationId_ { get; set; }
        public string BloodInformationDescription_ { get; set; }
    }
}