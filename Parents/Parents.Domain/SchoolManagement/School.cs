namespace Parents.Domain.SchoolManagement
{
    using Newtonsoft.Json;
    using Parents.Domain.SchoolManagement.Helpers;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class School
    {
        [Key]
        public int SchoolId { get; set; }

        [Display(Name ="School name")]
        public string SchoolName { get; set; }

        [Display(Name ="School Address")]
        public string SchoolAddress { get; set; }

        [Display(Name = "School Phone")]
        public string SchoolPhone { get; set; }

        [JsonIgnore]
        public virtual ICollection<Exam> Exam { get; set; }

        [JsonIgnore]
        public virtual ICollection<AcademicYear> AcademicYear { get; set; }
    }
}
