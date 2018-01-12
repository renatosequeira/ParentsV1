﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.Locations
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("Ubication_Description_Index", IsUnique = true)]
        public string Description { get; set; }

        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string Address { get; set; }

        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string Phone { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

    }
}
