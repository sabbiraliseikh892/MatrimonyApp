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
    public class EducationRepository : IEducationRepository
    {
        private readonly ApplicationDbContext _context;

        public EducationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EducationMaster>> GetAllAsync()
        {
            return await _context.EducationMasters
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();
        }

        public async Task<EducationMaster?> GetByIdAsync(Guid id)
        {
            return await _context.EducationMasters
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.EducationMasters
                .AnyAsync(x => x.Name == name);
        }

        public async Task AddAsync(EducationMaster religion)
        {
            await _context.EducationMasters.AddAsync(religion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EducationMaster education)
        {
            _context.EducationMasters.Update(education);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EducationMaster education)
        {
            _context.EducationMasters.Remove(education);
            await _context.SaveChangesAsync();
        }
    }
}
