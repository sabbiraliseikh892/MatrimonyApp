using Matrimony.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Domain.Entities.Masters
{
    public class CountryMaster : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string? CountryCode { get; set; }

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; }

        public virtual ICollection<StateMaster> States { get; set; }
            = new List<StateMaster>();
    }
}
