using System.ComponentModel.DataAnnotations;

namespace UserTaskApi.Models.DTO.Tasks
{
    public class CreateTaskRequestDto
    {
        [Required]
        public required string Title { get; set; } = string.Empty;

        [Required]
        public required string Description { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }
    }
}