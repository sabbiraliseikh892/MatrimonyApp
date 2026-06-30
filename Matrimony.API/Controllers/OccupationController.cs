using Matrimony.Application.Features.Masters.Occupation;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccupationController : ControllerBase
    {
        private readonly IOccupationService _occupationService;
        public OccupationController(IOccupationService occupationService)
        {
            _occupationService = occupationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _occupationService.GetAllAsync();

            return Ok(ApiResponse<List<OccupationResponse>>
                .SuccessResponse(result, "Occupations retrieved successfully."));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _occupationService.GetByIdAsync(id);

            return Ok(ApiResponse<OccupationResponse>
                .SuccessResponse(result!, "Occupation retrieved successfully."));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOccupationRequest request)
        {
            await _occupationService.AddAsync(request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Occupation created successfully."));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateOccupationRequest request)
        {
            await _occupationService.UpdateAsync(request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Occupation updated successfully."));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _occupationService.DeleteAsync(id);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Occupation deleted successfully."));
        }
    }
}
