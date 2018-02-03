namespace Parents.Models.HealthManagement
{
    using System;

    public class ChildrenWeight
    {
        public int ChildrenWeightId { get; set; }

        public int ChildrenId { get; set; }

        public double WeightVaue { get; set; }

        public string WeightUnit { get; set; }

        public DateTime RegistryDate { get; set; }
    }
}
