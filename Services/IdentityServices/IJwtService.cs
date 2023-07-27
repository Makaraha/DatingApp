using Common;
using Domain.Entities.Identity;

namespace Services.IdentityServices
{
    public interface IJwtService
    {
        Task<JwtTokens> AuthenticateByEmail(string username, string password);
    }
}
