using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.Profile.CreateProfile
{
    public class UpdateProfileRequest
    {
        public Guid Id { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Range(4, 7)]
        public int HeightFeet { get; set; }

        [Range(0, 11)]
        public int HeightInches { get; set; }

        [Range(20, 250)]
        public decimal Weight { get; set; }

        public Guid ReligionId { get; set; }

        public Guid CasteId { get; set; }

        public Guid MotherTongueId { get; set; }

        public Guid EducationId { get; set; }

        public Guid OccupationId { get; set; }

        public Guid CountryId { get; set; }

        public Guid StateId { get; set; }

        public Guid CityId { get; set; }

        [Range(0, 100000000)]
        public decimal AnnualIncome { get; set; }

        [MaxLength(2000)]
        public string AboutMe { get; set; } = string.Empty;
    }
}
