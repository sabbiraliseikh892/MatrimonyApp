using Matrimony.Application.Features.UserFavorites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Services
{
    public interface IUserFavoriteService
    {
        Task AddFavoriteAsync(
            Guid currentUserId,
            CreateUserFavoriteRequest request);

        Task RemoveFavoriteAsync(
            Guid currentUserId,
            Guid favoriteUserId);

        Task<List<UserFavoriteResponse>> GetFavoritesAsync(
            Guid currentUserId);

        Task<FavoriteStatusResponse> CheckFavoriteAsync(
            Guid currentUserId,
            Guid favoriteUserId);
    }
}
