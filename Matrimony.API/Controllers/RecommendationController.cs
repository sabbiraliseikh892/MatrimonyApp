using Matrimony.Application.Features.Recommendation;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : BaseController
    {
        private readonly IRecommendationService _recommendationService;
        private readonly ICurrentUserService _currentUserService;

        public RecommendationController(
            IRecommendationService recommendationService,
            ICurrentUserService currentUserService)
        {
            _recommendationService = recommendationService;
            _currentUserService = currentUserService;
        }

        //-------------------------------------------------------
        // Get Recommendations
        //-------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> GetRecommendations(
            [FromQuery] RecommendationRequest request)
        {
            var currentUserId =
                _currentUserService.GetUserId(User);

            var result =
                await _recommendationService.GetRecommendationsAsync(
                    currentUserId,
                    request);

            return Success(
    result,
    ResponseMessages.RecommendationsRetrieved);
        }
    }
}
