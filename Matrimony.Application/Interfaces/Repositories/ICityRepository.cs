using Matrimony.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Repositories
{
    public interface ICityRepository
    {
        Task<List<CityMaster>> GetAllAsync();

        Task<List<CityMaster>> GetByStateIdAsync(Guid stateId);

        Task<CityMaster?> GetByIdAsync(Guid id);

        Task AddAsync(CityMaster city);

        Task UpdateAsync(CityMaster city);

        Task DeleteAsync(CityMaster city);

        Task<bool> ExistsAsync(string name, Guid stateId);
    }
}
