﻿namespace Parents.Models
{
    using System;

    public class Parent
    {
        public int ParentId { get; set; }
        public string ParentFirstName { get; set; }
        public string ParentMiddleName { get; set; }
        public string ParentLastName { get; set; }
        public string ParentIdentityCard { get; set; }
        public DateTime ParentBirthDate { get; set; }
        public string ParentEmail { get; set; }
        public string ParentMobile { get; set; }
        public object ParentAddress { get; set; }
        public int ParentalTypeId { get; set; }
        public string ParentImage { get; set; }
    }
}
