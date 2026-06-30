using Matrimony.Application.Features.Masters.Education;
using Matrimony.Application.Features.Masters.Religion;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IEducationService _educationService;

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _educationService.GetAllAsync();

            return Ok(ApiResponse<List<EducationResponse>>
                .SuccessResponse(result, "Education retrieved successfully."));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _educationService.GetByIdAsync(id);

            return Ok(ApiResponse<EducationResponse>
                .SuccessResponse(result!, "Education retrieved successfully."));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEducationRequest request)
        {
            await _educationService.AddAsync(request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Education created successfully."));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateEducationRequest request)
        {
            await _educationService.UpdateAsync(request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Education updated successfully."));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _educationService.DeleteAsync(id);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Education deleted successfully."));
        }
    }
}
