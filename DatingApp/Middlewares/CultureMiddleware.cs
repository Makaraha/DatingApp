using System.Globalization;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.Middlewares
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;
        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var culture = context.Request.Headers["culture"].FirstOrDefault();

            if (culture.IsNullOrEmpty())
                culture = context.Request.Query["culture"].FirstOrDefault();

            if(!culture.IsNullOrEmpty())
                CultureInfo.CurrentCulture = new CultureInfo(culture);

            await _next(context);
        }
    }
}
