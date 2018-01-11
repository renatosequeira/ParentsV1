﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parents.Domain.Sistema
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        public int UserType { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("User_Email_Index", IsUnique = true)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(20, ErrorMessage = "The field {0} must be have betwen {2} and {1} characters lenght.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [NotMapped] //não mapeia campo para a base de dados
        public string Password { get; set; }
    }
}