using Matrimony.Application.Features.Masters.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Services
{
    public interface ICityService
    {
        Task<List<CityResponse>> GetAllAsync();

        Task<List<CityResponse>> GetByStateIdAsync(Guid stateId);

        Task<CityResponse?> GetByIdAsync(Guid id);

        Task AddAsync(CreateCityRequest request);

        Task UpdateAsync(UpdateCityRequest request);

        Task DeleteAsync(Guid id);
    }
}
