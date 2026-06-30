using Matrimony.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Domain.Entities.Masters
{
    public class StateMaster : BaseEntity
    {
        public Guid CountryId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? StateCode { get; set; }

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; }

        // Navigation Properties
        public virtual CountryMaster Country { get; set; } = null!;

        public virtual ICollection<CityMaster> Cities { get; set; }
            = new List<CityMaster>();
    }
}
