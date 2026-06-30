using Matrimony.Domain.Common;
using Matrimony.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Domain.Entities
{
    public class UserProfile :BaseEntity
    {
        public Guid UserId { get; set; }

        public string ProfileId { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        // Height
        public int HeightFeet { get; set; }

        public int HeightInches { get; set; }

        public decimal Weight { get; set; }

        // Master References
        public Guid ReligionId { get; set; }

        public Guid CasteId { get; set; }

        public Guid MotherTongueId { get; set; }

        public Guid EducationId { get; set; }

        public Guid OccupationId { get; set; }

        public Guid CountryId { get; set; }

        public Guid StateId { get; set; }

        public Guid CityId { get; set; }

        public decimal AnnualIncome { get; set; }

        public string AboutMe { get; set; } = string.Empty;

        // Navigation Properties
        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ReligionMaster Religion { get; set; } = null!;

        public virtual CasteMaster Caste { get; set; } = null!;

        public virtual MotherTongueMaster MotherTongue { get; set; } = null!;

        public virtual EducationMaster Education { get; set; } = null!;

        public virtual OccupationMaster Occupation { get; set; } = null!;

        public virtual CountryMaster Country { get; set; } = null!;

        public virtual StateMaster State { get; set; } = null!;

        public virtual CityMaster City { get; set; } = null!;

        public virtual ICollection<ProfilePhoto> Photos { get; set; }
            = new List<ProfilePhoto>();
    }
}
