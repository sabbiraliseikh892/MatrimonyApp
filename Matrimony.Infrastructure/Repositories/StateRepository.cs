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
    public class StateRepository : IStateRepository
    {
        private readonly ApplicationDbContext _context;

        public StateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StateMaster>> GetAllAsync()
        {
            return await _context.StateMasters
                .Include(x => x.Country)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();
        }

        public async Task<List<StateMaster>> GetByCountryIdAsync(Guid countryId)
        {
            return await _context.StateMasters
                .Where(x => x.CountryId == countryId)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<StateMaster?> GetByIdAsync(Guid id)
        {
            return await _context.StateMasters
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsAsync(string name, Guid countryId)
        {
            return await _context.StateMasters
                .AnyAsync(x =>
                    x.Name == name &&
                    x.CountryId == countryId);
        }

        public async Task AddAsync(StateMaster state)
        {
            await _context.StateMasters.AddAsync(state);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StateMaster state)
        {
            _context.StateMasters.Update(state);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(StateMaster state)
        {
            _context.StateMasters.Remove(state);

            await _context.SaveChangesAsync();
        }
    }
}
