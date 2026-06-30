using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.Masters.Country
{
    public class CreateCountryRequest
    {
        public string Name { get; set; } = string.Empty;

        public string CountryCode { get; set; } = string.Empty;
    }
}
