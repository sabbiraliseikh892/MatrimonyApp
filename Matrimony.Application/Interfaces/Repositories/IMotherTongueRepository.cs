using Matrimony.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Repositories
{
    public interface IMotherTongueRepository
    {
        Task<List<MotherTongueMaster>> GetAllAsync();

        Task<MotherTongueMaster?> GetByIdAsync(Guid id);

        Task AddAsync(MotherTongueMaster motherTongue);

        Task UpdateAsync(MotherTongueMaster motherTongue);

        Task DeleteAsync(MotherTongueMaster motherTongue);

        Task<bool> ExistsAsync(string name);
    }
}
