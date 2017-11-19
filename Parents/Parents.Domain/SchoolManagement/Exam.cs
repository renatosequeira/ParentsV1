using Newtonsoft.Json;
using Parents.Domain.SchoolManagement.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.SchoolManagement
{
    public class Exam
    {
        [Key]
        public int ExamId { get; set; }

        [DataType(DataType.Date)]
        public DateTime ExamDate { get; set; }

        public int? DisciplineId { get; set; }
        [JsonIgnore]
        public virtual Discipline Discipline { get; set; }

        public int? ExamFamilyId { get; set; }
        [JsonIgnore]
        public virtual ExamFamily ExamFamily { get; set; }

        public int? SchoolId { get; set; }
        [JsonIgnore]
        public virtual School School { get; set; }

        public string ExamFinalNote { get; set; }
    }
}
