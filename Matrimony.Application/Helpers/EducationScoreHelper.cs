using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Helpers
{
    public static class EducationScoreHelper
    {
        private static readonly Dictionary<string, string> EducationGroups =
            new(StringComparer.OrdinalIgnoreCase)
            {
                // Engineering / IT
                { "B.Tech", "Engineering" },
                { "M.Tech", "Engineering" },
                { "B.E", "Engineering" },
                { "M.E", "Engineering" },
                { "BCA", "Engineering" },
                { "MCA", "Engineering" },

                // Medical
                { "MBBS", "Medical" },
                { "MD", "Medical" },
                { "BDS", "Medical" },
                { "MDS", "Medical" },

                // Management
                { "BBA", "Management" },
                { "MBA", "Management" },

                // Commerce
                { "B.Com", "Commerce" },
                { "M.Com", "Commerce" },
                { "CA", "Commerce" },
                { "CS", "Commerce" },

                // Science
                { "B.Sc", "Science" },
                { "M.Sc", "Science" },

                // Arts
                { "B.A", "Arts" },
                { "M.A", "Arts" }
            };
        public static int Calculate(
            string currentEducation,
            string candidateEducation)
        {
            // Exact match
            if (string.Equals(currentEducation,
                              candidateEducation,
                              StringComparison.OrdinalIgnoreCase))
            {
                return 10;
            }

            if (!EducationGroups.TryGetValue(
                    currentEducation,
                    out var currentGroup))
            {
                return 0;
            }

            if (!EducationGroups.TryGetValue(
                    candidateEducation,
                    out var candidateGroup))
            {
                return 0;
            }

            // Same education group
            if (currentGroup == candidateGroup)
            {
                return 7;
            }

            return 0;
        }
    }
}
