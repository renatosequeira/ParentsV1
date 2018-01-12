using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Models.Sistema
{
    public class Location
    {
        public int LocationId { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
