using Matrimony.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.UserInterests
{
    public class RespondUserInterestRequest
    {
        [Required]
        public InterestStatus Status { get; set; }

        [MaxLength(500)]
        public string? ResponseReason { get; set; }
    }
}
