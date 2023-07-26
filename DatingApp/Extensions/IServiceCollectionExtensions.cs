using Domain.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterConnectionString(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(x =>
                x.UseSqlServer(configuration.GetConnectionString("Default")));
        }
    }
}
