using Matrimony.Application.Features.Profile.CreateProfile;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Matrimony.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserProfileService _profileService;

        public ProfileController(IUserProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _profileService.GetAllAsync();

            return Ok(ApiResponse<List<ProfileResponse>>
                .SuccessResponse(result, "Profiles retrieved successfully."));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _profileService.GetByIdAsync(id);

            return Ok(ApiResponse<ProfileResponse>
                .SuccessResponse(result!, "Profile retrieved successfully."));
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await _profileService.GetByUserIdAsync(userId);

            return Ok(ApiResponse<ProfileResponse>
                .SuccessResponse(result!, "Profile retrieved successfully."));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProfileRequest request)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _profileService.CreateAsync(userId, request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Profile created successfully."));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProfileRequest request)
        {
            await _profileService.UpdateAsync(request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Profile updated successfully."));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _profileService.DeleteAsync(id);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Profile deleted successfully."));
        }
    }
}
