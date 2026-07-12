using AutoMapper;
using Matrimony.Application.Features.UserProfileViews;
using Matrimony.Application.Interfaces.Persistence;
using Matrimony.Application.Interfaces.Repositories;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Domain.Entities;
using Matrimony.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Services
{
    public class UserProfileViewService : IUserProfileViewService
    {
        private readonly IUserProfileViewRepository _profileViewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserProfileViewService(
            IUserProfileViewRepository profileViewRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _profileViewRepository = profileViewRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task RecordViewAsync(
    Guid viewerUserId,
    Guid viewedUserId)
        {
            if (viewerUserId == viewedUserId)
            {
                return;
            }
            var user =
    await _userRepository.GetByIdAsync(viewedUserId);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }
            var existingView =
    await _profileViewRepository.GetAsync(
        viewerUserId,
        viewedUserId);
            if (existingView != null)
            {
                existingView.ViewedAt = DateTime.UtcNow;

                _profileViewRepository.Update(existingView);

                await _unitOfWork.SaveChangesAsync();

                return;
            }
            var profileView = new UserProfileView
            {
                ViewerUserId = viewerUserId,

                ViewedUserId = viewedUserId,

                ViewedAt = DateTime.UtcNow,

                CreatedAt = DateTime.UtcNow
            };

            await _profileViewRepository.AddAsync(profileView);
            var count =
    await _profileViewRepository.CountAsync(viewerUserId);

            if (count >= 100)
            {
                var oldest =
                    await _profileViewRepository.GetOldestAsync(
                        viewerUserId);

                if (oldest != null)
                {
                    _profileViewRepository.Remove(oldest);
                }
            }
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<List<GetProfileViewResponse>> GetRecentlyViewedAsync(
    Guid viewerUserId)
        {
            var profileViews =
                await _profileViewRepository.GetRecentlyViewedAsync(
                    viewerUserId);

            return _mapper.Map<List<GetProfileViewResponse>>(profileViews);
        }
    }
}
