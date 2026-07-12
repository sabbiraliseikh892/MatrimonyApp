using Matrimony.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Domain.Entities
{
    public class UserFavorite : BaseEntity
    {
        // User who created the favorite
        public Guid UserId { get; set; }

        // User who was favorited
        public Guid FavoriteUserId { get; set; }

        // Navigation Properties
        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ApplicationUser FavoriteUser { get; set; } = null!;
    }
}
