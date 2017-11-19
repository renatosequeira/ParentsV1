using Newtonsoft.Json;
using Parents.Domain.AppCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.ParentalManagement.Helpers
{
    /*

        Conjunta    
        Partilhada
        
     */

    public class ParentalGuardTerm
    {
        [Key]
        public int ParentalGuardTermId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("ParentalGuardTerm_ParentalGuardTermDescription_Index", IsUnique = true)]
        public string ParentalGuardTermDescription { get; set; }

        [DataType(DataType.MultilineText)]
        public string ParentalGuardTermRemarks { get; set; }

        [JsonIgnore]
        public virtual ICollection<ChildManagement> ChildManagement { get; set; }
    }
}
