using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.ProfilePhoto
{
    public class ProfilePhotoResponse
    {
        public Guid Id { get; set; }

        public string PhotoUrl { get; set; } = string.Empty;

        public bool IsPrimary { get; set; }

        public bool IsApproved { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
