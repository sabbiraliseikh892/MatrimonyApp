using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.Masters.State
{
    public class StateResponse
    {
        public Guid Id { get; set; }

        public Guid CountryId { get; set; }

        public string CountryName { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? StateCode { get; set; }
    }
}
