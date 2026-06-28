using Matrimony.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Repositories
{
    public interface ICasteRepository
    {
        Task<List<CasteMaster>> GetAllAsync();

        Task<CasteMaster?> GetByIdAsync(Guid id);

        Task AddAsync(CasteMaster caste);

        Task UpdateAsync(CasteMaster caste);

        Task DeleteAsync(CasteMaster caste);

        Task<bool> ExistsAsync(string name);
    }
}
