using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.SchoolManagement
{
    public class ParentsMeeting
    {
        [Key]
        public int ParentsMeetingId { get; set; }

        [DataType(DataType.Date)]
        public DateTime ParentsMeetingDate { get; set; }

        public int? ParentId { get; set; }
        [Display(Name = "Attendee")]
        [JsonIgnore]
        public virtual Parent Parent { get; set; }

        [Display(Name = "Remarks")]
        public string ParentsMeetingRemarks  { get; set; }
    }
}
