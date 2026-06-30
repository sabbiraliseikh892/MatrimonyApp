using Matrimony.Application.Features.Masters.Occupation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Services
{
    public interface IOccupationService
    {
        Task<List<OccupationResponse>> GetAllAsync();

        Task<OccupationResponse?> GetByIdAsync(Guid id);

        Task AddAsync(CreateOccupationRequest request);

        Task UpdateAsync(UpdateOccupationRequest request);

        Task DeleteAsync(Guid id);
    }
}
