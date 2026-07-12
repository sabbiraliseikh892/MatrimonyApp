using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Helpers
{
    public static class AgeScoreHelper
    {
        public static int Calculate(
           int candidateAge,
           int minAge,
           int maxAge)
        {
            if (candidateAge >= minAge &&
                candidateAge <= maxAge)
            {
                return 15;
            }

            var difference = 0;

            if (candidateAge < minAge)
                difference = minAge - candidateAge;
            else
                difference = candidateAge - maxAge;

            if (difference <= 2)
                return 10;

            if (difference <= 5)
                return 5;

            return 0;
        }
    }
}
