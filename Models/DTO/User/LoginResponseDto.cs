namespace UserTaskApi.Models.DTO.User
{
    public class LoginResponseDto
    {
        public string JwtToken { get; }

        public LoginResponseDto(string jwtToken)
        {
            JwtToken = jwtToken;
        }
    }
}