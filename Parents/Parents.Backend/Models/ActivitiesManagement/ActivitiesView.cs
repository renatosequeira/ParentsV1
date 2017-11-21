using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Parents.Domain;
using Parents.Domain.ActivitiesManagement;

namespace Parents.Backend.Models.ActivitiesManagement
{
    [NotMapped]
    public class ActivitiesView : Activity
    {
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}