using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.Search
{
    public class SearchProfileResponse
    {
        public Guid UserId { get; set; }

        public string ProfileId { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public int Age { get; set; }

        // Height

        public int HeightFeet { get; set; }

        public int HeightInches { get; set; }

        // Masters

        public string Religion { get; set; } = string.Empty;

        public string Caste { get; set; } = string.Empty;

        public string MotherTongue { get; set; } = string.Empty;

        public string Education { get; set; } = string.Empty;

        public string Occupation { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        // Income

        public decimal AnnualIncome { get; set; }

        // Photo

        public string? PhotoUrl { get; set; }

        // Future

        public int MatchPercentage { get; set; }
    }
}
