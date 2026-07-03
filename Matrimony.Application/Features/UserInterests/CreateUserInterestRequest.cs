using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.UserInterests
{
    public class CreateUserInterestRequest
    {
        [Required]
        public Guid ToUserId { get; set; }

        [MaxLength(500)]
        public string? InitialMessage { get; set; }
    }
}
