namespace Parents.API.Models.ActivitiesManagement
{
    using Domain.ActivitiesManagement;

    public class ActivityRequest : Activities
    {
        public byte[] ImageArray { get; set; }
    }
}