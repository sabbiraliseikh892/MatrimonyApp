using Matrimony.Application.Features.Masters.MotherTongue;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotherTongueController : ControllerBase
    {
        private readonly IMotherTongueService _motherTongueService;

        public MotherTongueController(IMotherTongueService motherTongueService)
        {
            _motherTongueService = motherTongueService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _motherTongueService.GetAllAsync();

            return Ok(ApiResponse<List<MotherTongueResponse>>
                .SuccessResponse(result, "Mother tongues retrieved successfully."));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _motherTongueService.GetByIdAsync(id);

            return Ok(ApiResponse<MotherTongueResponse>
                .SuccessResponse(result!, "Mother tongue retrieved successfully."));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMotherTongueRequest request)
        {
            await _motherTongueService.AddAsync(request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Mother tongue created successfully."));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateMotherTongueRequest request)
        {
            await _motherTongueService.UpdateAsync(request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Mother tongue updated successfully."));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _motherTongueService.DeleteAsync(id);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Mother tongue deleted successfully."));
        }
    }
}
