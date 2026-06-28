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
    public class CasteRepository : ICasteRepository
    {
        private readonly ApplicationDbContext _context;

        public CasteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CasteMaster>> GetAllAsync()
        {
            return await _context.CasteMasters
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();
        }

        public async Task<CasteMaster?> GetByIdAsync(Guid id)
        {
            return await _context.CasteMasters
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.CasteMasters
                .AnyAsync(x => x.Name == name);
        }

        public async Task AddAsync(CasteMaster caste)
        {
            await _context.CasteMasters.AddAsync(caste);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CasteMaster caste)
        {
            _context.CasteMasters.Update(caste);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CasteMaster caste)
        {
            _context.CasteMasters.Remove(caste);
            await _context.SaveChangesAsync();
        }
    }
}
