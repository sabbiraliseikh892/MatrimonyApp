using Matrimony.Application.Features.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Services
{
    public interface ISearchService
    {
        Task<PagedResult<SearchProfileResponse>> SearchAsync(
            Guid currentUserId,
            SearchProfileRequest request);
    }
}
