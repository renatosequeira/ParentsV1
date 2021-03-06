﻿namespace Parents.API.Models.AppCore
{
    using Parents.API.Models.ActivitiesManagement;
    using System;
    using System.Collections.Generic;

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
        public string SecondParentId { get; set; }
        #endregion

        #region PhysicalCharacteristics
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        #endregion

        public string BloodInformationDescription { get; set; }

        #region Multimedia
        public string ChildrenImage { get; set; }
        public string ChildrenProfileBannerImage { get; set; } 
        #endregion

        //public List<ActivityResponse> Activities { get; set; }
    }
}