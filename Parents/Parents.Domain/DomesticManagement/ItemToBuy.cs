namespace Parents.Domain.DomesticManagement
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ItemToBuy
    {
        [Key]
        public int ItemToBuyId { get; set; }

        public int ItemCategoryId { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string ItemToBuyDescription { get; set; }

        [DataType(DataType.Date)]
        public DateTime ItemToBuyDateAdded { get; set; }

        public string ItemToBuyOwner { get; set; }

        public string ItemToBuyAssignment { get; set; }

        public bool ItemToBuyStatus { get; set; }

        [JsonIgnore]
        public virtual ItemsCategory ItemsCategory { get; set; }
    }
}
