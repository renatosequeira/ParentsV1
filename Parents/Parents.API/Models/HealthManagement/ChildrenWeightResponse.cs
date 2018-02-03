using System;

namespace Parents.API.Models.HealthManagement
{
    public class ChildrenWeightResponse
    {
        public int ChildrenWeightId { get; set; }

        public int ChildrenId { get; set; }

        public double WeightVaue { get; set; }

        public string WeightUnit { get; set; }

        public DateTime RegistryDate { get; set; }
    }
}