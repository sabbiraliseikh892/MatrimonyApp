using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.Recommendation
{
    public class RecommendationResponse
    {
        public Guid UserId { get; set; }

        public string ProfileId { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;
        public string? AboutMe { get; set; }

        public Guid CountryId { get; set; }

        public Guid StateId { get; set; }

        public Guid CityId { get; set; }


        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int HeightFeet { get; set; }

        public int HeightInches { get; set; }

        public string Education { get; set; } = string.Empty;

        public string Occupation { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public decimal AnnualIncome { get; set; }

        public string? PrimaryPhotoUrl { get; set; }

        public int MatchPercentage { get; set; }

        public bool IsFavorite { get; set; }

        public bool InterestSent { get; set; }

        public bool RecentlyViewed { get; set; }
    }
}
