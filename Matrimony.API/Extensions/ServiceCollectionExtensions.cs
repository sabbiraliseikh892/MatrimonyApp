using Matrimony.Application.Interfaces.Repositories;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Infrastructure.Authentication;
using Matrimony.Infrastructure.Repositories;
using Matrimony.Infrastructure.Services;
using Matrimony.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Matrimony.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection
        AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options =>
                options.UseSqlServer(
                    configuration.GetConnectionString(
                        "DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}
