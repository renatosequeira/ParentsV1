using Newtonsoft.Json;
using Parents.Domain.HealthManagement.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Parents.Domain.HealthManagement
{
    public class Disease
    {
        [Key]
        public int DiseaseId { get; set; }

        #region External Identifiers
        public int? DiseaseTypeId { get; set; }

        public int? DiseaseFamilyId { get; set; }

        public int? MedicinePharmaceuticalFormId { get; set; }

        public int? MedicineDosageId { get; set; }

        public int? MedicineId { get; set; }
        #endregion

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string DiseaseDescription { get; set; }

        #region External Virtual Fields
        [JsonIgnore]
        public virtual DiseaseFamily DiseaseFamily { get; set; }

        [JsonIgnore]
        public virtual DiseaseType DiseaseType { get; set; }

        [JsonIgnore]
        public virtual Medicine Medicine { get; set; }

        [JsonIgnore]
        public virtual MedicineDosage MedicineDosage { get; set; }

        [JsonIgnore]
        public virtual MedicinePharmaceuticalForm MedicinePharmaceuticalForm { get; set; }

        [JsonIgnore]
        public virtual ICollection<Treatment> Treatment { get; set; }

        #endregion


        #region Timeline Identification
        [DataType(DataType.Date)]
        public DateTime DateDiagnosed { get; set; }

        [DataType(DataType.Date)]
        public Nullable<DateTime> DateCured { get; set; }
        #endregion

        #region History Identification
        public bool HasGeneticOrigin { get; set; }
        #endregion

        public bool IsTreatable { get; set; }
        public bool HasAssociatedMedicines{ get; set; }

        public string DiseaseImage { get; set; }

    }
}
