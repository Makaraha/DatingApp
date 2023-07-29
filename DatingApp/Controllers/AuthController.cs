using Common;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IdentityServices.Interfaces;

namespace DatingApp.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost, Route("auth")]
        public async Task<JwtTokens> Auth(AuthDto.AuthByEmail principles)
        {
            return await _jwtService.AuthenticateByEmail(principles.Email, principles.Password);
        }

        [HttpPost, Route("auth/refresh")]
        public async Task<JwtTokens> Refresh(JwtTokens oldTokens)
        {
            return await _jwtService.Refresh(oldTokens.AccessToken, oldTokens.RefreshToken);
        }
    }
}
