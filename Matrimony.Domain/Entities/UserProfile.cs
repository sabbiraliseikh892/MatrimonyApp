using Matrimony.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Domain.Entities
{
    public class UserProfile :BaseEntity
    {
        public Guid UserId { get; set; }

        public string ProfileId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Height { get; set; }

        public decimal Weight { get; set; }

        public string Religion { get; set; }

        public string Caste { get; set; }

        public string MotherTongue { get; set; }

        public string Education { get; set; }

        public string Occupation { get; set; }

        public decimal AnnualIncome { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string AboutMe { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<ProfilePhoto> Photos { get; set; }
    }
}
