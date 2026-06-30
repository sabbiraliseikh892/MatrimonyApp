using Matrimony.Application.Features.Masters.Education;
using Matrimony.Application.Features.Masters.Religion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Services
{
    public interface IEducationService
    {
        Task<List<EducationResponse>> GetAllAsync();

        Task<EducationResponse?> GetByIdAsync(Guid id);

        Task AddAsync(CreateEducationRequest request);

        Task UpdateAsync(UpdateEducationRequest request);

        Task DeleteAsync(Guid id);
    }
}
