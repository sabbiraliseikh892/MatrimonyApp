using Matrimony.Application.Features.Masters.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Services
{
    public interface ICountryService
    {
        Task<List<CountryResponse>> GetAllAsync();

        Task<CountryResponse?> GetByIdAsync(Guid id);

        Task AddAsync(CreateCountryRequest request);

        Task UpdateAsync(UpdateCountryRequest request);

        Task DeleteAsync(Guid id);
    }
}
