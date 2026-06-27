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

        public string Religion { get; set; }

        public string Caste { get; set; }

        public string Education { get; set; }

        public string Occupation { get; set; }

        public string Country { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
