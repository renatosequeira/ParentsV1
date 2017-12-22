using System;

namespace Parents.API.Models.ActivitiesManagement
{
    public class ActivityResponse
    {
        public int ActivityId { get; set; }

        public int ChildrenId { get; set; }

        public string ActivityDescription { get; set; }

        public DateTime ActivityDateStart { get; set; }

        public DateTime ActivityDateEnd { get; set; }

        public string ActivityRemarks { get; set; }

        public string ActivityAddress { get; set; }

        public string Image { get; set; }

        public string userId { get; set; }

        public bool ActivityPrivacy { get; set; }

        public string relatedChildrenIdentitiCard { get; set; }

        public string invitedUserId { get; set; }

        public bool invitationAcknowledged { get; set; }

        public bool Status { get; set; }
        
        public string ChildrenActivityType { get; set; }

        public string ChildrenActivityFamily { get; set; }

        public string ActivityPriority { get; set; }

        public TimeSpan ActivityTimeStart { get; set; }

        public TimeSpan ActivityTimeEnd { get; set; }

        public string ActivityRepeat { get; set; }

        public string EventId { get; set; }
    }
}