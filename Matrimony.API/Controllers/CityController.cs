using Matrimony.Application.Features.Masters.City;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cityService.GetAllAsync();

            return Ok(ApiResponse<List<CityResponse>>
                .SuccessResponse(result, "Cities retrieved successfully."));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _cityService.GetByIdAsync(id);

            return Ok(ApiResponse<CityResponse>
                .SuccessResponse(result!, "City retrieved successfully."));
        }

        [HttpGet("state/{stateId:guid}")]
        public async Task<IActionResult> GetByState(Guid stateId)
        {
            var result = await _cityService.GetByStateIdAsync(stateId);

            return Ok(ApiResponse<List<CityResponse>>
                .SuccessResponse(result, "Cities retrieved successfully."));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCityRequest request)
        {
            await _cityService.AddAsync(request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "City created successfully."));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCityRequest request)
        {
            await _cityService.UpdateAsync(request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "City updated successfully."));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _cityService.DeleteAsync(id);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "City deleted successfully."));
        }
    }
}
