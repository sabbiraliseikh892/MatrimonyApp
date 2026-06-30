using Matrimony.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Repositories
{
    public interface IOccupationRepository
    {
        Task<List<OccupationMaster>> GetAllAsync();

        Task<OccupationMaster?> GetByIdAsync(Guid id);

        Task AddAsync(OccupationMaster occupation);

        Task UpdateAsync(OccupationMaster occupation);

        Task DeleteAsync(OccupationMaster occupation);

        Task<bool> ExistsAsync(string name);
    }
}
