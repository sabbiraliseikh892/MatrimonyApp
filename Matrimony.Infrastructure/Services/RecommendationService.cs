using Matrimony.Application.Features.Recommendation;
using Matrimony.Application.Helpers;
using Matrimony.Application.Interfaces.Dapper;
using Matrimony.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRepository _recommendationRepository;
        private readonly IUserProfileService _userProfileService;
        private readonly IPartnerPreferenceService _partnerPreferenceService;
        public RecommendationService(
            IRecommendationRepository recommendationRepository,
            IUserProfileService userProfileService,
            IPartnerPreferenceService partnerPreferenceService)
        {
            _recommendationRepository = recommendationRepository;
            _userProfileService = userProfileService;
            _partnerPreferenceService = partnerPreferenceService;
        }
        public async Task<List<RecommendationResponse>> GetRecommendationsAsync(
    Guid currentUserId,
    RecommendationRequest request)
        {
            var currentUser =
    await _userProfileService.GetProfileByUserIdAsync(currentUserId);

            if (currentUser == null)
            {
                return new List<RecommendationResponse>();
            }
            var preference =
    await _partnerPreferenceService.GetMyPreferenceAsync(currentUserId);
            var recommendations =
    await _recommendationRepository.GetRecommendationsAsync(
        currentUserId,
        request);
            foreach (var profile in recommendations)
            {
                profile.MatchPercentage =
                    MatchScoreCalculator.Calculate(
                        profile,
                        currentUser,
                        preference);
            }

            return recommendations
                .OrderByDescending(x => x.MatchPercentage)
                .ToList();
        }
    }
}
