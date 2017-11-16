namespace Parents.Domain
{
    using Parents.Domain.HealthManagement;
    using Parents.Domain.HealthManagement.Categories;
    using Parents.Domain.ParentalManagement.Helpers;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Children
    {
        #region IdentityInformation
        [Key]
        public int ChildrenId { get; set; }

        public int ParentId { get; set; }

        public int BoodInformationId { get; set; }

        public int MatrimonialStateId { get; set; }

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

        [Required(ErrorMessage = "The field {0} is required")]
        [DataType(DataType.Date)]
        public DateTime ChildrenBirthDate { get; set; }
        #endregion

        #region HealthInformation
        public string ChildrenFamilyDoctor { get; set; }
        //public string BloodType { get; set; }
        

        #endregion

        #region ContactInformation
        public string ChildrenEmail { get; set; }
        public string ChildrenMobile { get; set; }
        public string ChildrenAddress { get; set; }
        #endregion

        #region SchoolInformation
        public string CurrentSchool { get; set; }
        public string SchoolContact { get; set; }
        #endregion

        #region ParentsInformation
        public string FatherId { get; set; }
        public string MotherId { get; set; }
        public virtual Parent Father { get; set; }
        public virtual Parent Mother { get; set; }
        public virtual MatrimonialState ParentsMatrimonialState { get; set; }
        #endregion

        public virtual Parent Parent { get; set; }
        public virtual BloodInformation BloodInformation { get; set; }
    }
}
