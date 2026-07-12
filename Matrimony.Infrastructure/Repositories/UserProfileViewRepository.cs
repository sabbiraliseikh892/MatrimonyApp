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
    public class UserProfileViewRepository : IUserProfileViewRepository
    {
        private readonly ApplicationDbContext _context;

        public UserProfileViewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserProfileView profileView)
        {
            await _context.UserProfileViews.AddAsync(profileView);
        }

        public void Update(UserProfileView profileView)
        {
            _context.UserProfileViews.Update(profileView);
        }

        public async Task<UserProfileView?> GetAsync(
            Guid viewerUserId,
            Guid viewedUserId)
        {
            return await _context.UserProfileViews
                .FirstOrDefaultAsync(x =>
                    x.ViewerUserId == viewerUserId &&
                    x.ViewedUserId == viewedUserId);
        }

        public async Task<List<UserProfileView>> GetRecentlyViewedAsync(
    Guid viewerUserId)
        {
            return await _context.UserProfileViews
                .AsNoTracking()
                .Include(x => x.ViewedUser)
                    .ThenInclude(x => x.Profile)
                        .ThenInclude(x => x.City)
                .Include(x => x.ViewedUser)
                    .ThenInclude(x => x.Profile)
                        .ThenInclude(x => x.Education)
                .Include(x => x.ViewedUser)
                    .ThenInclude(x => x.Profile)
                        .ThenInclude(x => x.Occupation)
                .Include(x => x.ViewedUser)
                    .ThenInclude(x => x.Profile)
                        .ThenInclude(x => x.Photos)
                .Where(x =>
                    x.ViewerUserId == viewerUserId &&
                    !x.IsDeleted)
                .OrderByDescending(x => x.ViewedAt)
                .ToListAsync();
        }

        public async Task<int> CountAsync(Guid viewerUserId)
        {
            return await _context.UserProfileViews
                .CountAsync(x =>
                    x.ViewerUserId == viewerUserId &&
                    !x.IsDeleted);
        }

        public async Task<UserProfileView?> GetOldestAsync(
            Guid viewerUserId)
        {
            return await _context.UserProfileViews
                .Where(x =>
                    x.ViewerUserId == viewerUserId &&
                    !x.IsDeleted)
                .OrderBy(x => x.ViewedAt)
                .FirstOrDefaultAsync();
        }

        public void Remove(UserProfileView profileView)
        {
            _context.UserProfileViews.Remove(profileView);
        }
    }
}
