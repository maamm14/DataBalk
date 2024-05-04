using System.ComponentModel.DataAnnotations;

namespace UserTaskApi.Models.DTO.User
{
    public class UserLoginRequestDto
    {
        [DataType(DataType.EmailAddress)]
        public required string Username { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}