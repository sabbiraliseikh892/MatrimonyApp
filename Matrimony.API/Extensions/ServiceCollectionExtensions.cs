using Matrimony.Application.Interfaces.Repositories;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Infrastructure.Authentication;
using Matrimony.Infrastructure.Email;
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
            services.AddScoped<IReligionRepository, ReligionRepository>();
            services.AddScoped<IReligionService, ReligionService>();
            services.AddScoped<ICasteRepository, CasteRepository>();
            services.AddScoped<ICasteService, CasteService>();
            services.AddScoped<IMotherTongueRepository, MotherTongueRepository>();
            services.AddScoped<IMotherTongueService, MotherTongueService>();
            services.AddScoped<IEducationService, EducationService>();
            services.AddScoped<IEducationRepository, EducationRepository>();
            services.AddScoped<IOccupationRepository, OccupationRepository>();
            services.AddScoped<IOccupationService, OccupationService>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserProfileService, UserProfileService>();

            services.AddScoped<IProfilePhotoRepository, ProfilePhotoRepository>();
            services.AddScoped<IProfilePhotoService, ProfilePhotoService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IJwtService, JwtService>();
            return services;
        }
    }
}
