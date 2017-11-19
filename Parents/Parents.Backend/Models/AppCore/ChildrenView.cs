﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Parents.Backend.Models.AppCore
{
    [NotMapped]
    public class ChildrenView
    {
        [Display(Name = "Image")]
        public HttpPostedFileBase Image { get; set; }
    }
}