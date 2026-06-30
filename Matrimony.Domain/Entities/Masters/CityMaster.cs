using Matrimony.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Domain.Entities.Masters
{
    public class CityMaster : BaseEntity
    {
        public Guid StateId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? CityCode { get; set; }

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; }

        public virtual StateMaster State { get; set; } = null!;
    }
}
