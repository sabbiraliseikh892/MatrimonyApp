using Matrimony.Application.Interfaces.Repositories;
using Matrimony.Domain.Entities.Masters;
using Matrimony.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Repositories
{
    public class ReligionRepository : IReligionRepository
    {
        private readonly ApplicationDbContext _context;

        public ReligionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReligionMaster>> GetAllAsync()
        {
            return await _context.ReligionMasters
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();
        }

        public async Task<ReligionMaster?> GetByIdAsync(Guid id)
        {
            return await _context.ReligionMasters
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.ReligionMasters
                .AnyAsync(x => x.Name == name);
        }

        public async Task AddAsync(ReligionMaster religion)
        {
            await _context.ReligionMasters.AddAsync(religion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReligionMaster religion)
        {
            _context.ReligionMasters.Update(religion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ReligionMaster religion)
        {
            _context.ReligionMasters.Remove(religion);
            await _context.SaveChangesAsync();
        }
    }
}
