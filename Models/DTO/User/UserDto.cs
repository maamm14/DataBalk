namespace UserTaskApi.Models.DTO.User
{
    public class UserDto
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required string UserName { get; set; }

        public required string[] Roles { get; set; }


    }
}