using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Helpers
{
    public static class AgeCalculator
    {
        public static int Calculate(DateTime dateOfBirth)
        {
            var age = DateTime.Today.Year - dateOfBirth.Year;

            if (dateOfBirth.Date > DateTime.Today.AddYears(-age))
                age--;

            return age;
        }
    }
}
