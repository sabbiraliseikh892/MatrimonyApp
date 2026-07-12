using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Helpers
{
    public static class HeightScoreHelper
    {
        public static int Calculate(
            int candidateFeet,
            int candidateInches,
            int minFeet,
            int minInches,
            int maxFeet,
            int maxInches)
        {
            int candidateHeight = candidateFeet * 12 + candidateInches;

            int minHeight = minFeet * 12 + minInches;

            int maxHeight = maxFeet * 12 + maxInches;

            // Perfect match
            if (candidateHeight >= minHeight &&
                candidateHeight <= maxHeight)
            {
                return 10;
            }

            int difference;

            if (candidateHeight < minHeight)
            {
                difference = minHeight - candidateHeight;
            }
            else
            {
                difference = candidateHeight - maxHeight;
            }

            // Close to preferred range
            if (difference <= 2)
                return 7;

            if (difference <= 5)
                return 4;

            return 0;
        }
    }
}
