using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UserTaskApi.Models.Domain
{
    public class User : IdentityUser
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }
    }
}