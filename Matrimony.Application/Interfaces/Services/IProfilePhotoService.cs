using Matrimony.Application.Features.ProfilePhoto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Services
{
    public interface IProfilePhotoService
    {
        Task UploadAsync(Guid userId, UploadProfilePhotoRequest request);

        Task<List<ProfilePhotoResponse>> GetMyPhotosAsync(Guid userId);

        Task SetPrimaryAsync(Guid userId, Guid photoId);

        Task DeleteAsync(Guid userId, Guid photoId);
    }
}
