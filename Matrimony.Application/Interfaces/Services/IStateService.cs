using Matrimony.Application.Features.Masters.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Services
{
    public interface IStateService
    {
        Task<List<StateResponse>> GetAllAsync();

        Task<List<StateResponse>> GetByCountryIdAsync(Guid countryId);

        Task<StateResponse?> GetByIdAsync(Guid id);

        Task AddAsync(CreateStateRequest request);

        Task UpdateAsync(UpdateStateRequest request);

        Task DeleteAsync(Guid id);
    }
}
