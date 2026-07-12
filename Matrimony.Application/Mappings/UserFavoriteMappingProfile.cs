using AutoMapper;
using Matrimony.Application.Features.UserFavorites;
using Matrimony.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Matrimony.Application.Mappings
{
    public class UserFavoriteMappingProfile : Profile
    {
        public UserFavoriteMappingProfile()
        {
            CreateMap<UserFavorite, UserFavoriteResponse>().ForMember(dest => dest.FavoriteId,
                    opt => opt.MapFrom(src => src.Id)).ForMember(dest => dest.AddedOn,
                    opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}
