using Parents.Domain.HealthManagement.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.HealthManagement
{
    public class Disease
    {
        [Key]
        public int DiseaseId { get; set; }

        #region External Identifiers
        public int DiseaseTypeId { get; set; }

        public int DiseaseFamilyId { get; set; }

        public int MedicinePharmaceuticalFormId { get; set; }

        public int MedicineDosageId { get; set; }

        public int MedicineId { get; set; }
        #endregion

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string DiseaseDescription { get; set; }

        #region External Virtual Fields
        public virtual DiseaseFamily DiseaseFamily { get; set; }

        public virtual DiseaseType DiseaseType { get; set; }

        public virtual Medicine Medicine { get; set; }

        public virtual MedicineDosage MedicineDosage { get; set; }

        public virtual MedicinePharmaceuticalForm MedicinePharmaceuticalForm { get; set; }
        
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

    }
}
