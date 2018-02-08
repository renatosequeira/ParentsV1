namespace Parents.Domain.HealthManagement
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ChildrenWeight
    {
        [Key]
        public int ChildrenWeightId { get; set; }

        public int ChildrenId { get; set; }

        public double WeightVaue { get; set; }

        public string WeightUnit { get; set; }

        public DateTime RegistryDate { get; set; }

        public double OldWeightValue { get; set; }
    }
}
