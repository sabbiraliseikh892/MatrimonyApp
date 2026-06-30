using Matrimony.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Repositories
{
    public interface ICountryRepository
    {
        Task<List<CountryMaster>> GetAllAsync();

        Task<CountryMaster?> GetByIdAsync(Guid id);

        Task AddAsync(CountryMaster country);

        Task UpdateAsync(CountryMaster country);

        Task DeleteAsync(CountryMaster country);

        Task<bool> ExistsAsync(string name);
    }
}
