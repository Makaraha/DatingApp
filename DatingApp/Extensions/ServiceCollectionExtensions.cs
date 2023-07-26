using Domain.EF.Context;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
using Repository.Repositories;
using Services.IService;
using Services.Services;

namespace DatingApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterConnectionString(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(x =>
                x.UseSqlServer(configuration.GetConnectionString("Default")));
        }

        public static void RegisterRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IRepository<User, int>, BaseRepository<User, int>>();
        }

        public static void RegisterServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IService<User, int>, BaseService<User, int>>();
        }
    }
}
