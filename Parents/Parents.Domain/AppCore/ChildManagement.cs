using Newtonsoft.Json;
using Parents.Domain.ParentalManagement.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.AppCore
{
    public class ChildManagement
    {

        [Key]
        public int ChildManagementId { get; set; }

        public int ChildrenId { get; set; }
        [JsonIgnore]
        public virtual Children Children { get; set; }

        public int ParentId { get; set; }
        [JsonIgnore]
        public virtual Parent Parent { get; set; }

        public int ParentalTypeId { get; set; }
        [JsonIgnore]
        public virtual ParentalType ParentalType { get; set; }

        public int? ChildSupportVisitTypeId { get; set; }
        [Display(Name ="Visit Type")]
        [JsonIgnore]
        public virtual ChildSupportVisitType ChildSupportVisitType { get; set; }

        public int? ChildSupportVisitId { get; set; }
        [Display(Name ="Last Visit")]
        [JsonIgnore]
        public virtual ChildSupportVisit ChildSupportLastVisit { get; set; }

        [DataType(DataType.Currency)]
        public double ChildSupportAgreedValue { get; set; }

        public int MatrimonialStateId { get; set; }
        [Display(Name ="Parents Matrimonial State")]
        [JsonIgnore]
        public virtual MatrimonialState ParentsMatrimonialState { get; set; }

        public int? ParentalGuardTermId { get; set; }
        [JsonIgnore]
        public virtual ParentalGuardTerm ParentalGuardTerm { get; set; }

    }

}
