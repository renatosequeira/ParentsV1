using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.ParentalManagement.Helpers
{
    /*
     Mapas de pagamentos de pensão de alimentos
         */
    public class ChildSupportPayment
    {
        [Key]
        public int ChildSupportPaymentId { get; set; }

        [Required(ErrorMessage ="Please insert a valid value")]
        [DataType(DataType.Currency)]
        [Display(Name = "Payed amount")]
        public double ChildSupportPaymentValue { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ChildSupportPaymentDate { get; set; }

        [Display(Name ="Remarks")]
        public string ChildSupportPaymentRemarks { get; set; }
    }
}
