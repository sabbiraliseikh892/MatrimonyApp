using Matrimony.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Domain.Entities
{
    public class ProfilePhoto : BaseEntity
    {
        public Guid UserProfileId { get; set; }

        public string PhotoUrl { get; set; }

        public bool IsPrimary { get; set; }

        public bool IsApproved { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
