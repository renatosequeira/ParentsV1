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
     Father/ Mother
     */

    public class ParentalType
    {
        [Key]
        public int ParentalTypeId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("ParentalType_ParentalTypeDescription_Index", IsUnique = true)]
        [Display(Name ="Description")]
        public string ParentalTypeDescription { get; set; }

        [JsonIgnore]
        public virtual ICollection<ChildManagement> ChildManagement { get; set; }

        [JsonIgnore]
        public virtual ICollection<Parent> Parent { get; set; }
    }
}
