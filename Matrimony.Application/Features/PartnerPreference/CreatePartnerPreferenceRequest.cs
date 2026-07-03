using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.PartnerPreference
{
    public class CreatePartnerPreferenceRequest
    {
        // Age
        public int MinAge { get; set; }

        public int MaxAge { get; set; }

        // Height
        public int MinHeightFeet { get; set; }

        public int MinHeightInches { get; set; }

        public int MaxHeightFeet { get; set; }

        public int MaxHeightInches { get; set; }

        // Income
        public decimal? MinAnnualIncome { get; set; }

        public decimal? MaxAnnualIncome { get; set; }

        // Master Preferences
        public Guid? ReligionId { get; set; }

        public Guid? CasteId { get; set; }

        public Guid? MotherTongueId { get; set; }

        public Guid? EducationId { get; set; }

        public Guid? OccupationId { get; set; }

        public Guid? CountryId { get; set; }

        public Guid? StateId { get; set; }

        public Guid? CityId { get; set; }

        // Marital Preferences
        public bool AcceptDivorced { get; set; }

        public bool AcceptWidowed { get; set; }

        public bool AcceptWithChildren { get; set; }
    }
}
