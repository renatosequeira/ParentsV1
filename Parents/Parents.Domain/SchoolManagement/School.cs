﻿using Parents.Domain.SchoolManagement.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.SchoolManagement
{
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

        public virtual ICollection<Exam> Exam { get; set; }

        public virtual ICollection<AcademicYear> AcademicYear { get; set; }
    }
}
