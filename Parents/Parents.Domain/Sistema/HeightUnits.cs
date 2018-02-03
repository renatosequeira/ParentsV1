namespace Parents.Domain.Sistema
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class HeightUnits
    {
        [Key]
        public int HeightUnitId { get; set; }

        [Display(Name = "Height Description")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string HeightUnitDescription { get; set; }
    }
}
