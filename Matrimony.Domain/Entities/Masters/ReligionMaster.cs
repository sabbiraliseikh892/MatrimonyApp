using Matrimony.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Domain.Entities.Masters
{
    public class ReligionMaster : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; }
    }
}
