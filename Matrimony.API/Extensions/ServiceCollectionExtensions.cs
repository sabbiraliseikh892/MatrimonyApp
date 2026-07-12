using AutoMapper;
using Matrimony.Application.Interfaces.Dapper;
using Matrimony.Application.Interfaces.Persistence;
using Matrimony.Application.Interfaces.Repositories;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Application.Mappings;
using Matrimony.Infrastructure.Authentication;
using Matrimony.Infrastructure.Dapper;
using Matrimony.Infrastructure.Dapper.Sql;
using Matrimony.Infrastructure.Email;
using Matrimony.Infrastructure.Persistence;
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

            services.AddSingleton<DapperContext>();
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
            services.AddScoped<IPartnerPreferenceRepository, PartnerPreferenceRepository>();
            services.AddScoped<IPartnerPreferenceService, PartnerPreferenceService>();
            services.AddScoped<ISearchReadRepository, SearchReadRepository>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddSingleton<SearchProfileSqlBuilder>();
            services.AddScoped<IProfilePhotoRepository, ProfilePhotoRepository>();
            services.AddScoped<IProfilePhotoService, ProfilePhotoService>();
            services.AddScoped<IUserInterestRepository, UserInterestRepository>();
            services.AddScoped<IUserInterestService, UserInterestService>();
            services.AddScoped<IUserFavoriteRepository, UserFavoriteRepository>();
            services.AddScoped<IUserFavoriteService, UserFavoriteService>();
            services.AddAutoMapper(typeof(UserProfileMappingProfile).Assembly);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserProfileViewRepository, UserProfileViewRepository>();
            services.AddScoped<RecommendationSqlBuilder>();
            services.AddScoped<IRecommendationRepository, RecommendationRepository>();
            services.AddScoped<IRecommendationService, RecommendationService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IJwtService, JwtService>();
            return services;
        }
    }
}
