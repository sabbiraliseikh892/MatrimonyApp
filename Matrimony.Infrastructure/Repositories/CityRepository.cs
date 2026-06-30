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
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CityMaster>> GetAllAsync()
        {
            return await _context.CityMasters
                .Include(x => x.State)
                .ThenInclude(x => x.Country)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();
        }

        public async Task<List<CityMaster>> GetByStateIdAsync(Guid stateId)
        {
            return await _context.CityMasters
                .Where(x => x.StateId == stateId)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<CityMaster?> GetByIdAsync(Guid id)
        {
            return await _context.CityMasters
                .Include(x => x.State)
                .ThenInclude(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsAsync(string name, Guid stateId)
        {
            return await _context.CityMasters
                .AnyAsync(x => x.Name == name && x.StateId == stateId);
        }

        public async Task AddAsync(CityMaster city)
        {
            await _context.CityMasters.AddAsync(city);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CityMaster city)
        {
            _context.CityMasters.Update(city);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CityMaster city)
        {
            _context.CityMasters.Remove(city);
            await _context.SaveChangesAsync();
        }
    }
}
