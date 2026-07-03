using Matrimony.Application.Features.PartnerPreference;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Matrimony.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerPreferenceController : ControllerBase
    {
        private readonly IPartnerPreferenceService _service;

        public PartnerPreferenceController(
            IPartnerPreferenceService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreatePartnerPreferenceRequest request)
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _service.CreateAsync(userId, request);

            return Ok(ApiResponse<object>.SuccessResponse(
                null,
                "Partner preference created successfully."));
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            UpdatePartnerPreferenceRequest request)
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _service.UpdateAsync(userId, request);

            return Ok(ApiResponse<object>.SuccessResponse(
                null,
                "Partner preference updated successfully."));
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyPreference()
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result =
                await _service.GetMyPreferenceAsync(userId);

            return Ok(ApiResponse<PartnerPreferenceResponse?>
                .SuccessResponse(
                    result,
                    "Partner preference retrieved successfully."));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _service.DeleteAsync(userId);

            return Ok(ApiResponse<object>.SuccessResponse(
                null,
                "Partner preference deleted successfully."));
        }
    }
}
