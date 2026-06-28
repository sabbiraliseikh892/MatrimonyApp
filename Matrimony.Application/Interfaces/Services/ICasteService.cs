using Matrimony.Application.Features.Masters.Caste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Services
{
    public interface ICasteService
    {
        Task<List<CasteResponse>> GetAllAsync();

        Task<CasteResponse?> GetByIdAsync(Guid id);

        Task AddAsync(CreateCasteRequest request);

        Task UpdateAsync(UpdateCasteRequest request);

        Task DeleteAsync(Guid id);
    }
}
