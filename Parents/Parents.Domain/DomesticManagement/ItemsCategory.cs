namespace Parents.Domain.DomesticManagement
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ItemsCategory
    {
        [Key]
        public int ItemCategoryId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("Children_ChildrenIdentityCard_Index", IsUnique = true)]
        public string ItemCategoryDescription { get; set; }

        [JsonIgnore]
        public virtual ICollection<ItemToBuy> ItemToBuy { get; set; }
    }
}
