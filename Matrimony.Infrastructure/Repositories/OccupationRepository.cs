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
    public class OccupationRepository : IOccupationRepository
    {
        private readonly ApplicationDbContext _context;

        public OccupationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OccupationMaster>> GetAllAsync()
        {
            return await _context.OccupationMasters
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();
        }

        public async Task<OccupationMaster?> GetByIdAsync(Guid id)
        {
            return await _context.OccupationMasters
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.OccupationMasters
                .AnyAsync(x => x.Name == name);
        }

        public async Task AddAsync(OccupationMaster occupation)
        {
            await _context.OccupationMasters.AddAsync(occupation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OccupationMaster occupation)
        {
            _context.OccupationMasters.Update(occupation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(OccupationMaster occupation)
        {
            _context.OccupationMasters.Remove(occupation);
            await _context.SaveChangesAsync();
        }
    }
}
