using Matrimony.Application.Interfaces.Services;
using Matrimony.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileViewController : BaseController
    {
        private readonly IUserProfileViewService _profileViewService;
        private readonly ICurrentUserService _currentUserService;
        public ProfileViewController(
            IUserProfileViewService profileViewService,
            ICurrentUserService currentUserService)
        {
            _profileViewService = profileViewService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecentlyViewed()
        {
            var currentUserId =
                _currentUserService.GetUserId(User);

            var result =
                await _profileViewService.GetRecentlyViewedAsync(
                    currentUserId);

            return Success(
                result,
                ResponseMessages.RecentProfilesRetrieved);
        }
        
        
    }
}
