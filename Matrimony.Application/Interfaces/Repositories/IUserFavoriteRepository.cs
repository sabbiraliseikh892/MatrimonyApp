using Matrimony.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Repositories
{
    public interface IUserFavoriteRepository
    {
        // Create
        Task AddAsync(UserFavorite favorite);

        // Update (Soft Delete / Restore)
        void Update(UserFavorite favorite);

        // Get one favorite between two users
        Task<UserFavorite?> GetAsync(
            Guid userId,
            Guid favoriteUserId);

        // Get all favorites of a user
        Task<List<UserFavorite>> GetFavoritesAsync(
            Guid userId);

        // Persist changes
        //Task SaveChangesAsync();
    }
}
