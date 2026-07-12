using Matrimony.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Repositories
{
    public interface IUserProfileViewRepository
    {
        // Create
        Task AddAsync(UserProfileView profileView);

        // Update
        void Update(UserProfileView profileView);

        // Get existing view between two users
        Task<UserProfileView?> GetAsync(
            Guid viewerUserId,
            Guid viewedUserId);

        // Get recently viewed profiles
        Task<List<UserProfileView>> GetRecentlyViewedAsync(
            Guid viewerUserId);

        // Number of viewed profiles
        Task<int> CountAsync(Guid viewerUserId);

        // Get oldest viewed profile
        Task<UserProfileView?> GetOldestAsync(
            Guid viewerUserId);

        // Remove oldest record
        void Remove(UserProfileView profileView);
    }
}
