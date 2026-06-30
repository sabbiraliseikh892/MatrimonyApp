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
    public class MotherTongueRepository : IMotherTongueRepository
    {
        private readonly ApplicationDbContext _context;

        public MotherTongueRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MotherTongueMaster>> GetAllAsync()
        {
            return await _context.MotherTongueMasters
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();
        }

        public async Task<MotherTongueMaster?> GetByIdAsync(Guid id)
        {
            return await _context.MotherTongueMasters
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.MotherTongueMasters
                .AnyAsync(x => x.Name == name);
        }

        public async Task AddAsync(MotherTongueMaster motherTongue)
        {
            await _context.MotherTongueMasters.AddAsync(motherTongue);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MotherTongueMaster motherTongue)
        {
            _context.MotherTongueMasters.Update(motherTongue);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MotherTongueMaster motherTongue)
        {
            _context.MotherTongueMasters.Remove(motherTongue);
            await _context.SaveChangesAsync();
        }
    }
}
