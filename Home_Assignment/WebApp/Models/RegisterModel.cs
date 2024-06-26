﻿using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
		[Required]
		[Display(Name = "Name")]
		public string Name { get; set; }
		[Required]
		[Display(Name = "Surname")]
		public string Surname { get; set; }
	}
}
