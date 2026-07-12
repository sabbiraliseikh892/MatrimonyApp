using Matrimony.Application.Features.UserInterests;
using Matrimony.Application.Interfaces.Repositories;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Domain.Entities;
using Matrimony.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Services
{
    public class UserInterestService : IUserInterestService
    {
        private readonly IUserInterestRepository _interestRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _profileRepository;
        public UserInterestService(
            IUserInterestRepository interestRepository,
            IUserRepository userRepository,
            IUserProfileRepository profileRepository)
        {
            _interestRepository = interestRepository;
            _userRepository = userRepository;
            _profileRepository = profileRepository;
        }
        public async Task SendInterestAsync(
    Guid currentUserId,
    CreateUserInterestRequest request)
        {
            if (currentUserId == request.ToUserId)
            {
                throw new Exception(
                    "You cannot send interest to yourself.");
            }
            var receiver =
    await _userRepository.GetByIdAsync(request.ToUserId);

            if (receiver == null)
            {
                throw new Exception("User not found.");
            }
            var senderProfile =
    await _profileRepository.GetProfileByUserIdAsync(currentUserId);

            if (senderProfile == null)
            {
                throw new Exception(
                    "Complete your profile before sending interests.");
            }
            var receiverProfile =
    await _profileRepository.GetProfileByUserIdAsync(request.ToUserId);

            if (receiverProfile == null)
            {
                throw new Exception(
                    "Receiver profile not found.");
            }
            var existingInterest =
    await _interestRepository.GetBetweenUsersAsync(
        currentUserId,
        request.ToUserId);
            if (existingInterest != null)
            {
                switch (existingInterest.Status)
                {
                    case InterestStatus.Pending:

                        throw new Exception(
                            "An interest request is already pending.");

                    case InterestStatus.Accepted:

                        throw new Exception(
                            "You are already connected.");

                    case InterestStatus.Cancelled:

                        break;

                    case InterestStatus.Rejected:

                        throw new Exception(
                            "Interest request was rejected.");

                    default:

                        throw new Exception(
                            "Invalid interest status.");
                }
            }
            var interest = new UserInterest
            {
                FromUserId = currentUserId,

                ToUserId = request.ToUserId,

                InitialMessage = request.InitialMessage,

                Status = InterestStatus.Pending,

                CreatedAt = DateTime.UtcNow
            };
            await _interestRepository.AddAsync(interest);

            await _interestRepository.SaveChangesAsync();
        }
        public async Task UpdateInterestStatusAsync(
    Guid currentUserId,
    Guid interestId,
    RespondUserInterestRequest request)
        {
            var interest = await _interestRepository.GetByIdAsync(interestId);

            if (interest == null)
                throw new Exception("Interest request not found.");

            //-------------------------------------------------------
            // Only receiver can respond
            //-------------------------------------------------------

            if (interest.ToUserId != currentUserId)
                throw new Exception("You are not authorized to respond to this request.");

            //-------------------------------------------------------
            // Only Pending request can be responded
            //-------------------------------------------------------

            if (interest.Status != InterestStatus.Pending)
                throw new Exception("This request has already been processed.");

            //-------------------------------------------------------
            // Only Accepted or Rejected
            //-------------------------------------------------------

            if (request.Status != InterestStatus.Accepted &&
                request.Status != InterestStatus.Rejected)
            {
                throw new Exception("Invalid status.");
            }

            interest.Status = request.Status;

            interest.ResponseReason = request.ResponseReason;

            interest.RespondedAt = DateTime.UtcNow;

            _interestRepository.Update(interest);

            await _interestRepository.SaveChangesAsync();
        }
        public async Task<List<UserInterestListResponse>> GetReceivedAsync(
    Guid currentUserId)
        {
            var interests =
                await _interestRepository.GetReceivedAsync(currentUserId);

            return interests.Select(x => new UserInterestListResponse
            {
                InterestId = x.Id,

                UserId = x.FromUserId,

                Status = x.Status,

                SentOn = x.CreatedAt
            }).ToList();
        }
        public async Task<List<UserInterestListResponse>> GetSentAsync(
    Guid currentUserId)
        {
            var interests =
                await _interestRepository.GetSentAsync(currentUserId);

            return interests.Select(x => new UserInterestListResponse
            {
                InterestId = x.Id,

                UserId = x.ToUserId,

                Status = x.Status,

                SentOn = x.CreatedAt
            }).ToList();
        }
        public async Task<UserInterestResponse?> GetByIdAsync(
    Guid interestId,
    Guid currentUserId)
        {
            var interest =
                await _interestRepository.GetByIdAsync(interestId);

            if (interest == null)
                return null;

            //-------------------------------------------------------
            // Security
            //-------------------------------------------------------

            if (interest.FromUserId != currentUserId &&
                interest.ToUserId != currentUserId)
            {
                throw new Exception("Unauthorized.");
            }

            return new UserInterestResponse
            {
                Id = interest.Id,

                FromUserId = interest.FromUserId,

                ToUserId = interest.ToUserId,

                InitialMessage = interest.InitialMessage,

                ResponseReason = interest.ResponseReason,

                Status = interest.Status,

                SentOn = interest.CreatedAt,

                RespondedAt = interest.RespondedAt
            };
        }
    }
}
