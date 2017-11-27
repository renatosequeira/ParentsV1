namespace Parents.API.Models.AppCore
{
    using System;
    using System.Collections.Generic;

    public class ParentResponse
    {
        public int ParentId { get; set; }


        public string ParentFirstName { get; set; }

        public string ParentMiddleName { get; set; }


        public string ParentLastName { get; set; }


        public string ParentIdentityCard { get; set; }


        public DateTime ParentBirthDate { get; set; }


        public string ParentEmail { get; set; }

        public string ParentMobile { get; set; }


        public string ParentAddress { get; set; }



        //public virtual List<Urgency> Urgency { get; set; }

        public virtual List<ChildrenResponse> Children { get; set; }

        //public virtual List<Activity> Activity { get; set; }

        //public virtual List<ParentsMeeting> ParentsMeeting { get; set; }

        public string ParentImage { get; set; }
    }
}