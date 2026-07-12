using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.UserFavorites
{
    public class UserFavoriteResponse
    {
        public Guid FavoriteId { get; set; }

        public Guid UserId { get; set; }

        public string ProfileId { get; set; } = string.Empty;

        public int Age { get; set; }

        public int HeightFeet { get; set; }

        public int HeightInches { get; set; }

        public string Education { get; set; } = string.Empty;

        public string Occupation { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string? PhotoUrl { get; set; }

        public DateTime AddedOn { get; set; }
    }
}
