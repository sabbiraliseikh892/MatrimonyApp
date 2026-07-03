using Matrimony.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.UserInterests
{
    public class UserInterestResponse
    {
        public Guid Id { get; set; }

        public Guid FromUserId { get; set; }

        public Guid ToUserId { get; set; }

        public string FromProfileId { get; set; } = string.Empty;

        public string ToProfileId { get; set; } = string.Empty;

        public string? InitialMessage { get; set; }

        public string? ResponseReason { get; set; }

        public InterestStatus Status { get; set; }

        public DateTime SentOn { get; set; }

        public DateTime? RespondedAt { get; set; }

        // Computed property
        public bool CanChat => Status == InterestStatus.Accepted;
    }
}
