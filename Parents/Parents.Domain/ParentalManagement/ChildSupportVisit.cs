using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.ParentalManagement.Helpers
{
    /*
     Inventário de visitas
         */
    public class ChildSupportVisit
    {
        [Key]
        public int ChildSupportVisitId { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage ="{0} is mandatory!")]
        [Display(Name = "Visit Date Start")]
        public DateTime ChildSupportVisitDateStart { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Visit Date End")]
        public Nullable<DateTime> ChildSupportVisitDateEnd { get; set; }

        public int ChildSupportVisitTypeId { get; set; }
        [Display(Name ="Visit Type")]
        public virtual ChildSupportVisitType ChildSupportVisitType { get; set; }

        [Display(Name ="Recorded Incidents?")]
        public bool ChildSupportVisitRecordedIncidents { get; set; }

        public ChildSupportVisit()
        {
            ChildSupportVisitRecordedIncidents = false;
        }
    }
}
