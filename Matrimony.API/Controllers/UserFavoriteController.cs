using Matrimony.Application.Features.UserFavorites;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Shared.Constants;
using Matrimony.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserFavoriteController : BaseController
    {
        private readonly IUserFavoriteService _favoriteService;
        private readonly ICurrentUserService _currentUserService;
        public UserFavoriteController(
            IUserFavoriteService favoriteService,
            ICurrentUserService currentUserService)
        {
            _favoriteService = favoriteService;
            _currentUserService = currentUserService;
        }
        //-------------------------------------------------------
        // Add Favorite
        //-------------------------------------------------------

        [HttpPost]
        public async Task<IActionResult> AddFavorite(
     CreateUserFavoriteRequest request)
        {
            var currentUserId =
                _currentUserService.GetUserId(User);

            await _favoriteService.AddFavoriteAsync(
                currentUserId,
                request);

            return Success(ResponseMessages.FavoriteAdded);
        }
        //-------------------------------------------------------
        // Remove Favorite
        //-------------------------------------------------------

        [HttpDelete("{favoriteUserId:guid}")]
        public async Task<IActionResult> RemoveFavorite(
    Guid favoriteUserId)
        {
            var currentUserId =
                _currentUserService.GetUserId(User);

            await _favoriteService.RemoveFavoriteAsync(
                currentUserId,
                favoriteUserId);

            return Success(ResponseMessages.FavoriteRemoved);
        }

        //-------------------------------------------------------
        // Get Favorites
        //-------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var currentUserId =
                _currentUserService.GetUserId(User);

            var result =
                await _favoriteService.GetFavoritesAsync(
                    currentUserId);

            return Success(
                result,
                ResponseMessages.FavoritesRetrieved);
        }

        //-------------------------------------------------------
        // Check Favorite
        //-------------------------------------------------------

        [HttpGet("check/{favoriteUserId:guid}")]
        public async Task<IActionResult> CheckFavorite(
    Guid favoriteUserId)
        {
            var currentUserId =
                _currentUserService.GetUserId(User);

            var result =
                await _favoriteService.CheckFavoriteAsync(
                    currentUserId,
                    favoriteUserId);

            return Success(
                result,
                ResponseMessages.FavoriteStatusRetrieved);
        }
    }
}
