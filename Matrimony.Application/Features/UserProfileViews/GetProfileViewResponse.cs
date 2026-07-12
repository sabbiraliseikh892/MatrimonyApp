using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.UserProfileViews
{
    public class GetProfileViewResponse
    {
        public Guid ViewedUserId { get; set; }

        public string ProfileId { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public int Age { get; set; }

        public int HeightFeet { get; set; }

        public int HeightInches { get; set; }

        public string Education { get; set; } = string.Empty;

        public string Occupation { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string? PrimaryPhotoUrl { get; set; }

        public DateTime ViewedAt { get; set; }
    }
}
