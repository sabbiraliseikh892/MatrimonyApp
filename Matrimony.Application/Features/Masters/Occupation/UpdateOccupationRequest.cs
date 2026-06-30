using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.Masters.Occupation
{
    public class UpdateOccupationRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
