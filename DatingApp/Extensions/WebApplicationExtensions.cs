using Domain.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void MigrateDb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();;
        }

        public static void RegisterSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
