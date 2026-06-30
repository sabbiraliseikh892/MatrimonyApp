using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.Masters.City
{
    public class CityResponse
    {
        public Guid Id { get; set; }

        public Guid StateId { get; set; }

        public string StateName { get; set; } = string.Empty;

        public Guid CountryId { get; set; }

        public string CountryName { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? CityCode { get; set; }
    }
}
