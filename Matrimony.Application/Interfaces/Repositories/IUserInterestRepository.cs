using Matrimony.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Repositories
{
    public interface IUserInterestRepository
    {
        Task AddAsync(UserInterest interest);

        void Update(UserInterest interest);

        Task<UserInterest?> GetByIdAsync(Guid interestId);

        Task<UserInterest?> GetBetweenUsersAsync(
            Guid user1,
            Guid user2);

        Task<List<UserInterest>> GetReceivedAsync(Guid userId);

        Task<List<UserInterest>> GetSentAsync(Guid userId);

        Task SaveChangesAsync();
    }
}
