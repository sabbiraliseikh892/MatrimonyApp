using Matrimony.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Repositories
{
    public interface IUserProfileRepository
    {
        Task<UserProfile?> GetByUserIdAsync(Guid userId);

        Task<UserProfile?> GetByIdAsync(Guid id);

        Task<List<UserProfile>> GetAllAsync();

        Task AddAsync(UserProfile profile);

        Task UpdateAsync(UserProfile profile);

        Task DeleteAsync(UserProfile profile);

        Task<bool> ExistsAsync(Guid userId);

        Task<string> GenerateProfileIdAsync();
    }
}
