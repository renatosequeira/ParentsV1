using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parents.API.Models.SchoolManagement.Helpers
{
    public class DisciplineResponse
    {
        public int DisciplineId { get; set; }

        public string DisciplineDescription { get; set; }

        public string DisciplineRemarks { get; set; }

        public string userId { get; set; }
    }
}