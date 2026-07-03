using Matrimony.Application.Features.UserInterests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Services
{
    public interface IUserInterestService
    {
        Task SendInterestAsync(
            Guid currentUserId,
            CreateUserInterestRequest request);

        Task UpdateInterestStatusAsync(
            Guid currentUserId,
            Guid interestId,
            RespondUserInterestRequest request);

        Task<List<UserInterestListResponse>> GetReceivedAsync(
            Guid currentUserId);

        Task<List<UserInterestListResponse>> GetSentAsync(
            Guid currentUserId);

        Task<UserInterestResponse?> GetByIdAsync(
            Guid interestId,
            Guid currentUserId);
    }
}
