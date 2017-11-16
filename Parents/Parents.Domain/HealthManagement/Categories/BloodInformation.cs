namespace Parents.Domain.HealthManagement.Categories
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BloodInformation
    {
        [Key]
        public int BoodInformationId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(10, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("BloodType_BloodTypeDescription_Index", IsUnique = true)]
        public string BloodInformationDescription { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(250, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string BloodInformationRemarks { get; set; }

        public virtual ICollection<Children> Children { get; set; }
    }
}
