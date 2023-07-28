using Common;
using Common.Exceptions.ServerExceptions;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Repository.IRepository;

namespace Services.IdentityServices
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<RefreshToken, int> _refreshTokenRepository;

        public JwtService(IConfiguration configuration, UserManager<User> userManager, IRepository<RefreshToken, int> refreshTokenRepository)
        {
            _configuration = configuration;
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
        }


        public async Task<JwtTokens> AuthenticateByEmail(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            CheckUser(user);

            var result = await _userManager.CheckPasswordAsync(user, password);
            if (!result)
                throw new BadRequestException("Wrong login or password");

            var tokens = GenerateJwt(user);
            await SaveRefreshTokenAsync(tokens.RefreshToken, user);

            return tokens;
        }

        public async Task<JwtTokens> Refresh(string accessToken, string refreshToken)
        {
            var claims = GetPrincipalFromExpiredToken(accessToken);
            var userName = claims.Identity?.Name;

            if (userName == null)
                throw new BadRequestException("Wrong token");

            var refToken = await _refreshTokenRepository.GetAllAsQuery()
                .FirstOrDefaultAsync(x => x.UserName == userName && x.Token == refreshToken);
            if (refToken == null)
                throw new BadRequestException("Refresh token not found. Try to sign in again");

            var user = await _userManager.FindByNameAsync(userName);
            var newTokens = GenerateJwt(user);
            await SaveRefreshTokenAsync(newTokens.RefreshToken, user);

            return newTokens;
        }

        private async Task SaveRefreshTokenAsync(string token, User user)
        {
            var entity = new RefreshToken()
            {
                Token = token,
                UserName = user.UserName
            };

            var oldTokes = await _refreshTokenRepository.GetAllAsQuery()
                .Where(x => x.UserName == user.UserName).ToListAsync();
            _refreshTokenRepository.RemoveRange(oldTokes);

            entity.CreatedDateUtc = DateTime.UtcNow;
            entity.LastModifiedDateUtc = DateTime.UtcNow;

            await _refreshTokenRepository.AddAsync(entity);
            await _refreshTokenRepository.SaveChangesAsync();
        }

        private JwtTokens GenerateJwt(User user)
        {
            return new JwtTokens()
            {
                AccessToken = GenerateAccessToken(user),
                RefreshToken = GenerateRefreshToken()
            };
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new BadRequestException("Invalid token");
            }

            return principal;
        }

        private string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),

                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWT:AcessTokenLifeTimeInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private void CheckUser(User? user)
        {
            if (user == null)
                throw new NotFoundException("User not found");
        }
    }
}
