namespace Parents.Domain
{
    using Newtonsoft.Json;
    using Parents.Domain.ActivitiesManagement;
    using Parents.Domain.HealthManagement.Categories;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Children
    {
        #region IdentityInformation
        [Key]
        public int ChildrenId { get; set; }

        public int? BoodInformationId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50,ErrorMessage = "Please insert a {0} with less than {1} characters")]
        public string ChildrenFirstName { get; set; }

        public string ChildrenMiddleName { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "Please insert a {0} with less than {1} characters")]
        public string ChildrenLastName { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("Children_ChildrenIdentityCard_Index", IsUnique = true)]
        public string ChildrenIdentityCard { get; set; }

        //[Required(ErrorMessage = "The field {0} is required")]
        [DataType(DataType.Date)]
        public DateTime? ChildrenBirthDate { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name ="Gender")]
        public string ChildrenSex { get; set; }

        public bool IsMale { get; set; }

        public bool ChildWithHealthIssues { get; set; }
        #endregion

        #region HealthInformation
        public string ChildrenFamilyDoctor { get; set; }
        #endregion

        #region ContactInformation
        public string ChildrenEmail { get; set; }
        public string ChildrenMobile { get; set; }
        public string ChildrenAddress { get; set; }

        public string FatherEmergencyContact { get; set; }
        public string MotherEmergencyContact { get; set; }
        public string AlternativeEmergencyContact1 { get; set; }
        public string AlternativeEmergencyContact2 { get; set; }

        #endregion

        #region SchoolInformation
        public string CurrentSchool { get; set; }
        public string SchoolContact { get; set; }
        #endregion

        #region ParentsInformation
        public string FirstParentId { get; set; }
        public string SecondParendId { get; set; }


        #endregion

        #region PhysicalCharacteristics
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        #endregion

        [JsonIgnore]
        public virtual BloodInformation BloodInformation { get; set; }

        public string BloodInformationDescription { get; set; }

        #region Multimedia
        public string ChildrenImage { get; set; }
        public string ChildrenProfileBannerImage { get; set; } 
        #endregion

        [JsonIgnore]
        public ICollection<Activities> Activities { get; set; }
    }
}
