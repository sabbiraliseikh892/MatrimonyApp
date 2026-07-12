using AutoMapper;
using Matrimony.Application.Features.UserFavorites;
using Matrimony.Application.Interfaces.Persistence;
using Matrimony.Application.Interfaces.Repositories;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Services
{
    public class UserFavoriteService : IUserFavoriteService
    {
        private readonly IUserFavoriteRepository _favoriteRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserFavoriteService(
            IUserFavoriteRepository favoriteRepository,
            IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _favoriteRepository = favoriteRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddFavoriteAsync(
    Guid currentUserId,
    CreateUserFavoriteRequest request)
        {
            if (currentUserId == request.FavoriteUserId)
            {
                throw new Exception(
                    "You cannot favorite yourself.");
            }
            var favoriteUser =
    await _userRepository.GetByIdAsync(request.FavoriteUserId);

            if (favoriteUser == null)
            {
                throw new Exception("User not found.");
            }
            var favorite =
    await _favoriteRepository.GetAsync(
        currentUserId,
        request.FavoriteUserId);
            if (favorite != null)
            {
                if (!favorite.IsDeleted)
                {
                    throw new Exception(
                        "User is already in your favorites.");
                }

                //--------------------------------------------------
                // Restore Soft Deleted Favorite
                //--------------------------------------------------

                favorite.IsDeleted = false;

                favorite.UpdatedAt = DateTime.UtcNow;

                _favoriteRepository.Update(favorite);

                await _unitOfWork.SaveChangesAsync();

                return;
            }
            var newFavorite = new UserFavorite
            {
                UserId = currentUserId,

                FavoriteUserId = request.FavoriteUserId,

                CreatedAt = DateTime.UtcNow
            };

            await _favoriteRepository.AddAsync(newFavorite);

            await _unitOfWork.SaveChangesAsync();
        }
        public async Task RemoveFavoriteAsync(
    Guid currentUserId,
    Guid favoriteUserId)
        {
            var favorite = await _favoriteRepository.GetAsync(
                currentUserId,
                favoriteUserId);

            if (favorite == null || favorite.IsDeleted)
            {
                throw new Exception("Favorite not found.");
            }

            //-------------------------------------------------------
            // Soft Delete
            //-------------------------------------------------------

            favorite.IsDeleted = true;

            // If BaseEntity has UpdatedAt
            // favorite.UpdatedAt = DateTime.UtcNow;

            _favoriteRepository.Update(favorite);

            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<List<UserFavoriteResponse>> GetFavoritesAsync(
    Guid currentUserId)
        {
            var favorites =
                await _favoriteRepository.GetFavoritesAsync(currentUserId);

            //return favorites.Select(x => new UserFavoriteResponse
            //{
            //    FavoriteId = x.Id,

            //    UserId = x.FavoriteUserId,

            //    AddedOn = x.CreatedAt
            //}).ToList();
            return _mapper.Map<List<UserFavoriteResponse>>(favorites);
        }
        public async Task<FavoriteStatusResponse> CheckFavoriteAsync(
    Guid currentUserId,
    Guid favoriteUserId)
        {
            var favorite = await _favoriteRepository.GetAsync(
                currentUserId,
                favoriteUserId);

            return new FavoriteStatusResponse
            {
                IsFavorite = favorite != null && !favorite.IsDeleted
            };
        }
    }
}
