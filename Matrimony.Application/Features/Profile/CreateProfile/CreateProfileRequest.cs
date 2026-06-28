using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.Profile.CreateProfile
{
    public class CreateProfileRequest
    {
        public Guid UserId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Height { get; set; }

        public decimal Weight { get; set; }

        public string Religion { get; set; } = string.Empty;

        public string Caste { get; set; } = string.Empty;

        public string MotherTongue { get; set; } = string.Empty;

        public string Education { get; set; } = string.Empty;

        public string Occupation { get; set; } = string.Empty;

        public decimal AnnualIncome { get; set; }

        public string Country { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string AboutMe { get; set; } = string.Empty;
    }
}
