﻿using Common;

namespace Services.IdentityServices.Interfaces
{
    public interface IJwtService
    {
        Task<JwtTokens> AuthenticateByEmail(string email, string password);

        Task<JwtTokens> Refresh(string accessToken, string refreshToken);
    }
}
