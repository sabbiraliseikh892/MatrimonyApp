using FluentValidation;
using Matrimony.Application.Features.UserFavorites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Validators
{
    public class CreateUserFavoriteRequestValidator : AbstractValidator<CreateUserFavoriteRequest>
    {
        public CreateUserFavoriteRequestValidator()
        {
            RuleFor(x => x.FavoriteUserId)
                .NotEmpty()
                .WithMessage("Favorite User Id is required.");
        }
    }
}
