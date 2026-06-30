using Matrimony.Application.Features.Masters.Caste;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasteController : ControllerBase
    {
        private readonly ICasteService _casteService;

        public CasteController(ICasteService casteService)
        {
            _casteService = casteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _casteService.GetAllAsync();

            return Ok(ApiResponse<List<CasteResponse>>
                .SuccessResponse(result, "Castes retrieved successfully."));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _casteService.GetByIdAsync(id);

            return Ok(ApiResponse<CasteResponse>
                .SuccessResponse(result!, "Caste retrieved successfully."));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCasteRequest request)
        {
            await _casteService.AddAsync(request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Caste created successfully."));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCasteRequest request)
        {
            await _casteService.UpdateAsync(request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Caste updated successfully."));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _casteService.DeleteAsync(id);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Caste deleted successfully."));
        }
    }
}
