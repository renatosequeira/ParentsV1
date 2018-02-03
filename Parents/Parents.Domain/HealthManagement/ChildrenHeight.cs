namespace Parents.Domain.HealthManagement
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ChildrenHeight
    {
        [Key]
        public int ChildrenHeightId { get; set; }

        public int ChildrenId { get; set; }

        public double HeightVaue { get; set; }

        public string HeightUnit { get; set; }

        public DateTime RegistryDate { get; set; }
    }
}
