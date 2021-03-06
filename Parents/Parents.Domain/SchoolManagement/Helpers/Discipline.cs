﻿namespace Parents.Domain.SchoolManagement.Helpers
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Discipline
    {
        [Key]
        public int DisciplineId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("Discipline_DisciplineDescription_Index", IsUnique = true)]
        public string DisciplineDescription { get; set; }

        [DataType(DataType.MultilineText)]
        public string DisciplineRemarks { get; set; }

        [JsonIgnore]
        public virtual ICollection<Exam> Exam { get; set; }

        public string userId { get; set; }

    }
}
