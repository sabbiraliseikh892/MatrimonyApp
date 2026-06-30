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
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;
        public UserProfileRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<List<UserProfile>> GetAllAsync()
        {
            return await _context.UserProfiles
                .Include(x => x.User)
                .Include(x => x.Religion)
                .Include(x => x.Caste)
                .Include(x => x.MotherTongue)
                .Include(x => x.Education)
                .Include(x => x.Occupation)
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.City)
                .Include(x => x.Photos)
                .ToListAsync();
        }
        public async Task<UserProfile?> GetByIdAsync(Guid id)
        {
            return await _context.UserProfiles
                .Include(x => x.User)
                .Include(x => x.Religion)
                .Include(x => x.Caste)
                .Include(x => x.MotherTongue)
                .Include(x => x.Education)
                .Include(x => x.Occupation)
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.City)
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<UserProfile?> GetByUserIdAsync(Guid userId)
        {
            return await _context.UserProfiles
                .Include(x => x.User)
                .Include(x => x.Religion)
                .Include(x => x.Caste)
                .Include(x => x.MotherTongue)
                .Include(x => x.Education)
                .Include(x => x.Occupation)
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.City)
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
        public async Task<bool> ExistsAsync(Guid userId)
        {
            return await _context.UserProfiles
                .AnyAsync(x => x.UserId == userId);
        }
        public async Task<string> GenerateProfileIdAsync()
        {
            var lastProfile = await _context.UserProfiles
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (lastProfile == null)
                return "MAT100001";

            var lastNumber = int.Parse(lastProfile.ProfileId.Replace("MAT", ""));

            return $"MAT{(lastNumber + 1):D6}";
        }
        public async Task AddAsync(UserProfile profile)
        {
            await _context.UserProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserProfile profile)
        {
            _context.UserProfiles.Update(profile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserProfile profile)
        {
            _context.UserProfiles.Remove(profile);
            await _context.SaveChangesAsync();
        }
    }
}
