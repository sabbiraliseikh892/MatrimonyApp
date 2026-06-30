using Matrimony.Application.Features.Profile.CreateProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Services
{
    public interface IUserProfileService
    {
        Task<List<ProfileResponse>> GetAllAsync();

        Task<ProfileResponse?> GetByIdAsync(Guid id);

        Task<ProfileResponse?> GetByUserIdAsync(Guid userId);

        Task CreateAsync(Guid userId, CreateProfileRequest request);

        Task UpdateAsync(UpdateProfileRequest request);

        Task DeleteAsync(Guid id);
    }
}
