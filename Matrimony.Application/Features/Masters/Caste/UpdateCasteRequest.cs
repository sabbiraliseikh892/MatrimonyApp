using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.Masters.Caste
{
    public class UpdateCasteRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
