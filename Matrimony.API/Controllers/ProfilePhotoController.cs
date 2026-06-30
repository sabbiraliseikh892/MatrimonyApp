using Matrimony.Application.Features.ProfilePhoto;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Matrimony.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilePhotoController : ControllerBase
    {
        private readonly IProfilePhotoService _photoService;

        public ProfilePhotoController(IProfilePhotoService photoService)
        {
            _photoService = photoService;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadProfilePhotoRequest request)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _photoService.UploadAsync(userId, request);

            return Ok(ApiResponse<object>.SuccessResponse(
                null,
                "Photo uploaded successfully."));
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyPhotos()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await _photoService.GetMyPhotosAsync(userId);

            return Ok(ApiResponse<List<ProfilePhotoResponse>>.SuccessResponse(
                result,
                "Photos retrieved successfully."));
        }

        [HttpPut("set-primary/{photoId:guid}")]
        public async Task<IActionResult> SetPrimary(Guid photoId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _photoService.SetPrimaryAsync(userId, photoId);

            return Ok(ApiResponse<object>.SuccessResponse(
                null,
                "Primary photo updated successfully."));
        }

        [HttpDelete("{photoId:guid}")]
        public async Task<IActionResult> Delete(Guid photoId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _photoService.DeleteAsync(userId, photoId);

            return Ok(ApiResponse<object>.SuccessResponse(
                null,
                "Photo deleted successfully."));
        }
    }
}
