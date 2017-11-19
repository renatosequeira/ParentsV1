using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.SchoolManagement.Helpers
{
    /*
        Provas de aferição
        Provas finais
        Provas nacionais
         */
    public class ExamFamily
    {
        [Key]
        public int ExamFamilyId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("ExamFamily_ExamFamilyDescription_Index", IsUnique = true)]
        public string ExamFamilyDescription { get; set; }

        [JsonIgnore]
        public virtual ICollection<Exam> Exam { get; set; }
    }
}
