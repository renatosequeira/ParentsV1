namespace Parents.API.Models.ActivitiesManagement
{
    using Domain.ActivitiesManagement;

    public class ActivityRequest : Activity
    {
        public byte[] ImageArray { get; set; }
    }
}