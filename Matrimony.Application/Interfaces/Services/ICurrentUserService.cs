using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Services
{
    public interface ICurrentUserService
    {
        Guid GetUserId(ClaimsPrincipal user);
    }
}
