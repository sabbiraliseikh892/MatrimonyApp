using Matrimony.Application.Features.Masters.Religion;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReligionController : ControllerBase
    {
        private readonly IReligionService _religionService;

        public ReligionController(IReligionService religionService)
        {
            _religionService = religionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _religionService.GetAllAsync();

            return Ok(ApiResponse<List<ReligionResponse>>
                .SuccessResponse(result, "Religions retrieved successfully."));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _religionService.GetByIdAsync(id);

            return Ok(ApiResponse<ReligionResponse>
                .SuccessResponse(result!, "Religion retrieved successfully."));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReligionRequest request)
        {
            await _religionService.AddAsync(request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Religion created successfully."));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateReligionRequest request)
        {
            await _religionService.UpdateAsync(request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Religion updated successfully."));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _religionService.DeleteAsync(id);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Religion deleted successfully."));
        }
    }
}
