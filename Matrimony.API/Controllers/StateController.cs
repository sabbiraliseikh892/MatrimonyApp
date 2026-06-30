using Matrimony.Application.Features.Masters.State;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _stateService.GetAllAsync();

            return Ok(ApiResponse<List<StateResponse>>
                .SuccessResponse(result, "States retrieved successfully."));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _stateService.GetByIdAsync(id);

            return Ok(ApiResponse<StateResponse>
                .SuccessResponse(result!, "State retrieved successfully."));
        }

        [HttpGet("country/{countryId:guid}")]
        public async Task<IActionResult> GetByCountry(Guid countryId)
        {
            var result = await _stateService.GetByCountryIdAsync(countryId);

            return Ok(ApiResponse<List<StateResponse>>
                .SuccessResponse(result, "States retrieved successfully."));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStateRequest request)
        {
            await _stateService.AddAsync(request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "State created successfully."));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStateRequest request)
        {
            await _stateService.UpdateAsync(request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "State updated successfully."));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _stateService.DeleteAsync(id);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "State deleted successfully."));
        }
    }
}
