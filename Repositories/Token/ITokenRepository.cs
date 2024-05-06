using UserTaskApi.Models.Domain;

namespace UserTaskApi.Repositories.Token
{
    public interface ITokenRepository
    {
        string CreateJWTToken(User user, List<string> roles);
    }
}