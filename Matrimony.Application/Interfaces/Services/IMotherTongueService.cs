using Matrimony.Application.Features.Masters.MotherTongue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Services
{
    public interface IMotherTongueService
    {
        Task<List<MotherTongueResponse>> GetAllAsync();

        Task<MotherTongueResponse?> GetByIdAsync(Guid id);

        Task AddAsync(CreateMotherTongueRequest request);

        Task UpdateAsync(UpdateMotherTongueRequest request);

        Task DeleteAsync(Guid id);
    }
}
