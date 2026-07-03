using Matrimony.Application.Features.Search;
using Matrimony.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly ICurrentUserService _currentUserService;

        public SearchController(
            ISearchService searchService,
            ICurrentUserService currentUserService)
        {
            _searchService = searchService;
            _currentUserService = currentUserService;
        }
        [HttpPost]
        public async Task<IActionResult> SearchProfiles(
            SearchProfileRequest request)
        {
            var currentUserId =
                _currentUserService.GetUserId(User);

            var result =
                await _searchService.SearchAsync(
                    currentUserId,
                    request);

            return Ok(result);
        }
    }
}
