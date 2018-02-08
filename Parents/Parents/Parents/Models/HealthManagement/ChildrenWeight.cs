namespace Parents.Models.HealthManagement
{
    using System;
    using Microcharts;

    public class ChildrenWeight
    {
        #region Attributtes
        public int ChildrenWeightId { get; set; }

        public int ChildrenId { get; set; }

        public double WeightVaue { get; set; }

        public string WeightUnit { get; set; }

        public DateTime RegistryDate { get; set; }

        public double OldWeightValue { get; set; }
        #endregion

        public string WeightDifference
        {
            get
            {
                double dif = (WeightVaue - OldWeightValue);
                double difRounded = Math.Round(dif, 2);
               return difRounded.ToString();
            }
        }

        public string DifferenceImage
        {
            get
            {
                string img = "";
                double _weightDifference = double.Parse(WeightDifference);

                if(_weightDifference > 0)
                {
                    img = "ic_up";
                }
                else if(_weightDifference < 0)
                {
                    img = "ic_down";
                }
                else
                {
                    img = "ic_equal";
                }

                return img;
            }
        }

        #region Methods
        public override int GetHashCode()
        {
            return ChildrenWeightId;
        }


        #endregion
    }
}
