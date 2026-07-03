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
    public class UserInterestRepository : IUserInterestRepository
    {
        private readonly ApplicationDbContext _context;

        public UserInterestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserInterest interest)
        {
            await _context.UserInterests.AddAsync(interest);
        }

        public void Update(UserInterest interest)
        {
            _context.UserInterests.Update(interest);
        }

        public async Task<UserInterest?> GetByIdAsync(Guid interestId)
        {
            return await _context.UserInterests
                .Include(x => x.FromUser)
                .Include(x => x.ToUser)
                .FirstOrDefaultAsync(x =>
                    x.Id == interestId &&
                    !x.IsDeleted);
        }

        public async Task<UserInterest?> GetBetweenUsersAsync(
            Guid user1,
            Guid user2)
        {
            return await _context.UserInterests
                .FirstOrDefaultAsync(x =>
                    (
                        (x.FromUserId == user1 &&
                         x.ToUserId == user2)
                        ||
                        (x.FromUserId == user2 &&
                         x.ToUserId == user1)
                    )
                    && !x.IsDeleted);
        }

        public async Task<List<UserInterest>> GetReceivedAsync(Guid userId)
        {
            return await _context.UserInterests
                .AsNoTracking()
                .Include(x => x.FromUser)
                .Where(x =>
                    x.ToUserId == userId &&
                    !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<UserInterest>> GetSentAsync(Guid userId)
        {
            return await _context.UserInterests
                .AsNoTracking()
                .Include(x => x.ToUser)
                .Where(x =>
                    x.FromUserId == userId &&
                    !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
