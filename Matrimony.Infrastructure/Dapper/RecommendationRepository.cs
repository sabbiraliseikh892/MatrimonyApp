using Dapper;
using Matrimony.Application.Features.Recommendation;
using Matrimony.Application.Interfaces.Dapper;
using Matrimony.Infrastructure.Dapper.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Dapper
{
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly DapperContext _context;
        private readonly RecommendationSqlBuilder _sqlBuilder;
        public RecommendationRepository(
           DapperContext dapperContext,
           RecommendationSqlBuilder sqlBuilder)
        {
            _context = dapperContext;
            _sqlBuilder = sqlBuilder;
        }
        public async Task<List<RecommendationResponse>> GetRecommendationsAsync(
           Guid currentUserId,
           RecommendationRequest request)
        {
            using var connection = _context.CreateConnection();

            var sqlResult = _sqlBuilder.Build(
                currentUserId,
                request);

            var result = await connection.QueryAsync<RecommendationResponse>(
                sqlResult.Sql,
                sqlResult.Parameters);

            return result.ToList();
        }

    }
}
