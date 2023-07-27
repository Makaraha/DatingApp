using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common;
using Common.Exceptions.ServerExceptions;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Services.IdentityServices
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<JwtTokens> AuthenticateByEmail(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            CheckUser(user);

            var result = await _userManager.CheckPasswordAsync(user, password);
            if (!result)
                throw new BadRequestException("Wrong login or password");


        }

        private JwtTokens GenerateJwt(User user)
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
            return new JwtTokens()
            {
                Token = tokenHandler.WriteToken(token)
            };
        }

        private void CheckUser(User? user)
        {
            if (user == null)
                throw new NotFoundException("User not found");
        }
    }
}
