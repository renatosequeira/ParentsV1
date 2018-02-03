namespace Parents.Domain.Sistema
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class WeightUnits
    {
        [Key]
        public int WeightUnitId { get; set; }

        [Display(Name = "Weight Description")]
        [Required(ErrorMessage ="The field {0} is mandatory")]
        public string WeightUnitDescription { get; set; }
    }
}
