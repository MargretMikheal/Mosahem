using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace mosahem.Application.Features.Authentication.Commands.RevokeToken
{
    public class RevokeTokenValidator : AbstractValidator<RevokeTokenCommand>
    {
        public RevokeTokenValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }
    }
}