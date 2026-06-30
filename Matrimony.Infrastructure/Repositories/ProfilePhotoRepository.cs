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
    public class ProfilePhotoRepository : IProfilePhotoRepository
    {
        private readonly ApplicationDbContext _context;
        public ProfilePhotoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProfilePhoto>> GetByUserProfileIdAsync(Guid userProfileId)
        {
            return await _context.ProfilePhotos
                .Where(x => x.UserProfileId == userProfileId)
                .OrderByDescending(x => x.IsPrimary)
                .ThenByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
        public async Task<ProfilePhoto?> GetByIdAsync(Guid photoId)
        {
            return await _context.ProfilePhotos
                .FirstOrDefaultAsync(x => x.Id == photoId);
        }
        public async Task<ProfilePhoto?> GetPrimaryPhotoAsync(Guid userProfileId)
        {
            return await _context.ProfilePhotos
                .FirstOrDefaultAsync(x =>
                    x.UserProfileId == userProfileId &&
                    x.IsPrimary);
        }
        public async Task AddAsync(ProfilePhoto photo)
        {
            await _context.ProfilePhotos.AddAsync(photo);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(ProfilePhoto photo)
        {
            _context.ProfilePhotos.Update(photo);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(ProfilePhoto photo)
        {
            _context.ProfilePhotos.Remove(photo);
            await _context.SaveChangesAsync();
        }
    }
}

