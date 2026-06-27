using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.Auth.Register
{
    public class RegisterResponse
    {
        public Guid UserId { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
