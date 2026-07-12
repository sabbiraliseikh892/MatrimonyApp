using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.Recommendation
{
    public class RecommendationRequest
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}
