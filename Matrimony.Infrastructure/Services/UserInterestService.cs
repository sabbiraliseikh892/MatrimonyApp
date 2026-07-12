using Matrimony.Application.Features.Notifications;
using Matrimony.Application.Features.UserInterests;
using Matrimony.Application.Interfaces.Repositories;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Domain.Entities;
using Matrimony.Domain.Enums;
using Matrimony.Shared.Constants;
using Matrimony.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Services
{
    public class UserInterestService : IUserInterestService
    {
        private readonly IUserInterestRepository _interestRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _profileRepository;
        private readonly INotificationService _notificationService;
        public UserInterestService(
            IUserInterestRepository interestRepository,
            IUserRepository userRepository,
            IUserProfileRepository profileRepository, INotificationService notificationService)
        {
            _interestRepository = interestRepository;
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _notificationService = notificationService;
        }
        public async Task SendInterestAsync(
    Guid currentUserId,
    CreateUserInterestRequest request)
        {
            if (currentUserId == request.ToUserId)
                throw new BusinessException("You cannot send interest to yourself.");

            var sender =
                await _userRepository.GetByIdAsync(currentUserId)
                ?? throw new NotFoundException("Sender not found.");

            var receiver =
                await _userRepository.GetByIdAsync(request.ToUserId)
                ?? throw new NotFoundException("Receiver not found.");

            var senderProfile =
                await _profileRepository.GetProfileByUserIdAsync(currentUserId)
                ?? throw new BusinessException(
                    "Complete your profile before sending interests.");

            var receiverProfile =
                await _profileRepository.GetProfileByUserIdAsync(request.ToUserId)
                ?? throw new NotFoundException("Receiver profile not found.");

            var existing =
                await _interestRepository.GetBetweenUsersAsync(
                    currentUserId,
                    request.ToUserId);

            if (existing != null)
            {
                switch (existing.Status)
                {
                    case InterestStatus.Pending:
                        throw new BusinessException(
                            "An interest request is already pending.");

                    case InterestStatus.Accepted:
                        throw new BusinessException(
                            "You are already connected.");

                    case InterestStatus.Rejected:
                        throw new BusinessException(
                            "Interest request was rejected.");

                    case InterestStatus.Cancelled:
                        break;
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

            //-------------------------------------------------------
            // Real-time Notification
            //-------------------------------------------------------

            await _notificationService.SendAsync(
                request.ToUserId,
                new NotificationResponse
                {
                    UserId = request.ToUserId,

                    Title = "New Interest Received",

                    Message =
                        $"{sender.FirstName} {sender.LastName} has shown interest in your profile.",

                    Type = NotificationTypes.InterestReceived,

                    CreatedAt = DateTime.UtcNow,

                    Data = JsonSerializer.Serialize(new
                    {
                        InterestId = interest.Id,
                        FromUserId = currentUserId
                    })
                });
        }
        public async Task UpdateInterestStatusAsync(
    Guid currentUserId,
    Guid interestId,
    RespondUserInterestRequest request)
        {
            var interest =
                await _interestRepository.GetByIdAsync(interestId)
                ?? throw new NotFoundException("Interest request not found.");

            if (interest.ToUserId != currentUserId)
                throw new UnauthorizedException(
                    "You are not authorized to respond.");

            if (interest.Status != InterestStatus.Pending)
                throw new BusinessException(
                    "This request has already been processed.");

            if (request.Status != InterestStatus.Accepted &&
                request.Status != InterestStatus.Rejected)
            {
                throw new BusinessException(
                    "Invalid status.");
            }

            interest.Status = request.Status;
            interest.ResponseReason = request.ResponseReason;
            interest.RespondedAt = DateTime.UtcNow;

            _interestRepository.Update(interest);

            await _interestRepository.SaveChangesAsync();

            //-------------------------------------------------------
            // Notification
            //-------------------------------------------------------

            if (request.Status == InterestStatus.Accepted)
            {
                await _notificationService.SendAsync(
                    interest.FromUserId,
                    new NotificationResponse
                    {
                        UserId = interest.FromUserId,

                        Title = "Interest Accepted",

                        Message =
                            "Your interest request has been accepted.",

                        Type = NotificationTypes.InterestAccepted,

                        CreatedAt = DateTime.UtcNow,

                        Data = JsonSerializer.Serialize(new
                        {
                            InterestId = interest.Id
                        })
                    });
            }

            if (request.Status == InterestStatus.Rejected)
            {
                await _notificationService.SendAsync(
                    interest.FromUserId,
                    new NotificationResponse
                    {
                        UserId = interest.FromUserId,

                        Title = "Interest Rejected",

                        Message =
                            "Your interest request has been rejected.",

                        Type = NotificationTypes.InterestRejected,

                        CreatedAt = DateTime.UtcNow,

                        Data = JsonSerializer.Serialize(new
                        {
                            InterestId = interest.Id
                        })
                    });
            }
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
                throw new UnauthorizedException("Unauthorized.");
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
