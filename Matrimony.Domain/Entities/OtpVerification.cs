using Matrimony.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Domain.Entities
{
    public class OtpVerification : BaseEntity
    {
        public Guid UserId { get; set; }

        public string OtpCode { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsUsed { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}
