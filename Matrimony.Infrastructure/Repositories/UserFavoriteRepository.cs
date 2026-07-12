using Matrimony.Application.Interfaces.Repositories;
using Matrimony.Domain.Entities;
using Matrimony.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Repositories
{
    public class UserFavoriteRepository : IUserFavoriteRepository
    {
        private readonly ApplicationDbContext _context;

        public UserFavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserFavorite favorite)
        {
            await _context.UserFavorites.AddAsync(favorite);
        }

        public void Update(UserFavorite favorite)
        {
            _context.UserFavorites.Update(favorite);
        }

        public async Task<UserFavorite?> GetAsync(
            Guid userId,
            Guid favoriteUserId)
        {
            return await _context.UserFavorites
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.FavoriteUserId == favoriteUserId);
        }

        public async Task<List<UserFavorite>> GetFavoritesAsync(
            Guid userId)
        {
            return await _context.UserFavorites
                .AsNoTracking()
                .Include(x => x.FavoriteUser)
                .Where(x =>
                    x.UserId == userId &&
                    !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        //public async Task SaveChangesAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}
    }
}
