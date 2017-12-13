namespace Parents.Domain
{
    using Newtonsoft.Json;
    using Parents.Domain.ActivitiesManagement;
    using Parents.Domain.HealthManagement;
    using Parents.Domain.ParentalManagement.Helpers;
    using Parents.Domain.SchoolManagement;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Parent
    {
        #region IdentityInformation
        [Key]
        public int ParentId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "Please insert a {0} with less than {1} characters")]
        [Display(Name = "First Name")]
        public string ParentFirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string ParentMiddleName { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "Please insert a {0} with less than {1} characters")]
        [Display(Name = "Last Name")]
        public string ParentLastName { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "Please insert a {0} with less than {1} characters")]
        [Index("Children_ChildrenIdentityCard_Index", IsUnique = true)]
        [Display(Name = "Identity Card")]
        public string ParentIdentityCard { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime ParentBirthDate { get; set; }
        #endregion

        #region ContactInformation
        [Required(ErrorMessage = "The field {0} is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string ParentEmail { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string ParentMobile { get; set; }

        [Display(Name = "Complete address")]
        public string ParentAddress { get; set; }
        #endregion

        #region ParenthoodInformation
        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "Parenthood type (father/ mother/ other)")]
        //public string ParenthoodType { get; set; }
        public int ParentalTypeId { get; set; }
        [JsonIgnore]
        public virtual ParentalType ParentalType { get; set; }
        #endregion

        [JsonIgnore]
        public virtual ICollection<Urgency> Urgency { get; set; }

        [JsonIgnore]
        public virtual ICollection<Children> Children { get; set; }

        [JsonIgnore]
        public virtual ICollection<Activity> Activity { get; set; }

        [JsonIgnore]
        public virtual ICollection<ParentsMeeting> ParentsMeeting { get; set; }

        public string ParentImage { get; set; }
    }
}
