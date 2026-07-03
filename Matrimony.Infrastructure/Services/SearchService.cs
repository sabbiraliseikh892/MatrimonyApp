using Matrimony.Application.Features.Search;
using Matrimony.Application.Interfaces.Dapper;
using Matrimony.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchReadRepository _repository;

        public SearchService(
            ISearchReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<SearchProfileResponse>> SearchAsync(
            Guid currentUserId,
            SearchProfileRequest request)
        {
            //--------------------------------------------------
            // Pagination Validation
            //--------------------------------------------------

            if (request.PageNumber <= 0)
                request.PageNumber = 1;

            if (request.PageSize <= 0)
                request.PageSize = 20;

            if (request.PageSize > 100)
                request.PageSize = 100;

            //--------------------------------------------------
            // Call Repository
            //--------------------------------------------------

            return await _repository.SearchAsync(
                request,
                currentUserId);
        }
    }
}
