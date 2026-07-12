using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Helpers
{
    public static class OccupationScoreHelper
    {
        private static readonly Dictionary<string, string> OccupationGroups =
            new(StringComparer.OrdinalIgnoreCase)
            {
                // IT
                { "Software Engineer", "IT" },
                { "Senior Software Engineer", "IT" },
                { "Technical Lead", "IT" },
                { "Team Lead", "IT" },
                { "Solution Architect", "IT" },
                { "Software Architect", "IT" },
                { "Project Manager", "IT" },

                // Medical
                { "Doctor", "Medical" },
                { "Surgeon", "Medical" },
                { "Dentist", "Medical" },
                { "Nurse", "Medical" },

                // Education
                { "Teacher", "Education" },
                { "Lecturer", "Education" },
                { "Professor", "Education" },

                // Government
                { "IAS", "Government" },
                { "IPS", "Government" },
                { "Government Employee", "Government" },
                { "Police Officer", "Government" },

                // Business
                { "Businessman", "Business" },
                { "Entrepreneur", "Business" },
                { "Self Employed", "Business" },

                // Finance
                { "Chartered Accountant", "Finance" },
                { "Accountant", "Finance" },
                { "Bank Manager", "Finance" },
                { "Financial Analyst", "Finance" }
            };

        public static int Calculate(
            string currentOccupation,
            string candidateOccupation)
        {
            // Exact match
            if (string.Equals(
                currentOccupation,
                candidateOccupation,
                StringComparison.OrdinalIgnoreCase))
            {
                return 10;
            }

            if (!OccupationGroups.TryGetValue(
                    currentOccupation,
                    out var currentGroup))
            {
                return 0;
            }

            if (!OccupationGroups.TryGetValue(
                    candidateOccupation,
                    out var candidateGroup))
            {
                return 0;
            }

            // Same occupation group
            if (currentGroup == candidateGroup)
            {
                return 7;
            }

            return 0;
        }
    }
}
