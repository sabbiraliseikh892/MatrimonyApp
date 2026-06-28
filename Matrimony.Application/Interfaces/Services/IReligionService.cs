using Matrimony.Application.Features.Masters.Religion;
using Matrimony.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Services
{
    public interface IReligionService
    {
       
            Task<List<ReligionResponse>> GetAllAsync();

            Task<ReligionResponse?> GetByIdAsync(Guid id);

            Task AddAsync(CreateReligionRequest request);

            Task UpdateAsync(UpdateReligionRequest request);

            Task DeleteAsync(Guid id);
        
    }
}
