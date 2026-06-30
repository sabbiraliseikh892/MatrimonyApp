using Matrimony.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Repositories
{
    public interface IProfilePhotoRepository
    {
        Task<List<ProfilePhoto>> GetByUserProfileIdAsync(Guid userProfileId);

        Task<ProfilePhoto?> GetByIdAsync(Guid photoId);

        Task<ProfilePhoto?> GetPrimaryPhotoAsync(Guid userProfileId);

        Task AddAsync(ProfilePhoto photo);

        Task UpdateAsync(ProfilePhoto photo);

        Task DeleteAsync(ProfilePhoto photo);
    }

}
