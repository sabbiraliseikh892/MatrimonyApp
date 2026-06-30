using Matrimony.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Domain.Entities
{
    public class PartnerPreference :BaseEntity
    {
        public Guid UserId { get; set; }

        public int MinAge { get; set; }

        public int MaxAge { get; set; }

        public int MinHeight { get; set; }

        public int MaxHeight { get; set; }

        public Guid ReligionId { get; set; }

        public string Caste { get; set; }

        public Guid EducationId { get; set; }

        public string Occupation { get; set; }

        public Guid CountryId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
