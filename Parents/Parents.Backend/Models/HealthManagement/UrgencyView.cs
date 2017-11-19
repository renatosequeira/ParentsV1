using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Parents.Backend.Models.HealthManagement
{
    [NotMapped]
    public class UrgencyView
    {
        [Display(Name = "Image")]
        public HttpPostedFileBase Image { get; set; }
    }
}