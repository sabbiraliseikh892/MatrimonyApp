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
    public class PartnerPreferenceRepository : IPartnerPreferenceRepository
    {
        private readonly ApplicationDbContext _context;

        public PartnerPreferenceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PartnerPreference?> GetByUserIdAsync(Guid userId)
        {
            return await _context.PartnerPreferences
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<PartnerPreference?> GetByIdAsync(Guid id)
        {
            return await _context.PartnerPreferences
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(PartnerPreference preference)
        {
            await _context.PartnerPreferences.AddAsync(preference);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PartnerPreference preference)
        {
            _context.PartnerPreferences.Update(preference);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PartnerPreference preference)
        {
            _context.PartnerPreferences.Remove(preference);
            await _context.SaveChangesAsync();
        }
    }
}
