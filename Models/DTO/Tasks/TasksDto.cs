using System.ComponentModel.DataAnnotations;

namespace UserTaskApi.Models.DTO.Tasks
{
    public class TasksDto
    {
        [Required]
        public int Id { get; internal set; }

        [Required]
        public required string Title { get; set; } = string.Empty;

        [Required]
        public required string Description { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public DateTime CreatedOn { get; internal set; } = DateTime.Now;

        public DateTime UpdatedOn { get; internal set; } = DateTime.Now;

        public string? AssigneeId { get; set; }
    }
}