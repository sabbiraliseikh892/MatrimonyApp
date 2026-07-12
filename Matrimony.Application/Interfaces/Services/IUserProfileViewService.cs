using Matrimony.Application.Features.UserProfileViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Services
{
    public interface IUserProfileViewService
    {
        /// <summary>
        /// Called automatically when a user opens another user's profile.
        /// </summary>
        Task RecordViewAsync(
            Guid viewerUserId,
            Guid viewedUserId);

        /// <summary>
        /// Returns recently viewed profiles.
        /// </summary>
        Task<List<GetProfileViewResponse>> GetRecentlyViewedAsync(
            Guid viewerUserId);
    }
}
