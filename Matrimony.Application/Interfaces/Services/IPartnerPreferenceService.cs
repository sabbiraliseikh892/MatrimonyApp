using Matrimony.Application.Features.PartnerPreference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Services
{
    public interface IPartnerPreferenceService
    {
        Task CreateAsync(Guid userId, CreatePartnerPreferenceRequest request);

        Task UpdateAsync(Guid userId, UpdatePartnerPreferenceRequest request);

        Task<PartnerPreferenceResponse?> GetMyPreferenceAsync(Guid userId);

        Task DeleteAsync(Guid userId);
       
    }
}
