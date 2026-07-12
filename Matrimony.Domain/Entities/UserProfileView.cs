using Matrimony.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Domain.Entities
{
    public class UserProfileView : BaseEntity
    {
        // User who viewed the profile
        public Guid ViewerUserId { get; set; }

        // Profile owner
        public Guid ViewedUserId { get; set; }

        // Last viewed time
        public DateTime ViewedAt { get; set; }

        // Navigation Properties
        public virtual ApplicationUser ViewerUser { get; set; } = null!;

        public virtual ApplicationUser ViewedUser { get; set; } = null!;
    }
}
