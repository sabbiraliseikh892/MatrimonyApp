using FluentValidation;
using Matrimony.Application.Features.UserInterests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Validators
{
    public class CreateUserInterestRequestValidator : AbstractValidator<CreateUserInterestRequest>
    {
        public CreateUserInterestRequestValidator()
        {
            RuleFor(x => x.ToUserId)
                .NotEmpty();

            RuleFor(x => x.InitialMessage)
                .MaximumLength(500);
        }

    }
}
