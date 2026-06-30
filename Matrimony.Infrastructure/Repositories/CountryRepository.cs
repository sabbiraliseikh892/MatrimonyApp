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
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CountryMaster>> GetAllAsync()
        {
            return await _context.CountryMasters
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();
        }

        public async Task<CountryMaster?> GetByIdAsync(Guid id)
        {
            return await _context.CountryMasters
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.CountryMasters
                .AnyAsync(x => x.Name == name);
        }

        public async Task AddAsync(CountryMaster country)
        {
            await _context.CountryMasters.AddAsync(country);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CountryMaster country)
        {
            _context.CountryMasters.Update(country);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CountryMaster country)
        {
            _context.CountryMasters.Remove(country);

            await _context.SaveChangesAsync();
        }
    }
}
