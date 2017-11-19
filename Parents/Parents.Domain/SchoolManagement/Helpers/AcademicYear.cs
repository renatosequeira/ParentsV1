using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.SchoolManagement.Helpers
{
    public class AcademicYear
    {
        [Key]
        public int AcademicYearId { get; set; }

        [Display(Name ="Academic Year")]
        public string AcademicYearReference { get; set; }

        public string AcademicYearClassDirector { get; set; }

        public string AcademicYearGPA { get; set; } //Graduation Points Average (Media de final de curso)

        public bool AcademicYearAchievment { get; set; }

        public int? SchoolId { get; set; }
        [JsonIgnore]
        public virtual School  School{ get; set; }
    }
}
