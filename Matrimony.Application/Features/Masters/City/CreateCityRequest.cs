using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.Masters.City
{
    public class CreateCityRequest
    {
        public Guid StateId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? CityCode { get; set; }
    }
}
