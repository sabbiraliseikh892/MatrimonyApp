using Matrimony.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Repositories
{
    public interface IReligionRepository
    {
        Task<List<ReligionMaster>> GetAllAsync();

        Task<ReligionMaster?> GetByIdAsync(Guid id);

        Task AddAsync(ReligionMaster religion);

        Task UpdateAsync(ReligionMaster religion);

        Task DeleteAsync(ReligionMaster religion);

        Task<bool> ExistsAsync(string name);
    }
}
