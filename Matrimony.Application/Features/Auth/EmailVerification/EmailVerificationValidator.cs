using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Features.Auth.EmailVerification
{
    public class EmailVerificationValidator
     : AbstractValidator<EmailVerificationRequest>
    {
        public EmailVerificationValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("Verification token is required.");
        }
    }
}
