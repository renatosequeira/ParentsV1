using Parents.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Parents.Backend.Models.AppCore
{
    [NotMapped] //não vai mapear os items da classe, para a base de dados
    public class ParentView : Parent
    {
        [Display(Name = "Image")]
        public HttpPostedFileBase Image { get; set; }
    }
}