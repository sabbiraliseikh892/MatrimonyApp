using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.Profile.CreateProfile
{
    public class CreateProfileResponse
    {
        public Guid ProfileId { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
