using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Helpers
{
    public static class ProfileCompletenessHelper
    {
        public static int Calculate(
            string? aboutMe,
            string? primaryPhotoUrl,
            string? education,
            string? occupation,
            decimal annualIncome,
            Guid cityId)
        {
            int completedFields = 0;

            if (!string.IsNullOrWhiteSpace(aboutMe))
                completedFields++;

            if (!string.IsNullOrWhiteSpace(primaryPhotoUrl))
                completedFields++;

            if (!string.IsNullOrWhiteSpace(education))
                completedFields++;

            if (!string.IsNullOrWhiteSpace(occupation))
                completedFields++;

            if (annualIncome > 0)
                completedFields++;

            if (cityId != Guid.Empty)
                completedFields++;

            // Maximum 5 points
            return Math.Min(completedFields, 5);
        }
    }
}
