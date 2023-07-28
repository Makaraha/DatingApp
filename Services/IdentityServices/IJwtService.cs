using Common;

namespace Services.IdentityServices
{
    public interface IJwtService
    {
        Task<JwtTokens> AuthenticateByEmail(string email, string password);

        Task<JwtTokens> Refresh(string accessToken, string refreshToken);
    }
}
