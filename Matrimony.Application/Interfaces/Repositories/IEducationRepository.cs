using Matrimony.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Repositories
{
    public interface IEducationRepository
    {
        Task<List<EducationMaster>> GetAllAsync();

        Task<EducationMaster?> GetByIdAsync(Guid id);

        Task AddAsync(EducationMaster education);

        Task UpdateAsync(EducationMaster education);

        Task DeleteAsync(EducationMaster education);

        Task<bool> ExistsAsync(string name);
    }
}
