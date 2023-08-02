using Domain.EF.Context;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Entities.Translations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.IRepository;
using Repository.Repositories;
using Services.IdentityServices;
using Services.IdentityServices.Interfaces;
using Services.IService;
using Services.Services;
using System.Text;

namespace DatingApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterConnectionString(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("Default"));
#if DEBUG
                x.LogTo(Console.WriteLine, LogLevel.Information);
#endif
            });
        }

        public static void RegisterIdentity(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddIdentity<User, Role>(x =>
                {
                    x.User.RequireUniqueEmail = true;

                    x.Password.RequireDigit = false;
                    x.Password.RequireLowercase = false;
                    x.Password.RequireNonAlphanumeric = false;
                    x.Password.RequireUppercase = false;
                    x.Password.RequiredLength = 1;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        public static void RegisterRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IRepository<RefreshToken, int>, BaseRepository<RefreshToken, int>>();
            serviceCollection.AddTransient<ITranslationHasRepository<Gender, GenderTranslation>, TranslationHasRepository<Gender, GenderTranslation>>();
            serviceCollection
                .AddTransient<ITranslationHasRepository<Interest, InterestTranslation>,
                    TranslationHasRepository<Interest, InterestTranslation>>();
        }

        public static void RegisterServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService<User, int>, UserService>();
            serviceCollection.AddTransient<IJwtService, JwtService>();
            serviceCollection.AddTransient<ITranslationHasService<Gender>, TranslationHasService<Gender, GenderTranslation>>();
            serviceCollection
                .AddTransient<ITranslationHasService<Interest>, TranslationHasService<Interest, InterestTranslation>>();
        }

        public static void RegisterJWT(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

            serviceCollection.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        }

        public static void RegisterSwagger(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(x =>
            {
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            serviceCollection.ConfigureSwaggerGen(x =>
            {
                x.CustomSchemaIds(x => x.FullName);
            });
        }
    }
}
