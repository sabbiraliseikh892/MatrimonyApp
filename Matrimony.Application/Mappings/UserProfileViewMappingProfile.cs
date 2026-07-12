using AutoMapper;
using Matrimony.Application.Features.UserProfileViews;
using Matrimony.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Mappings
{
    public class UserProfileViewMappingProfile : Profile
    {
        public UserProfileViewMappingProfile()
        {
            CreateMap<UserProfileView, GetProfileViewResponse>()
                .ForMember(dest => dest.ViewedUserId,
                    opt => opt.MapFrom(src => src.ViewedUserId))

                .ForMember(dest => dest.ProfileId,
                    opt => opt.MapFrom(src =>
                        src.ViewedUser.Profile.ProfileId))

                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src =>
                        $"{src.ViewedUser.FirstName} {src.ViewedUser.LastName}"))

                .ForMember(dest => dest.Age,
    opt => opt.MapFrom(src =>
        DateTime.Today.Year - src.ViewedUser.Profile.DateOfBirth.Year -
        (src.ViewedUser.Profile.DateOfBirth.Date >
            DateTime.Today.AddYears(
                -(DateTime.Today.Year - src.ViewedUser.Profile.DateOfBirth.Year))
            ? 1 : 0)))

                .ForMember(dest => dest.HeightFeet,
                    opt => opt.MapFrom(src =>
                        src.ViewedUser.Profile.HeightFeet))

                .ForMember(dest => dest.HeightInches,
                    opt => opt.MapFrom(src =>
                        src.ViewedUser.Profile.HeightInches))

                .ForMember(dest => dest.City,
                    opt => opt.MapFrom(src =>
                        src.ViewedUser.Profile.City.Name))

                .ForMember(dest => dest.Education,
                    opt => opt.MapFrom(src =>
                        src.ViewedUser.Profile.Education.Name))

                .ForMember(dest => dest.Occupation,
                    opt => opt.MapFrom(src =>
                        src.ViewedUser.Profile.Occupation.Name))

                .ForMember(dest => dest.PrimaryPhotoUrl,
                    opt => opt.MapFrom(src =>
                        src.ViewedUser.Profile.Photos
                            .Where(p => p.IsPrimary)
                            .Select(p => p.PhotoUrl)
                            .FirstOrDefault()))

                .ForMember(dest => dest.ViewedAt,
                    opt => opt.MapFrom(src => src.ViewedAt));
        }
    }
}
