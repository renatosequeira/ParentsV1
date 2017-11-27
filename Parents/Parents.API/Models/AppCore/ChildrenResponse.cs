namespace Parents.API.Models.AppCore
{
    using System;

    public class ChildrenResponse
    {
        #region IdentityInformation

        public int ChildrenId { get; set; }

        public string ChildrenFirstName { get; set; }

        public string ChildrenMiddleName { get; set; }

        public string ChildrenLastName { get; set; }

        public string ChildrenIdentityCard { get; set; }

        public DateTime ChildrenBirthDate { get; set; }

        public string ChildrenSex { get; set; }
        #endregion

        #region HealthInformation
        public string ChildrenFamilyDoctor { get; set; }
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
        public string FirstParentId { get; set; }
        public string SecondParentId { get; set; }
        #endregion


        public string BloodInformationDescription { get; set; }

        public string ChildrenImage { get; set; }
    }
}