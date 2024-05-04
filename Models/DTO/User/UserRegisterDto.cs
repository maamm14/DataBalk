using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserTaskApi.Models.DTO.User
{
    public class UserRegisterDto
    {
        public required string UserName { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Please provide a password")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]

        public required string[] Roles { get; set; }
        public required string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
    }
}