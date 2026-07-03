using Matrimony.Application.Features.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Dapper
{
    public interface ISearchReadRepository
    {
        Task<PagedResult<SearchProfileResponse>> SearchAsync(
           SearchProfileRequest request,
           Guid currentUserId);
    }
}
