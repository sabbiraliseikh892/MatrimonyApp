using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Helpers
{
    public static class IncomeScoreHelper
    {
        public static int Calculate(
    decimal candidateIncome,
    decimal? minIncome,
    decimal? maxIncome)
        {
            if (!minIncome.HasValue || !maxIncome.HasValue)
                return 0;

            if (candidateIncome >= minIncome.Value &&
                candidateIncome <= maxIncome.Value)
            {
                return 5;
            }

            decimal difference;

            if (candidateIncome < minIncome.Value)
            {
                difference = minIncome.Value - candidateIncome;
            }
            else
            {
                difference = candidateIncome - maxIncome.Value;
            }

            if (difference <= 200000)
                return 3;

            if (difference <= 500000)
                return 1;

            return 0;
        }
    }
}
