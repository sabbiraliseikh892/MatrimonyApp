using Matrimony.Domain.Common;
using Matrimony.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Domain.Entities
{
    public class UserInterest : BaseEntity
    {
        public Guid FromUserId { get; set; }

        public Guid ToUserId { get; set; }

        public string? InitialMessage { get; set; }

        public InterestStatus Status { get; set; }
            = InterestStatus.Pending;

        public string? ResponseReason { get; set; }

        public DateTime? RespondedAt { get; set; }

        public virtual ApplicationUser FromUser { get; set; } = null!;

        public virtual ApplicationUser ToUser { get; set; } = null!;
    }

}
