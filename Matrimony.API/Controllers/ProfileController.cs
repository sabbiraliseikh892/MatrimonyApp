using Matrimony.Application.Features.Profile.CreateProfile;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : BaseController
    {
        private readonly IUserProfileService _profileService;
        private readonly IUserProfileViewService _profileViewService;
        private readonly ICurrentUserService _currentUserService;

        public ProfileController(
            IUserProfileService profileService,
            IUserProfileViewService profileViewService,
            ICurrentUserService currentUserService)
        {
            _profileService = profileService;
            _profileViewService = profileViewService;
            _currentUserService = currentUserService;
        }

        //-------------------------------------------------------
        // Get All Profiles
        //-------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _profileService.GetAllAsync();

            return Success(
                result,
                "Profiles retrieved successfully.");
        }

        //-------------------------------------------------------
        // Get Profile By Profile Id (Primary Key)
        //-------------------------------------------------------

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _profileService.GetProfileByIdAsync(id);

            return Success(
                result,
                "Profile retrieved successfully.");
        }

        //-------------------------------------------------------
        // Get My Profile
        //-------------------------------------------------------

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var currentUserId =
                _currentUserService.GetUserId(User);

            var result =
                await _profileService.GetProfileByUserIdAsync(currentUserId);

            return Success(
                result,
                "Profile retrieved successfully.");
        }

        //-------------------------------------------------------
        // Get Profile By User Id
        // Automatically records Recently Viewed
        //-------------------------------------------------------

        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var currentUserId =
                _currentUserService.GetUserId(User);

            var profile =
                await _profileService.GetProfileByUserIdAsync(userId);

            // Record profile view
            await _profileViewService.RecordViewAsync(
                currentUserId,
                userId);

            return Success(
                profile,
                "Profile retrieved successfully.");
        }

        //-------------------------------------------------------
        // Create Profile
        //-------------------------------------------------------

        [HttpPost]
        public async Task<IActionResult> Create(CreateProfileRequest request)
        {
            var currentUserId =
                _currentUserService.GetUserId(User);

            await _profileService.CreateAsync(
                currentUserId,
                request);

            return Success(
                "Profile created successfully.");
        }

        //-------------------------------------------------------
        // Update Profile
        //-------------------------------------------------------

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProfileRequest request)
        {
            await _profileService.UpdateAsync(request);

            return Success(
                "Profile updated successfully.");
        }

        //-------------------------------------------------------
        // Delete Profile
        //-------------------------------------------------------

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _profileService.DeleteAsync(id);

            return Success(
                "Profile deleted successfully.");
        }
    }
}