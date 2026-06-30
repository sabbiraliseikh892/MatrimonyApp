using Matrimony.Application.Features.Masters.Country;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _countryService.GetAllAsync();

            return Ok(ApiResponse<List<CountryResponse>>
                .SuccessResponse(result, "Countries retrieved successfully."));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _countryService.GetByIdAsync(id);

            return Ok(ApiResponse<CountryResponse>
                .SuccessResponse(result!, "Country retrieved successfully."));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCountryRequest request)
        {
            await _countryService.AddAsync(request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Country created successfully."));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCountryRequest request)
        {
            await _countryService.UpdateAsync(request);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Country updated successfully."));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _countryService.DeleteAsync(id);

            return Ok(ApiResponse<object>
                .SuccessResponse(null, "Country deleted successfully."));
        }
    }
}
