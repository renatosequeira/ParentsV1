using Parents.Domain.ParentalManagement.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.AppCore
{
    public class ChildManagement
    {
        [Key]
        public int ChildManagementId { get; set; }

        public int ChildrenId { get; set; }
        public virtual Children Children { get; set; }

        public int ParentId { get; set; }
        public virtual Parent Parent { get; set; }

        public int ParentalTypeId { get; set; }
        public virtual ParentalType ParentalType { get; set; }

        public int ChildSupportVisitTypeId { get; set; }
        [Display(Name ="Visit Type")]
        public virtual ChildSupportVisitType ChildSupportVisitType { get; set; }

        public int ChildSupportVisitId { get; set; }
        [Display(Name ="Last Visit")]
        public virtual ChildSupportVisit ChildSupportLastVisit { get; set; }

        public double ChildSupportAgreedValue { get; set; }

        public int MatrimonialStateId { get; set; }
        [Display(Name ="Parents Matrimonial State")]
        public virtual MatrimonialState ParentsMatrimonialState { get; set; }

        public int ParentalGuardTermId { get; set; }
        public virtual ParentalGuardTerm ParentalGuardTerm { get; set; }
    }
}
