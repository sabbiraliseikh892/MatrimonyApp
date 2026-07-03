using Dapper;
using Matrimony.Application.Features.Search;
using Matrimony.Application.Interfaces.Dapper;
using Matrimony.Infrastructure.Dapper.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Dapper
{
    public class SearchReadRepository : ISearchReadRepository
    {
        private readonly DapperContext _context;
        private readonly SearchProfileSqlBuilder _builder;
        public SearchReadRepository(
           DapperContext context,
           SearchProfileSqlBuilder builder)
        {
            _context = context;
            _builder = builder;
        }
        public async Task<PagedResult<SearchProfileResponse>> SearchAsync(
            SearchProfileRequest request,
            Guid currentUserId)
        {
            var result = _builder.Build(request, currentUserId);

            using var connection = _context.CreateConnection();

            var profiles = await connection.QueryAsync<SearchProfileResponse>(
                result.Sql,
                result.Parameters);

            return new PagedResult<SearchProfileResponse>
            {
                Items = profiles,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = profiles.Count()
            };
        }
    }
}
