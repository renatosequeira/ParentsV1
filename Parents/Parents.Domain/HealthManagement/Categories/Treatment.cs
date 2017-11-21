using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.HealthManagement.Categories
{
    public class Treatment
    {
        [Key]
        public int TreatmentId { get; set; }

        [DataType(DataType.MultilineText)]
        public string TreatmentRemarks { get; set; }

        [DataType(DataType.Date)]
        public DateTime TreatmentStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? TreatmentEndDate { get; set; }

        public string TreatmentStartHour { get; set; }

        public string TreatmentRepeatsPerDay { get; set; }

        public string TreatmentDosagePerRepeat { get; set; }

        public bool TreatmentStarted { get; set; }

        public bool TreadmentFinished { get; set; }

        public int? MedicineId { get; set; }
        [JsonIgnore]
        public virtual Medicine Medicine { get; set; }

        public int? MedicineDosageId { get; set; }
        [JsonIgnore]
        public virtual MedicineDosage MedicineDosage { get; set; }

        public int? MedicinePharmaceuticalFormId { get; set; }
        [JsonIgnore]
        public virtual MedicinePharmaceuticalForm MedicinePharmaceuticalForm { get; set; }

        public int? DiseaseId { get; set; }
        [JsonIgnore]
        public virtual Disease Disease { get; set; }
    }
}
