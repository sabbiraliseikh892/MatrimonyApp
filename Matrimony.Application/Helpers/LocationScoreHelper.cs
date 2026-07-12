using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Helpers
{
    public static class LocationScoreHelper
    {
        public static int Calculate(
            Guid currentCityId,
            Guid currentStateId,
            Guid currentCountryId,
            Guid candidateCityId,
            Guid candidateStateId,
            Guid candidateCountryId)
        {
            // Same City
            if (currentCityId == candidateCityId)
            {
                return 5;
            }

            // Same State
            if (currentStateId == candidateStateId)
            {
                return 3;
            }

            // Same Country
            if (currentCountryId == candidateCountryId)
            {
                return 1;
            }

            return 0;
        }
    }
}
