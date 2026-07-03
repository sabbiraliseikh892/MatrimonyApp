using Matrimony.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Repositories
{
    public interface IPartnerPreferenceRepository
    {
        Task<PartnerPreference?> GetByUserIdAsync(Guid userId);

        Task<PartnerPreference?> GetByIdAsync(Guid id);

        Task AddAsync(PartnerPreference preference);

        Task UpdateAsync(PartnerPreference preference);

        Task DeleteAsync(PartnerPreference preference);
    }
}
