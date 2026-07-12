using Matrimony.Application.Features.Recommendation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Dapper
{
    public interface IRecommendationRepository
    {
        Task<List<RecommendationResponse>> GetRecommendationsAsync(
            Guid currentUserId,
            RecommendationRequest request);
    }
}
