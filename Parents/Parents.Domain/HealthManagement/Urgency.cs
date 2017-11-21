using Newtonsoft.Json;
using Parents.Domain.HealthManagement.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.HealthManagement
{
    public class Urgency
    {
        #region Incident
        [Key]
        public int UrgencyId { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "{0} is mandatory!")]
        [MaxLength(250, ErrorMessage = "{0} should be less than {1} characteres")]
        [DataType(DataType.MultilineText)]
        public string UrgencyDescription { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} is mandatory!")]
        public DateTime UrgencyDateIn { get; set; }

        [DataType(DataType.Date)]
        public DateTime? UrgencyDateOut { get; set; }

        public bool UrgencyStatus { get; set; }

        #endregion

        #region ExternalTables
        public int? UrgencySeverityId { get; set; }

        public int? UrgencyCategoryId { get; set; }

        public int? ParentId { get; set; }

        public int? MedicalInstitutionId { get; set; }

        [JsonIgnore]
        public virtual UrgencyCategory UrgencyCategory { get; set; }

        [JsonIgnore]
        public virtual UrgencySeverity UrgencySeverity { get; set; }

        [JsonIgnore]
        public virtual Parent ParentInCharge { get; set; }

        [JsonIgnore]
        public virtual MedicalInstitutions MedicalInstitutions { get; set; }

        #endregion

        public string UrgencyImage { get; set; }

    }
}
