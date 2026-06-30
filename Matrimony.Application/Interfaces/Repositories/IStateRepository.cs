using Matrimony.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Repositories
{
    public interface IStateRepository
    {
        Task<List<StateMaster>> GetAllAsync();

        Task<List<StateMaster>> GetByCountryIdAsync(Guid countryId);

        Task<StateMaster?> GetByIdAsync(Guid id);

        Task AddAsync(StateMaster state);

        Task UpdateAsync(StateMaster state);

        Task DeleteAsync(StateMaster state);

        Task<bool> ExistsAsync(string name, Guid countryId);
    }
}
