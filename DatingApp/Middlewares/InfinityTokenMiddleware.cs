using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.Middlewares
{
    public class InfinityTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public InfinityTokenMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            SetUser(context);
            await _next(context);
        }

        private void SetUser(HttpContext context)
        {
            var infToken = _configuration["Authorize"];
            var token = context.Request.Headers["Authorization"];

            if (infToken.IsNullOrEmpty() || token.IsNullOrEmpty())
                return;

            if ("Bearer " + infToken == token)
                context.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.NameIdentifier, "1"),
                    new Claim(ClaimTypes.Email, "admin@admin.com"),
                    new Claim(ClaimTypes.Role,  "admin")
                }));
        }
    }
}
