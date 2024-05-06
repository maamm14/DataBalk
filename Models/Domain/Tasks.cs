using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace UserTaskApi.Models.Domain
{
    public class TaskDomain
    {
        public int Id { get; internal set; }

        [Required]
        public required string Title { get; set; } = string.Empty;

        [Required]
        public required string Description { get; set; } = string.Empty;

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime CreatedOn { get; internal set; } = DateTime.Now;

        public DateTime UpdatedOn { get; internal set; } = DateTime.Now;

        public string? AssigneeId { get; set; }

        public User? AssignedUser { get; set; }
    }
}