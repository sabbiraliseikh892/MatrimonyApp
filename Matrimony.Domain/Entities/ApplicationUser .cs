using Matrimony.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>

    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public bool IsVerified { get; set; }

        public UserType UserType { get; set; }

        public SubscriptionType SubscriptionType { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual UserProfile Profile { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<UserInterest> SentInterests { get; set; }
    = new List<UserInterest>();

        public virtual ICollection<UserInterest> ReceivedInterests { get; set; }
            = new List<UserInterest>();
        public virtual ICollection<UserFavorite> Favorites { get; set; }
    = new List<UserFavorite>();

        public virtual ICollection<UserFavorite> FavoritedByUsers { get; set; }
            = new List<UserFavorite>();
        public virtual ICollection<UserProfileView> ViewedProfiles { get; set; }
    = new List<UserProfileView>();

        public virtual ICollection<UserProfileView> ViewedByUsers { get; set; }
            = new List<UserProfileView>();
        public ICollection<Notification> Notifications { get; set;} = new List<Notification>();
    }
}
