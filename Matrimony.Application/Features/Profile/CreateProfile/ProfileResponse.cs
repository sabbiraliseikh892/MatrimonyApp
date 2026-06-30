using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.Profile.CreateProfile
{
    public class ProfileResponse
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string ProfileId { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public int HeightFeet { get; set; }

        public int HeightInches { get; set; }

        public decimal Weight { get; set; }

        public Guid ReligionId { get; set; }

        public string Religion { get; set; } = string.Empty;

        public Guid CasteId { get; set; }

        public string Caste { get; set; } = string.Empty;

        public Guid MotherTongueId { get; set; }

        public string MotherTongue { get; set; } = string.Empty;

        public Guid EducationId { get; set; }

        public string Education { get; set; } = string.Empty;

        public Guid OccupationId { get; set; }

        public string Occupation { get; set; } = string.Empty;

        public Guid CountryId { get; set; }

        public string Country { get; set; } = string.Empty;

        public Guid StateId { get; set; }

        public string State { get; set; } = string.Empty;

        public Guid CityId { get; set; }

        public string City { get; set; } = string.Empty;

        public decimal AnnualIncome { get; set; }

        public string AboutMe { get; set; } = string.Empty;
    }
}
